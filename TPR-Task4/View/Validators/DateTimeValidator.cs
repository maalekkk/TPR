using System;
using System.Globalization;
using System.Windows.Controls;

namespace View.Validators
{
    class DateTimeValidator : ValidationRule
    {
        public string Error { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date;
            try
            {
                date = DateTime.Parse(value.ToString(), cultureInfo);
            }
            catch
            {
                string Error = "Invalid date format!";
                return new ValidationResult(false, Error);
            }
            if (date.Year > 1799 && date.Year <= DateTime.Today.Year)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                Error = "Date must be between 1/1/1799 and 1/1/9999";
                return new ValidationResult(false, Error);
            }
        }
    }
}