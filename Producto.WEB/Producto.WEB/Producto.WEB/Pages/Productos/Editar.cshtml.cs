using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Producto.WEB.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; } = new ProductoRequest();

        [BindProperty]
        public Guid idProducto { get; set; }

        [BindProperty]
        public List<SelectListItem> categorias { get; set; } = new();

        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; } = new();

        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var productoResponse = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

            if (productoResponse == null)
                return NotFound();

            idProducto = productoResponse.Id;

            producto.Nombre = productoResponse.Nombre;
            producto.Descripcion = productoResponse.Descripcion;
            producto.Precio = productoResponse.Precio;
            producto.Stock = productoResponse.Stock;
            producto.CodigoBarras = productoResponse.CodigoBarras;
            producto.IdSubCategoria = productoResponse.IdSubCategoria;

            categoriaSeleccionada = productoResponse.IdCategoria;

            await ObtenerCategorias();

            subcategorias = (await ObtenerSubCategorias(categoriaSeleccionada))
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nombre,
                    Selected = s.Id == productoResponse.IdSubCategoria
                }).ToList();

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await ObtenerCategorias();

                if (categoriaSeleccionada != Guid.Empty)
                {
                    subcategorias = (await ObtenerSubCategorias(categoriaSeleccionada))
                        .Select(s => new SelectListItem
                        {
                            Value = s.Id.ToString(),
                            Text = s.Nombre,
                            Selected = s.Id == producto.IdSubCategoria
                        }).ToList();
                }

                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            var cliente = new HttpClient();

            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, idProducto), producto);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }

        private async Task ObtenerCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultadoDeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);

            categorias = resultadoDeserializado.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == categoriaSeleccionada
            }).ToList();
        }

        private async Task<List<SubCategoria>> ObtenerSubCategorias(Guid idCategoria)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, idCategoria));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }

            return new List<SubCategoria>();
        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid categoriaId)
        {
            var lista = await ObtenerSubCategorias(categoriaId);
            return new JsonResult(lista);
        }
    }
}