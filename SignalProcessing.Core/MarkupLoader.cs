using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class MarkupLoader
    {
        static public List<OutOfRangeEventArgs> Load(string filePath, out float freq)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                freq = (float.Parse(reader.ReadLine()));
                List<OutOfRangeEventArgs> markup = new List<OutOfRangeEventArgs>();
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    string[] bounds = line.Split(';');
                    markup.Add(new OutOfRangeEventArgs(int.Parse(bounds[0]), int.Parse(bounds[1])));
                }
                return markup;
            }

        }

    }
}
