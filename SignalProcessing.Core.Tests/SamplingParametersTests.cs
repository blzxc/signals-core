using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SamplingParametersTests
    {
        [Fact] //Тест 1
        public void Constructor_100HzAnd2Seconds_SampleCountIs200()
        {
            SamplingParameters sampling = new SamplingParameters(100, 2);

            Assert.Equal(200, sampling.SampleCount);
        }

        [Theory] //Тест 2
        [InlineData(0f)]
        [InlineData(-1f)]
        public void Constructor_NotPositiveSamplingRate_Throws(float samplingRate)
        {
            Assert.Throws<ArgumentOutOfRangeException>("samplingRate",
                () => new SamplingParameters(samplingRate, 2));
        }
    }
}
