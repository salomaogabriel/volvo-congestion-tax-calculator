using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Congestion.API.Models
{
    public class CalculatorResponse
    {
        public int TaxAmount { get; set; }
        public string City { get; set; }
        public string Vehicle { get; set; }
    }
}
