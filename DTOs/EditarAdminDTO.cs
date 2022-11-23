using System.ComponentModel.DataAnnotations;

namespace ApiLibros2.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
