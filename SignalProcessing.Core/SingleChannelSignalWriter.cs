using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class SingleChannelSignalWriter
    {
        static public void Write(SingleChannelSignal signal, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(signal.Frequency);
                foreach(double element in signal.Data)
                {
                    writer.WriteLine(element);
                }
                
            }

        }
    }
}
