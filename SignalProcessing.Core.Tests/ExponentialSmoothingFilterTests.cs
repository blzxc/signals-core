using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class ExponentialSmoothingFilterTests
    {
        [Fact] //Тест Д.3.1
        public void Filter_ConstantSignal_ReturnsSameConstant()
        {
            ExponentialSmoothingFilter filter = new ExponentialSmoothingFilter(0.3f);
            float[] data = { 3, 3, 3, 3, 3 };

            float[] result = filter.Filter(data);

            AssertSignal.Equal(data, result);
        }

        [Fact] //Тест Д.3.2
        public void Filter_AlphaOne_ReturnsInput()
        {
            ExponentialSmoothingFilter filter = new ExponentialSmoothingFilter(1);
            float[] data = { 1, 5, -2, 7 };

            float[] result = filter.Filter(data);

            AssertSignal.Equal(data, result);
        }

        [Fact] //Тест Д.3.3
        public void Filter_Step_ReturnsExpected()
        {
            float alpha = 0.5f;
            ExponentialSmoothingFilter filter = new ExponentialSmoothingFilter(alpha);
            float[] data = { 0, 0, 1, 1, 1, 1, 1 };

            float[] result = filter.Filter(data);

            for (int n = 1; n <= 5; n++)
            {
                float expected = 1 - (float)Math.Pow(1 - alpha, n);
                Assert.Equal(expected, result[n + 1], 1e-5f);
            }
        }

        [Theory] //Тест Д.3.4
        [InlineData(0f)]
        [InlineData(-0.5f)]
        [InlineData(1.5f)]
        public void Constructor_InvalidAlpha_Throws(float alpha)
        {
            Assert.Throws<ArgumentOutOfRangeException>("alpha",
                () => new ExponentialSmoothingFilter(alpha));
        }
    }
}
