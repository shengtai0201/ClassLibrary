using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.DataAnnotations
{
    public class CheckBoxAttribute : ValidationAttribute
    {
        private readonly bool isChecked;
        private readonly string errorMessage;
        public CheckBoxAttribute(bool isChecked, string errorMessage)
        {
            this.isChecked = isChecked;
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool check = Convert.ToBoolean(value);
            if (check == this.isChecked)
                return ValidationResult.Success;
            else
                return new ValidationResult(errorMessage);
        }
    }
}
