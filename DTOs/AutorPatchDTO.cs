using ApiLibros2.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros2.DTOs
{
    public class AutorPatchDTO
    {
        [StringLength(maximumLength: 10, ErrorMessage = "El campom {0} solo puede tener 10 caracteres")]
        [PrimeraLetraMayuscula]
        public string NombreAutor { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campom {0} solo puede tener 10 caracteres")]

        public string ApellidoAutor { get; set; }

    }
}
