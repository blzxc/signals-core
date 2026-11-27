using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SignalOperationsTests
    {
        [Fact] //Тест 20
        public void Sum_DifferentLength_ReturnsSumWithZeros()
        {
            SingleChannelSignal first = new SingleChannelSignal(new float[] { 1, 2, 3 }, 100);
            SingleChannelSignal second = new SingleChannelSignal(new float[] { 10, 20 }, 100);

            SingleChannelSignal result = SignalOperations.Sum(new[] { first, second });

            AssertSignal.Equal(new float[] { 11, 22, 3 }, result.Data);
            Assert.Equal(100f, result.Frequency, 1e-5f);
        }

        [Fact] //Тест 21
        public void Concat_TwoSignals_ReturnsJoinedSignal()
        {
            SingleChannelSignal first = new SingleChannelSignal(new float[] { 1, 2 }, 100);
            SingleChannelSignal second = new SingleChannelSignal(new float[] { 3 }, 100);

            SingleChannelSignal result = SignalOperations.Concat(new[] { first, second });

            AssertSignal.Equal(new float[] { 1, 2, 3 }, result.Data);
            Assert.Equal(100f, result.Frequency, 1e-5f);
        }

        [Fact] //Тест 22
        public void Sum_DifferentFrequencies_Throws()
        {
            SingleChannelSignal first = new SingleChannelSignal(new float[] { 1, 2 }, 100);
            SingleChannelSignal second = new SingleChannelSignal(new float[] { 3, 4 }, 200);

            Assert.Throws<ArgumentException>(() => SignalOperations.Sum(new[] { first, second }));
        }

        [Fact] //Тест 22
        public void Concat_DifferentFrequencies_Throws()
        {
            SingleChannelSignal first = new SingleChannelSignal(new float[] { 1, 2 }, 100);
            SingleChannelSignal second = new SingleChannelSignal(new float[] { 3, 4 }, 200);

            Assert.Throws<ArgumentException>(() => SignalOperations.Concat(new[] { first, second }));
        }

        [Fact] //Тест 23
        public void Sum_EmptyList_Throws()
        {
            List<SingleChannelSignal> signals = new List<SingleChannelSignal>();

            Assert.Throws<ArgumentException>(() => SignalOperations.Sum(signals));
        }

        [Fact] //Тест 23
        public void Sum_NullList_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => SignalOperations.Sum(null));
        }

        [Fact] //Тест 23
        public void Sum_ListWithNull_Throws()
        {
            SingleChannelSignal first = new SingleChannelSignal(new float[] { 1, 2 }, 100);

            Assert.Throws<ArgumentNullException>(() => SignalOperations.Sum(new[] { first, null }));
        }

        [Fact] //Тест 24
        public void Sum_SourceSignals_NotChanged()
        {
            SingleChannelSignal first = new SingleChannelSignal(new float[] { 1, 2, 3 }, 100);
            SingleChannelSignal second = new SingleChannelSignal(new float[] { 10, 20 }, 100);

            SignalOperations.Sum(new[] { first, second });

            AssertSignal.Equal(new float[] { 1, 2, 3 }, first.Data);
            AssertSignal.Equal(new float[] { 10, 20 }, second.Data);
        }
    }
}
