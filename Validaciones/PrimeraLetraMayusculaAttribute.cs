using System.ComponentModel.DataAnnotations;

namespace ApiLibros2.Validaciones
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute
    {
        //ValidationAttribute me permite utilizar isValid, este me permite obtener el valor del objeto
        //Y mostrar si es correcto o incorrecto.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) //Validacion para saber si el dato es nulo o no existe
            {
                return ValidationResult.Success;
            }

            var primeraLetra = value.ToString()[0].ToString(); //Obtenemos la primer letra del objeto

            if (primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primer letra debe ser mayuscula");
            }

            return ValidationResult.Success;
        }

    }
}
