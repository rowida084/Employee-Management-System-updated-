using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Results<t>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public t data { get; set; }

    }
}
