using System.ComponentModel.DataAnnotations;

namespace prog3_practica_08.CustomValidations
{
    public class ValidateAlquilerDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;

            if (dt <= DateTime.Now.AddDays(3) && dt >= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Fecha no esta en rango (maximo 3 dias).");
            }
        }
    }
}
