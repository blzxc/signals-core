using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class NoiseGeneratorTests
    {
        [Fact] //Тест 9
        public void Generate_FixedSeed_ValuesInRangeAndRepeatable()
        {
            float range = 3;
            NoiseGenerator generator = new NoiseGenerator(range, () => new Random(42));
            SamplingParameters sampling = new SamplingParameters(100, 2);

            SingleChannelSignal first = generator.Generate(sampling);
            SingleChannelSignal second = generator.Generate(sampling);

            for (int i = 0; i < first.Data.Length; i++)
            {
                Assert.InRange(first.Data[i], -range, range);
            }
            AssertSignal.Equal(first.Data, second.Data);
        }
    }
}
