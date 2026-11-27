using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class OutOfRangeEventArgs : EventArgs
    {
        public OutOfRangeEventArgs(int start, int end)
        {
            Start = start;
            End = end;
        }
        public int Start { get; }
        public int End { get; }
    }
}
