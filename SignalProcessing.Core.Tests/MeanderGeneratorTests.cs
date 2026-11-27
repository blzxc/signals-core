using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class MeanderGeneratorTests
    {
        [Fact] //Тест Д.3.1
        public void Generate_Period1DutyCycleHalf_ReturnsMeanderValues()
        {
            MeanderGenerator generator = new MeanderGenerator(1, 0, 1, 0.5f);

            SingleChannelSignal signal = generator.Generate(new SamplingParameters(4, 1));

            AssertSignal.Equal(new float[] { 1, 1, -1, -1 }, signal.Data);
        }

        [Theory] //Тест Д.3.2
        [InlineData(0f)]
        [InlineData(1f)]
        public void Constructor_InvalidDutyCycle_Throws(float dutyCycle)
        {
            Assert.Throws<ArgumentOutOfRangeException>("dutyCycle",
                () => new MeanderGenerator(1, 0, 1, dutyCycle));
        }
    }
}
