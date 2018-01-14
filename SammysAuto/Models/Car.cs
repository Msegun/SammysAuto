using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SammysAuto.Models
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string VIN { get; set; }
        [Required]
        [BMWCheck]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public string Style { get; set; }
        [Required]
        [YearCheck]
        public int Year { get; set; }
        [Required]
        public double Miles { get; set; }
        [Required]
        public string Color { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class YearCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Car car = (Car)validationContext.ObjectInstance;

            if (car == null)
                throw new ArgumentException("Atribute not applied on Car");

            if(car.Year > DateTime.Now.Year)
            {
                return new ValidationResult(GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            return "Can't Create car that has not been created yet";
        }
    }


    public class BMWCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Car car = (Car)validationContext.ObjectInstance;

            if (car == null)
                throw new ArgumentException("Atribute not applied on Car");

            if (car.Make == "BMW")
            {
                return new ValidationResult(GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            return "You're not welcome in our Company plz leave";
        }
    }
}
