using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required]
        [StringLength(40, ErrorMessage = "El nombre no debe exceder los 40 caracteres", MinimumLength = 4)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(90, ErrorMessage = "La descripcion no debe exceder los 90 caracteres", MinimumLength = 9)]
        public string Descripcion { get; set; }
        [Required]
        
        public decimal Precio { get; set; }
        [Required]
        
        public int Stock { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El codigo de barra no debe exceder los 100 caracteres", MinimumLength =10)]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        [Required(ErrorMessage = "La subcategoría es requerida")]
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdSubCategoria { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
    }
}
