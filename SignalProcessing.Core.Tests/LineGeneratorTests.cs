using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class LineGeneratorTests
    {
        [Fact] //Тест 8
        public void Generate_K2B1_ReturnsLineValues()
        {
            LineGenerator generator = new LineGenerator(2, 1);
            SamplingParameters sampling = new SamplingParameters(10, 2);

            SingleChannelSignal signal = generator.Generate(sampling);

            float[] expected = new float[sampling.SampleCount];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = 2 * sampling.TimeAt(i) + 1;
            }
            AssertSignal.Equal(expected, signal.Data);
        }
    }
}
