using System;
namespace Congestion.API.Models
{
    public class CalculatorRequest
    {
       public string vehicleType {get; set;}
       public string[] DateTimes {get; set;}
       public string City {get; set;} = "Gothenburg";
    }
}
