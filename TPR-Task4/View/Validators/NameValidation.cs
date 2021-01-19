using System.Globalization;
using System.Windows.Controls;

namespace View.Validators
{
    class NameValidation : ValidationRule
    {
        public string Error { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null)
            {
                Error = "Name cannot be null!";
                return new ValidationResult(false, Error);
            }
            if(value.ToString().Length == 0 || value.ToString().Length > 50)
            {
                Error = "Name should has <1;50> characters!";
                return new ValidationResult(false, Error);
            }
            return new ValidationResult(true, null);
        }
    }
}