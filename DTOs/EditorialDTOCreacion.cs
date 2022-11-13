﻿using ApiLibros2.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros2.DTOs
{
    public class EditorialDTOCreacion
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} solo puede tener 30 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener 150 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 12, ErrorMessage = "El campom {0} solo puede tener 12 caracteres")]
        public string Telefono { get; set; }
    }
}
