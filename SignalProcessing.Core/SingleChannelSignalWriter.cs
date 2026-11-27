using System;
using System.Collections.Generic;
using System.Globalization;
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
                writer.WriteLine(signal.Frequency.ToString("R", CultureInfo.InvariantCulture));
                foreach(float element in signal.Data)
                {
                    writer.WriteLine(element.ToString("R", CultureInfo.InvariantCulture));
                }
                
            }

        }
    }
}
