using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class MarkupWriter
    {
        static public void Write(float freq, List<OutOfRangeEventArgs> markup, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(freq);
                foreach (OutOfRangeEventArgs element in markup)
                {
                    writer.WriteLine(element.Start + ";" + element.End);
                }

            }

        }
    }
}
