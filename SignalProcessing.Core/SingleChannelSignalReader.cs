using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class SingleChannelSignalReader
    {
        static public SingleChannelSignal Read(string filePath)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                string first_line = reader.ReadLine();
                if (first_line == null)
                {
                    throw new InvalidDataException("Файл пуст");
                }
                float freq;
                if (!float.TryParse(first_line, NumberStyles.Float, CultureInfo.InvariantCulture, out freq))
                {
                    throw new InvalidDataException("Строка 1: частота не является числом");
                }
                if (freq <= 0)
                {
                    throw new InvalidDataException("Строка 1: частота должна быть положительной");
                }
                List<float> value = new List<float>();
                int line_number = 1;
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    line_number++;
                    float element;
                    if (!float.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out element))
                    {
                        throw new InvalidDataException("Строка " + line_number + ": значение не является числом");
                    }
                    value.Add(element);
                }
                float[] arrSignal = value.ToArray();
                SingleChannelSignal data = new SingleChannelSignal(arrSignal, freq);
                return data;
            }

        }

    }
}
