using congestion.calculator;
using System.Linq;
using System;
namespace Congestion.API.Services
{
    public static class StringToDateTimeConverter
    {
      public static DateTime GetDateTime(string time)
      {
        try
        {
            return DateTime.Parse(time);
        }
        catch (System.Exception)
        {
            
            return DateTime.Now;
        }
      }
        public static DateTime[] GetDateTimes(string[] times)
      {
       return times.Select(t => GetDateTime(t)).ToArray();
      }
    }
}
