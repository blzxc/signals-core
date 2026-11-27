using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class OfflineMedianFilterTests
    {
        //--------------Тест 43
        [Fact]
        public void Filter_SingleOutlier_IsRemoved()
        {
            float[] data = { 1, 1, 1, 9, 1, 1, 1 };

            float[] result = OfflineMedianFilter.Filter(data, 3);

            AssertSignal.Equal(new float[] { 1, 1, 1, 1, 1, 1, 1 }, result);
        }

        //--------------Тест 44
        [Fact]
        public void Filter_Step_ReturnsSameStep()
        {
            float[] data = { 0, 0, 0, 1, 1, 1 };

            float[] result = OfflineMedianFilter.Filter(data, 3);

            AssertSignal.Equal(data, result);
        }

        //--------------Тест 45
        [Fact]
        public void MovingAverage_Step_IsBlurred()
        {
            float[] data = { 0, 0, 0, 1, 1, 1 };

            float[] result = OfflineFilter.Filter(data, 3);

            Assert.NotEqual(data, result);
        }

        //--------------Тест 48
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(4)]
        public void Filter_InvalidWindowSize_Throws(int windowSize)
        {
            float[] data = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>("windowSize",
                () => OfflineMedianFilter.Filter(data, windowSize));
        }
    }
}
