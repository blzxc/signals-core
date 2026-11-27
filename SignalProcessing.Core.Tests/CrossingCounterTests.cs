using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class CrossingCounterTests
    {
        [Fact] //Тест Д.1.1
        public void OnMarkerDetected_KnownCrossings_CountsUpAndDown()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { -1, 1, -1, 1 }, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            CrossingCounter counter = new CrossingCounter();
            analyzer.MarkerDetected += counter.OnMarkerDetected;

            analyzer.Analyze(signal, 0);

            Assert.Equal(2, counter.UpCount);
            Assert.Equal(1, counter.DownCount);
        }

        [Fact] //Тест Д.1.2
        public void Reset_AfterAnalyze_CountsAreZero()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { -1, 1, -1, 1 }, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            CrossingCounter counter = new CrossingCounter();
            analyzer.MarkerDetected += counter.OnMarkerDetected;
            analyzer.Analyze(signal, 0);

            counter.Reset();

            Assert.Equal(0, counter.UpCount);
            Assert.Equal(0, counter.DownCount);
        }
    }
}
