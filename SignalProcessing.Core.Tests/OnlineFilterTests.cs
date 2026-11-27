using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class OnlineFilterTests
    {
        [Fact] //Тест 10
        public void Filter_LinearSignal_ReturnsExpected()
        {
            OnlineFilter filter = new OnlineFilter(3);
            float[] data = { 1, 2, 3, 4, 5 };

            float[] result = filter.Filter(data);

            AssertSignal.Equal(new float[] { 1, 4f / 3, 2, 3, 4 }, result);
        }

        [Fact] //Тест 11
        public void Filter_TwoCalls_ReturnsSameAsOneCall()
        {
            OnlineFilter filter = new OnlineFilter(3);
            float[] first_part = { 1, 2 };
            float[] second_part = { 3, 4, 5 };

            float[] first_result = filter.Filter(first_part);
            float[] second_result = filter.Filter(second_part);

            float[] result = first_result.Concat(second_result).ToArray();
            AssertSignal.Equal(new float[] { 1, 4f / 3, 2, 3, 4 }, result);
        }

        [Theory] //Тест 12
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(100)]
        public void Filter_SignalInChunks_ReturnsSameAsWhole(int chunkSize)
        {
            float[] data = CreateRandomSignal(1000);
            float[] expected = new OnlineFilter(7).Filter(data);
            OnlineFilter filter = new OnlineFilter(7);
            List<float> result = new List<float>();

            for (int i = 0; i < data.Length; i += chunkSize)
            {
                float[] chunk = data.Skip(i).Take(chunkSize).ToArray();
                result.AddRange(filter.Filter(chunk));
            }

            AssertSignal.Equal(expected, result.ToArray());
        }

        [Fact] //Тест 13
        public void Filter_RandomSignal_EqualsOfflineWithDelay()
        {
            float[] data = CreateRandomSignal(1000);
            float[] offline = OfflineFilter.Filter(data, 7);

            float[] online = new OnlineFilter(7).Filter(data);

            for (int i = 3; i < data.Length; i++)
            {
                Assert.Equal(offline[i - 3], online[i], 1e-5f);
            }
        }

        [Theory] //Тест 14
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidWindowSize_Throws(int windowSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>("windowSize",
                () => new OnlineFilter(windowSize));
        }

        [Fact] //Тест 33
        public void Reset_AfterSignal_SameAsNewFilter()
        {
            OnlineFilter filter = new OnlineFilter(3);
            float[] first_signal = { 10, 20, 30, 40 };
            float[] second_signal = { 1, 2, 3, 4, 5 };
            float[] expected = new OnlineFilter(3).Filter(second_signal);
            filter.Filter(first_signal);

            filter.Reset();
            float[] result = filter.Filter(second_signal);

            AssertSignal.Equal(expected, result);
        }

        [Fact] //Тест 34
        public void Filter_WindowSizeOne_ReturnsInput()
        {
            OnlineFilter filter = new OnlineFilter(1);
            float[] data = { 1, 5, -2, 7 };

            float[] result = filter.Filter(data);

            AssertSignal.Equal(data, result);
        }

        [Fact] //Тест 35
        public void Filter_LongSignal_MatchesDirectCalculation()
        {
            Random random = new Random(42);
            float[] data = new float[200000];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 900 + (float)random.NextDouble() * 200;
            }
            float[] expected = new float[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                double sum = 0;
                for (int j = i - 6; j <= i; j++)
                {
                    if (j < 0)
                    {
                        sum += data[0];
                    }
                    else
                    {
                        sum += data[j];
                    }
                }
                expected[i] = (float)(sum / 7);
            }

            float[] result = new OnlineFilter(7).Filter(data);

            AssertSignal.Equal(expected, result, 1e-3f);
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
