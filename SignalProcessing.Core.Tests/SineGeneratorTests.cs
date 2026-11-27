using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SineGeneratorTests
    {
        [Fact] //Тест 7
        public void Generate_Period1Phase0Amplitude1_ReturnsSineValues()
        {
            SineGenerator generator = new SineGenerator(1, 0, 1);

            SingleChannelSignal signal = generator.Generate(new SamplingParameters(100, 1));

            Assert.Equal(0, signal.Data[0], 1e-6);
            Assert.Equal(1, signal.Data[25], 1e-6);
            Assert.Equal(0, signal.Data[50], 1e-6);
        }

        [Fact] //Тест 10
        public void Constructor_ZeroPeriod_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>("period",
                () => new SineGenerator(0, 0, 1));
        }
    }
}
