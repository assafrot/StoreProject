using System;
using System.ComponentModel.DataAnnotations;

namespace Client.Extensions
{
    public class DateTimeRangeAttribute : RangeAttribute
    {
        public DateTimeRangeAttribute()
            : base(typeof(DateTime),
                DateTime.Now.AddYears(-90).ToString("dd/MM/yyyy"),
                DateTime.Now.ToString("dd/MM/yyyy"))
        {
            ErrorMessage = $"Birth Date must be between {DateTime.Now.AddYears(-100).ToString("dd/MM/yyyy")}" +
                           $" and {DateTime.Now.ToString("dd/MM/yyyy")} ";
        }
    }
}