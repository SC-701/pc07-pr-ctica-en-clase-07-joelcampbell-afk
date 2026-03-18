using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.BancoCentral
{
    public class TipoCambioModels
    {
    }
    public class TipoCambioBccrResponse
    {
        public bool Estado { get; set; }
        public string? Mensaje { get; set; }
        public List<TipoCambioDatoItem> Datos { get; set; } = new();
    }

    public class TipoCambioDatoItem
    {
        public string? Titulo { get; set; }
        public string? Periodicidad { get; set; }
        public List<TipoCambioIndicador> Indicadores { get; set; } = new();
    }

    public class TipoCambioIndicador
    {
        public string? CodigoIndicador { get; set; }
        public string? NombreIndicador { get; set; }
        public List<TipoCambioSerie> Series { get; set; } = new();
    }

    public class TipoCambioSerie
    {
        public string? Fecha { get; set; }
        public decimal ValorDatoPorPeriodo { get; set; }
    }
}