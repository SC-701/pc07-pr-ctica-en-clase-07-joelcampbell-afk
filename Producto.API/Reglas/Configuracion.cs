using Abstracciones.Interfaces.Reglas;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class Configuracion: IConfiguracion
    {
        private IConfiguration _configuration;
        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ObtenerMetodo(string seccion, string metodo)
        {
            var valor = _configuration.GetSection(seccion)[metodo];
            if (string.IsNullOrWhiteSpace(valor))
                throw new Exception($"No se encontró la configuración: {seccion}:{metodo}");
            return valor;
        }

        public string ObtenerValor(string seccion, string llave)
        {
            var valor = _configuration.GetSection(seccion)[llave];
            if (string.IsNullOrWhiteSpace(valor))
                throw new Exception($"No se encontró la configuración: {seccion}:{llave}");
            return valor;
        }
    }
}
