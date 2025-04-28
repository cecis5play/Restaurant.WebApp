using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Models
{
    public class CustomDateAttribute : RangeAttribute
    {
            public CustomDateAttribute()
              : base(typeof(DateTime),
                      DateTime.Now.ToShortDateString(),
                      DateTime.Now.AddYears(1).ToShortDateString())

            { }  
    }
}
