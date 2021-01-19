using System.Globalization;
using System.Windows.Controls;

namespace View.Validators
{
    class DecimalValidation : ValidationRule
    {
        public string Error { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(decimal.TryParse(value.ToString(), NumberStyles.AllowDecimalPoint, cultureInfo ,out decimal i))
            {
                
                if(i >= 0)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    Error = "Number must be positive!";
                    return new ValidationResult(false, Error);
                }
            }
            Error = "Invalid number format!";
            return new ValidationResult(false, Error);
        }
    }
}