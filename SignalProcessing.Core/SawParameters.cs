using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class SawParameters : GeneratorParameters
    {
        public float Period { get; set; }
        public float Phase { get; set; }
        public float Amplitude { get; set; }
    }
}
