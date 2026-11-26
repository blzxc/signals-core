using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class OfflineFilter
    {
        static public float[] Filter(float[] data, int windowSize)
        {
            //int M = (windowSize - 1) / 2;
            //IEnumerable<float> x = Enumerable.Range(0, M).Select(el => data[0]);
            //IEnumerable<float> z = Enumerable.Range(0, M).Select(el => data.Last());
            //float[] y = x.Concat(data).Concat(z).ToArray();

            //float[] newSignal = new float[data.Length];

            //for (int i = 0; i < y.Length; i++)
            //{
            //    y[i] += (data[i - M]) / windowSize;
            //}

            //return newSignal;
            // Расширяем сигнал в начале и конце дублированием первого и последнего значений
            // Проверка входных параметров
            int M = (windowSize - 1) / 2;

            // Создаём расширенный массив с дублированием крайних значений
            IEnumerable<float> x = Enumerable.Range(0, M).Select(el => data[0]);
            IEnumerable<float> z = Enumerable.Range(0, M).Select(el => data.Last());
            float[] y = x.Concat(data).Concat(z).ToArray();

            float[] newSignal = new float[data.Length];

            // Применяем скользящее усреднение
            for (int i = 0; i < data.Length; i++)
            {
                float sum = 0f;

                // Суммируем значения в окне: от i до i + windowSize в расширенном массиве
                for (int j = i; j < i + windowSize; j++)
                {
                    sum += y[j];
                }

                // Усредняем и сохраняем в результат
                newSignal[i] = sum / windowSize;
            }

            return newSignal;

        }
    }
}
