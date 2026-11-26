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
        static public void Write(SingleChannelSignal obj, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(obj.Freq);
                foreach(double element in obj.Value)
                {
                    writer.WriteLine(element);
                }
                
            }

        }
    }
}
