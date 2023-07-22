using System.ComponentModel.DataAnnotations;
using System;


namespace PrivateClinic.Persistence
{


    public class FutureDate : ValidationAttribute
    {

        public string GetErrorMessage() =>
            $"Csak jövőbeli időpont foglalható";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var booking = (Booking)validationContext.ObjectInstance;
            var releaseYear = ((DateTime)value!).Year;

            if (booking.DateTime <= DateTime.Now)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }


}
