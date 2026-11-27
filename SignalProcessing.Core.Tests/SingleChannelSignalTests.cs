using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SingleChannelSignalTests
    {
        //--------------Тест 1
        [Fact]
        public void Constructor_NullData_Throws()
        {
            Assert.Throws<ArgumentNullException>("data",
                () => new SingleChannelSignal(null, 100));
        }

        //--------------Тест 2
        [Theory]
        [InlineData(0f)]
        [InlineData(-1f)]
        public void Constructor_NotPositiveFrequency_Throws(float frequency)
        {
            float[] data = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>("frequency",
                () => new SingleChannelSignal(data, frequency));
        }
    }
}
