using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class SineParameters : GeneratorParameters
    {
        public float Period { get; set; }       // период, с
        public float Phase { get; set; }        // фаза (сдвиг), с
        public float Amplitude { get; set; }    // масштабирующий коэффициент
    }
}
