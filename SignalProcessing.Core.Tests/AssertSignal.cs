using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    internal static class AssertSignal
    {
        static public void Equal(float[] expected, float[] actual, float tolerance = 1e-5f)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.True(Math.Abs(expected[i] - actual[i]) <= tolerance,
                    "Индекс " + i + ": ожидалось " + expected[i] + ", получено " + actual[i]);
            }
        }
    }
}
