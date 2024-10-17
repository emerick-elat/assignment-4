using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char Symbol { get; set; }
        public decimal ValueToUSD { get; set; }
        

    }
}
