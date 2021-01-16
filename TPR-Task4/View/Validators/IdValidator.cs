using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace View.Validators
{
    class IdValidator : ValidationRule
    {
        public string Error { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(short.TryParse(value.ToString(), out short i))
            {
                
                if(i >= 0)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    Error = "ID must be greater than 0";
                    return new ValidationResult(false, Error);
                }
            }
            Error = "Invalid number format!";
            return new ValidationResult(false, Error);
        }
    }
}
