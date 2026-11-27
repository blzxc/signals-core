using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SawGeneratorTests
    {
        [Fact] //Тест Д.1.1
        public void Generate_Period1Phase0Amplitude1_ReturnsSawValues()
        {
            SawGenerator generator = new SawGenerator(1, 0, 1);

            SingleChannelSignal signal = generator.Generate(new SamplingParameters(4, 1));

            AssertSignal.Equal(new float[] { -1, -0.5f, 0, 0.5f }, signal.Data);
        }

        [Fact] //Тест Д.1.2
        public void Constructor_ZeroPeriod_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>("period",
                () => new SawGenerator(0, 0, 1));
        }
    }
}
