using System.ComponentModel.DataAnnotations;

namespace ApiLibros2.Entidades
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} solo puede tener 30 caracteres")] //Delimitamos la cantidad de caracteres antes de pasarlo a la base de datos. 
        
        public string NombreLibro { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 4, ErrorMessage = "El campo solo debe tener 4 caracteres")]
        public string Fecha { get; set; }

        public List<LibroAutor> LibroAutor { get; set; }
        public List<Autor> autor { get; set; }
        public List<Editorial> Editorial { get; set; }
    }
}

