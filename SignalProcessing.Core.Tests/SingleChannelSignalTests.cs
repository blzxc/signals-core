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
        [Fact] //Тест 1
        public void Constructor_NullData_Throws()
        {
            Assert.Throws<ArgumentNullException>("data",
                () => new SingleChannelSignal(null, 100));
        }

        [Theory] //Тест 2
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
