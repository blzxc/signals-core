using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SignalGeneratorFactoryTests
    {
        private sealed class FakeParameters : GeneratorParameters { }

        [Fact] //Тест 3
        public void Create_SineParameters_ReturnsSineGenerator()
        {
            GeneratorParameters parameters = new SineParameters { Period = 1, Phase = 0, Amplitude = 1 };

            ISignalGenerator generator = SignalGeneratorFactory.Create(parameters);

            Assert.True(generator is SineGenerator);
        }

        [Fact] //Тест 4
        public void Create_LineAndNoiseParameters_ReturnsCorrespondingGenerators()
        {
            ISignalGenerator line = SignalGeneratorFactory.Create(new LineParameters { K = 2, B = 1 });
            ISignalGenerator noise = SignalGeneratorFactory.Create(new NoiseParameters { Range = 1 });

            Assert.True(line is LineGenerator);
            Assert.True(noise is NoiseGenerator);
        }

        [Fact] //Тест 5
        public void Create_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>("parameters",
                () => SignalGeneratorFactory.Create(null));
        }

        [Fact] //Тест 6
        public void Create_UnsupportedParameters_Throws()
        {
            NotSupportedException ex = Assert.Throws<NotSupportedException>(
                () => SignalGeneratorFactory.Create(new FakeParameters()));

            Assert.Contains("FakeParameters", ex.Message);
        }
    }
}
