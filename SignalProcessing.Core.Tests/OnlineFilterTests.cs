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
        [Fact]
        public void Filter_LinearSignal_ReturnsExpected()
        {
            OnlineFilter filter = new OnlineFilter(3);
            float[] data = { 1, 2, 3, 4, 5 };

            float[] result = filter.Filter(data);

            AssertSignal.Equal(new float[] { 1, 4f / 3, 2, 3, 4 }, result);
        }

        [Fact]
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

        [Theory]
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

        [Fact]
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

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidWindowSize_Throws(int windowSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>("windowSize",
                () => new OnlineFilter(windowSize));
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
