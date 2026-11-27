using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SignalFileTests : IDisposable
    {
        private readonly string _filePath;

        public SignalFileTests()
        {
            // выполняется перед каждым тестом
            _filePath = Path.GetTempFileName();
        }

        public void Dispose()
        {
            // выполняется после каждого теста
            File.Delete(_filePath);
        }

        [Fact] //Тест 15
        public void WriteAndRead_Signal_ReturnsSameSignal()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 0.5f, -1.25f, 3f, 0.1f }, 250);
            SingleChannelSignalWriter.Write(signal, _filePath);

            SingleChannelSignal result = SingleChannelSignalReader.Read(_filePath);

            Assert.Equal(250f, result.Frequency, 1e-5f);
            AssertSignal.Equal(signal.Data, result.Data);
        }

        [Fact] //Тест 16
        public void Write_RussianCulture_UsesDot()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 0.5f, -1.25f, 3f }, 250);

            CultureInfo previous = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            try
            {
                SingleChannelSignalWriter.Write(signal, _filePath);
                string[] lines = File.ReadAllLines(_filePath);
                Assert.Equal(new[] { "250", "0.5", "-1.25", "3" }, lines);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = previous;
            }
        }

        [Fact] //Тест 17
        public void Read_RussianCulture_ParsesDot()
        {
            File.WriteAllLines(_filePath, new[] { "100", "0.5", "-1.5" });

            CultureInfo previous = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            try
            {
                SingleChannelSignal result = SingleChannelSignalReader.Read(_filePath);
                Assert.Equal(100f, result.Frequency, 1e-5f);
                AssertSignal.Equal(new float[] { 0.5f, -1.5f }, result.Data);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = previous;
            }
        }

        [Fact] //Тест 18
        public void Read_OnlyFrequency_ReturnsEmptySignal()
        {
            File.WriteAllLines(_filePath, new[] { "250" });

            SingleChannelSignal result = SingleChannelSignalReader.Read(_filePath);

            Assert.Equal(250f, result.Frequency, 1e-5f);
            Assert.Empty(result.Data);
        }

        [Fact] //Тест 19
        public void Read_EmptyFile_Throws()
        {
            Assert.Throws<InvalidDataException>(() => SingleChannelSignalReader.Read(_filePath));
        }

        [Fact] //Тест 19
        public void Read_NotNumberValue_Throws()
        {
            File.WriteAllLines(_filePath, new[] { "100", "1", "abc", "2" });

            Assert.Throws<InvalidDataException>(() => SingleChannelSignalReader.Read(_filePath));
        }

        [Fact] //Тест 19
        public void Read_ZeroFrequency_Throws()
        {
            File.WriteAllLines(_filePath, new[] { "0", "1", "2" });

            Assert.Throws<InvalidDataException>(() => SingleChannelSignalReader.Read(_filePath));
        }
    }
}
