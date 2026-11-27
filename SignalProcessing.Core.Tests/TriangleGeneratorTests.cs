using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class TriangleGeneratorTests
    {
        [Fact] //Тест Д.2.1
        public void Generate_Period1Phase0Amplitude1_ReturnsTriangleValues()
        {
            TriangleGenerator generator = new TriangleGenerator(1, 0, 1);

            SingleChannelSignal signal = generator.Generate(new SamplingParameters(4, 1));

            AssertSignal.Equal(new float[] { -1, 0, 1, 0 }, signal.Data);
        }

        [Fact] //Тест Д.2.2
        public void Constructor_ZeroPeriod_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>("period",
                () => new TriangleGenerator(0, 0, 1));
        }
    }
}
