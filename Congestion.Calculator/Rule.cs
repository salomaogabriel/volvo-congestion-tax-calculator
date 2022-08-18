using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator
{
    public class Rule
    {
        public string CityName { get; set; }
        public bool hasSingleChargeRule { get; set; }
        public List<CongestionTableElement> Table { get; set; }

    }
    [Serializable]
    public class CongestionTableElement
    {
        public int startHour { get; set; }
        public int startMinute { get; set; }
        public int endHour { get; set; }
        public int endMinute { get; set; }
        public int amount { get; set; }
    }
}