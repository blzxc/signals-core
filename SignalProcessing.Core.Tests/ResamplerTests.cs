using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class ResamplerTests
    {
        [Fact] //Тест 36
        public void Resample_SameFrequency_ReturnsSameData()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 1, 5, -2, 7 }, 10);

            SingleChannelSignal result = Resampler.Resample(signal, 10);

            AssertSignal.Equal(signal.Data, result.Data);
        }

        [Fact] //Тест 37
        public void Resample_DoubleFrequency_ReturnsInterpolated()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 0, 2, 4, 8 }, 1);

            SingleChannelSignal result = Resampler.Resample(signal, 2);

            AssertSignal.Equal(new float[] { 0, 1, 2, 3, 4, 6, 8 }, result.Data);
            Assert.Equal(2f, result.Frequency, 1e-5f);
        }

        [Fact] //Тест 38
        public void Resample_HalfFrequency_ReturnsEveryOther()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 0, 1, 2, 3, 4 }, 2);

            SingleChannelSignal result = Resampler.Resample(signal, 1);

            AssertSignal.Equal(new float[] { 0, 2, 4 }, result.Data);
        }

        [Fact] //Тест 39
        public void Resample_NotIntegerRatio_ReturnsInterpolated()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 0, 3, 6, 9 }, 3);

            SingleChannelSignal result = Resampler.Resample(signal, 2);

            AssertSignal.Equal(new float[] { 0, 4.5f, 9 }, result.Data);
        }

        [Fact] //Тест 40
        public void Resample_Line_StaysSameLine()
        {
            float[] data = new float[100];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 2 * (i / 100f) + 1;
            }
            SingleChannelSignal signal = new SingleChannelSignal(data, 100);

            SingleChannelSignal result = Resampler.Resample(signal, 37);

            float[] expected = new float[37];
            for (int j = 0; j < expected.Length; j++)
            {
                expected[j] = 2 * (j / 37f) + 1;
            }
            AssertSignal.Equal(expected, result.Data, 1e-4f);
        }

        [Fact] //Тест 41
        public void Resample_OneSample_ReturnsSameSample()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 5 }, 10);

            SingleChannelSignal result = Resampler.Resample(signal, 20);

            AssertSignal.Equal(new float[] { 5 }, result.Data);
        }

        [Fact] //Тест 41
        public void Resample_EmptySignal_ReturnsEmpty()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[0], 10);

            SingleChannelSignal result = Resampler.Resample(signal, 20);

            Assert.Empty(result.Data);
            Assert.Equal(20f, result.Frequency, 1e-5f);
        }

        [Theory] //Тест 42
        [InlineData(0f)]
        [InlineData(-1f)]
        public void Resample_NotPositiveFrequency_Throws(float newFrequency)
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 1, 2, 3 }, 10);

            Assert.Throws<ArgumentOutOfRangeException>("newFrequency",
                () => Resampler.Resample(signal, newFrequency));
        }

        [Fact] //Тест 42
        public void Resample_NullSignal_Throws()
        {
            Assert.Throws<ArgumentNullException>("signal",
                () => Resampler.Resample(null, 10));
        }
    }
}
