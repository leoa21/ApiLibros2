using ApiLibros2.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros2.Entidades
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campom {0} solo puede tener 10 caracteres")]
        [PrimeraLetraMayuscula]
        public string NombreAutor { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campom {0} solo puede tener 10 caracteres")]
        [PrimeraLetraMayuscula]
        public string ApellidoAutor { get; set; }

        public List<LibroAutor> LibroAutor { get; set; }
    }
}
