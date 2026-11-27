using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class PeriodEstimatorTests
    {
        [Fact] //Тест Д.2.1
        public void Period_ThreeUpCrossings_ReturnsAverageInterval()
        {
            SingleChannelSignal signal = new SingleChannelSignal(
                new float[] { -1, 1, -1, -1, -1, 1, -1, -1, -1, 1 }, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            PeriodEstimator estimator = new PeriodEstimator();
            analyzer.MarkerDetected += estimator.OnMarkerDetected;

            analyzer.Analyze(signal, 0);

            Assert.Equal(0.4, estimator.Period, 1e-6);
        }

        [Fact] //Тест Д.2.2
        public void Period_OneUpCrossingOrReset_IsNaN()
        {
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            PeriodEstimator estimator = new PeriodEstimator();
            analyzer.MarkerDetected += estimator.OnMarkerDetected;

            analyzer.Analyze(new SingleChannelSignal(new float[] { -1, 1 }, 10), 0);
            Assert.True(float.IsNaN(estimator.Period));

            analyzer.Analyze(new SingleChannelSignal(new float[] { -1, 1, -1, 1 }, 10), 0);
            estimator.Reset();
            Assert.True(float.IsNaN(estimator.Period));
        }
    }
}
