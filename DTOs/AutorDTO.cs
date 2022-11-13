using ApiLibros2.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros2.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string NombreAutor { get; set; }

        public string ApellidoAutor { get; set; }


    }
}
