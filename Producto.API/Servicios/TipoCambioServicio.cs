 using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.BancoCentral;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servicios
{
    public class TipoCambioServicio: ITipoCambioServicio
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguracion _configuracion;
        public TipoCambioServicio(IHttpClientFactory httpClient, IConfiguracion configuracion)
        {
            _httpClient = httpClient;
            _configuracion = configuracion;
        }
        public async Task<decimal> ObtenerTipoCambioVentaAsync(DateTime fecha)
        {
            var urlBase = _configuracion.ObtenerValor("BancoCentralCR", "UrlBase");
            var token = _configuracion.ObtenerValor("BancoCentralCR", "BearerToken");
            var idioma = _configuracion.ObtenerValor("BancoCentralCR", "Idioma"); // "ES"
            var codigoIndicador = _configuracion.ObtenerValor("BancoCentralCR", "CodigoIndicador"); // "318"

            
            var fechaParam = fecha.ToString("yyyy/MM/dd");

            var url =
                $"{urlBase}?fechaInicio={Uri.EscapeDataString(fechaParam)}" +
                $"&fechaFin={Uri.EscapeDataString(fechaParam)}" +
                $"&idioma={Uri.EscapeDataString(idioma)}";

            var client = _httpClient.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var resp = await client.GetAsync(url);
            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new Exception($"Error BCCR {(int)resp.StatusCode}: {json}");

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<TipoCambioBccrResponse>(json, opciones);

            if (data == null || data.Datos == null || data.Datos.Count == 0)
                throw new Exception("Respuesta del BCCR sin datos.");

            // Buscar el indicador 318 y tomar el primer valor de series
            var indicador = data.Datos
                .SelectMany(d => d.Indicadores ?? new List<TipoCambioIndicador>())
                .FirstOrDefault(i => string.Equals(i.CodigoIndicador, codigoIndicador, StringComparison.OrdinalIgnoreCase));

            var serie = indicador?.Series?.FirstOrDefault();

            if (serie == null || serie.ValorDatoPorPeriodo <= 0)
                throw new Exception("No se encontró un valor válido de tipo de cambio en la respuesta.");

            return serie.ValorDatoPorPeriodo;
        }
    }
}