using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class OnlineMedianFilterTests
    {
        [Fact] //Тест 46
        public void Filter_RandomSignal_EqualsOfflineWithDelay()
        {
            float[] data = CreateRandomSignal(1000);
            float[] offline = OfflineMedianFilter.Filter(data, 7);

            float[] online = new OnlineMedianFilter(7).Filter(data);

            for (int i = 3; i < data.Length; i++)
            {
                Assert.Equal(offline[i - 3], online[i], 1e-5f);
            }
        }

        [Theory] //Тест 47
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(100)]
        public void Filter_SignalInChunks_ReturnsSameAsWhole(int chunkSize)
        {
            float[] data = CreateRandomSignal(1000);
            float[] expected = new OnlineMedianFilter(7).Filter(data);
            OnlineMedianFilter filter = new OnlineMedianFilter(7);
            List<float> result = new List<float>();

            for (int i = 0; i < data.Length; i += chunkSize)
            {
                float[] chunk = data.Skip(i).Take(chunkSize).ToArray();
                result.AddRange(filter.Filter(chunk));
            }

            AssertSignal.Equal(expected, result.ToArray());
        }

        [Theory] //Тест 48
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(4)]
        public void Constructor_InvalidWindowSize_Throws(int windowSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>("windowSize",
                () => new OnlineMedianFilter(windowSize));
        }

        static private float[] CreateRandomSignal(int length)
        {
            Random random = new Random(42);
            float[] data = new float[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = (float)random.NextDouble();
            }
            return data;
        }
    }
}
