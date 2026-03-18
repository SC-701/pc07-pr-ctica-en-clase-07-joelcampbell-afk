using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdSubCategoria = producto.IdSubCategoria,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            });
            return resultadoConsulta;
        }
            
        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            await verificaProductoExiste(Id);
            string query = @"EditarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id= Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdSubCategoria = producto.IdSubCategoria,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificaProductoExiste(Id);
            string query = @"EliminarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"Obtener";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;

        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query, new {id=Id});
            return resultadoConsulta.FirstOrDefault();
        }
        private async Task verificaProductoExiste(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("El producto no existe");
        }
    }
}
