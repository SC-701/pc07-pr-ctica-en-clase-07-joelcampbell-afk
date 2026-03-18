using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }

        public async Task<decimal> CalcularPrecioUSD(decimal precioCRC)
        {
            decimal tipoCambio = 0;

            for (int i = 0; i < 7; i++)
            {
                try
                {
                    tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioVentaAsync(DateTime.Today.AddDays(-i));
                    if (tipoCambio > 0) break;
                }
                catch
                {
                    // probamos día anterior
                }
            }

            if (tipoCambio <= 0)
                throw new Exception("No fue posible obtener tipo de cambio válido en los últimos 7 días.");

            return Math.Round(precioCRC / tipoCambio, 2);
        }
    }
}