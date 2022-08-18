using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace congestion.calculator
{
    public static class RuleFactory
    {
        public static Rule GetRuleFromFile(string cityName)
        {
            using (StreamReader r = new StreamReader($"{cityName.ToLower()}.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Rule>(json);
            }
        }
    }
}
