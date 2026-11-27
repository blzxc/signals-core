using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class OfflineFilterTests
    {
        [Fact] //Тест 3
        public void Filter_ConstantSignal_ReturnsSameConstant()
        {
            // Arrange
            float[] data = { 5, 5, 5, 5, 5 };

            // Act
            float[] result = OfflineFilter.Filter(data, 3);

            // Assert
            Assert.Equal(data.Length, result.Length);
            foreach (float value in result)
            {
                Assert.Equal(5f, value, 1e-5f);
            }
        }

        [Fact] //Тест 4
        public void Filter_LinearSignal_ReturnsExpected()
        {
            float[] data = { 1, 2, 3, 4, 5 };

            float[] result = OfflineFilter.Filter(data, 3);

            AssertSignal.Equal(new float[] { 4f / 3, 2, 3, 4, 14f / 3 }, result);
        }

        [Fact] //Тест 5
        public void Filter_Impulse_ReturnsSpreadImpulse()
        {
            float[] data = { 0, 0, 0, 1, 0, 0, 0 };

            float[] result = OfflineFilter.Filter(data, 3);

            AssertSignal.Equal(new float[] { 0, 0, 1f / 3, 1f / 3, 1f / 3, 0, 0 }, result);
        }

        [Fact] //Тест 6
        public void Filter_WindowSizeOne_ReturnsCopyOfInput()
        {
            float[] data = { 1, 5, -2, 7 };

            float[] result = OfflineFilter.Filter(data, 1);

            AssertSignal.Equal(data, result);
            Assert.NotSame(data, result);
        }

        [Theory] //Тест 7
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(4)]
        public void Filter_InvalidWindowSize_Throws(int windowSize)
        {
            float[] data = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>("windowSize",
                () => OfflineFilter.Filter(data, windowSize));
        }

        [Fact] //Тест 8
        public void Filter_NullData_Throws()
        {
            Assert.Throws<ArgumentNullException>("data",
                () => OfflineFilter.Filter(null, 3));
        }

        [Fact] //Тест 8
        public void Filter_EmptyData_ReturnsEmpty()
        {
            float[] data = { };

            float[] result = OfflineFilter.Filter(data, 3);

            Assert.Empty(result);
        }

        [Fact] //Тест 9
        public void Filter_InputData_NotChanged()
        {
            float[] data = { 1, 2, 3, 4, 5 };
            float[] copy = { 1, 2, 3, 4, 5 };

            OfflineFilter.Filter(data, 3);

            AssertSignal.Equal(copy, data);
        }
    }
}
