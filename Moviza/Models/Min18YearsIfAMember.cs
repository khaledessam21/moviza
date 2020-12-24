using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Moviza.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Uknown ||
                customer.MembershipTypeId == MembershipType.PayAsUGo)
            {
                return ValidationResult.Success;
            }

            if(customer.Birthday==null)
            {
                return new ValidationResult("birthdate is required");
            }
            var age = DateTime.Today.Year - customer.Birthday.Year;
            return (age >= 18) ? ValidationResult.Success : new ValidationResult("customer must be more than 18 years old");
        }
    }
}