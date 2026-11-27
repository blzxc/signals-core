using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class LineParameters : GeneratorParameters
    {
        public float K { get; set; }
        public float B { get; set; }
    }
}
