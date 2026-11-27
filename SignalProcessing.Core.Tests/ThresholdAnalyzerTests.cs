using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class ThresholdAnalyzerTests
    {
        public static IEnumerable<object[]> NoCrossingData = new List<object[]>
        {
            new object[] { new float[0] },
            new object[] { new float[] { 1 } },
            new object[] { new float[] { 1, 2, 3 } }
        };

        public static IEnumerable<object[]> InvalidSampleData = new List<object[]>
        {
            new object[] { new float[] { -1, 1, float.NaN } },
            new object[] { new float[] { float.PositiveInfinity, 1, 2 } },
            new object[] { new float[] { -1, float.NegativeInfinity, 1 } }
        };

        [Fact] //Тест 6
        public void Analyze_NoSubscribers_ReturnsMarkers()
        {
            float[] data = new float[] { -1, 1, 2, -1, -2, 1 };
            float[] copy = (float[])data.Clone();
            SingleChannelSignal signal = new SingleChannelSignal(data, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();

            SignalAnalysisResult result = analyzer.Analyze(signal, 0);

            Assert.Equal(3, result.Markers.Count);
            Assert.Equal(1, result.Markers[0].SampleIndex);
            Assert.Equal(3, result.Markers[1].SampleIndex);
            Assert.Equal(5, result.Markers[2].SampleIndex);
            Assert.Equal(CrossingDirection.Up, result.Markers[0].Direction);
            Assert.Equal(CrossingDirection.Down, result.Markers[1].Direction);
            Assert.Equal(CrossingDirection.Up, result.Markers[2].Direction);
            Assert.Same(signal, result.Signal);
            Assert.Equal(copy, data);
        }

        [Fact] //Тест 7
        public void Analyze_ThresholdPlateau_ReturnsTwoMarkers()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { -1, 0, 0, -1 }, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            int count = 0;
            analyzer.MarkerDetected += (sender, e) => count++;

            SignalAnalysisResult result = analyzer.Analyze(signal, 0);

            Assert.Equal(2, count);
            Assert.Equal(2, result.Markers.Count);
            Assert.Equal(1, result.Markers[0].SampleIndex);
            Assert.Equal(CrossingDirection.Up, result.Markers[0].Direction);
            Assert.Equal(3, result.Markers[1].SampleIndex);
            Assert.Equal(CrossingDirection.Down, result.Markers[1].Direction);
        }

        [Theory] //Тест 8
        [MemberData(nameof(NoCrossingData))]
        public void Analyze_NoCrossings_ReturnsEmptyResult(float[] data)
        {
            SingleChannelSignal signal = new SingleChannelSignal(data, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            int count = 0;
            analyzer.MarkerDetected += (sender, e) => count++;

            SignalAnalysisResult result = analyzer.Analyze(signal, 0);

            Assert.Empty(result.Markers);
            Assert.Equal(0, count);
        }

        [Fact] //Тест 9
        public void Analyze_TwoSubscribersThenUnsubscribe_OnlyFirstGetsSecondEvent()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { -1, 1 }, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            int firstCount = 0;
            int secondCount = 0;
            object savedSender = null;
            MarkerDetectedEventArgs savedArgs = null;
            EventHandler<MarkerDetectedEventArgs> first = (sender, e) =>
            {
                firstCount++;
                savedSender = sender;
                savedArgs = e;
            };
            EventHandler<MarkerDetectedEventArgs> second = (sender, e) => secondCount++;
            analyzer.MarkerDetected += first;
            analyzer.MarkerDetected += second;

            SignalAnalysisResult result1 = analyzer.Analyze(signal, 0);

            Assert.Equal(1, firstCount);
            Assert.Equal(1, secondCount);
            Assert.Single(result1.Markers);
            Assert.Same(analyzer, savedSender);
            Assert.Same(signal, savedArgs.Signal);
            Assert.Same(result1.Markers[0], savedArgs.Marker);

            analyzer.MarkerDetected -= second;
            SignalAnalysisResult result2 = analyzer.Analyze(signal, 0);

            Assert.Equal(2, firstCount);
            Assert.Equal(1, secondCount);
            Assert.Single(result2.Markers);
        }

        [Fact] //Тест 10
        public void Analyze_NullSignal_Throws()
        {
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            int count = 0;
            analyzer.MarkerDetected += (sender, e) => count++;

            Assert.Throws<ArgumentNullException>("signal",
                () => analyzer.Analyze(null, 0));
            Assert.Equal(0, count);
        }

        [Theory] //Тест 10
        [InlineData(float.NaN)]
        [InlineData(float.PositiveInfinity)]
        [InlineData(float.NegativeInfinity)]
        public void Analyze_InvalidThreshold_Throws(float threshold)
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { -1, 1 }, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            int count = 0;
            analyzer.MarkerDetected += (sender, e) => count++;

            Assert.Throws<ArgumentOutOfRangeException>("threshold",
                () => analyzer.Analyze(signal, threshold));
            Assert.Equal(0, count);
        }

        [Theory] //Тест 11
        [MemberData(nameof(InvalidSampleData))]
        public void Analyze_SignalWithNaNOrInfinity_Throws(float[] data)
        {
            SingleChannelSignal signal = new SingleChannelSignal(data, 10);
            ThresholdAnalyzer analyzer = new ThresholdAnalyzer();
            int count = 0;
            analyzer.MarkerDetected += (sender, e) => count++;

            Assert.Throws<ArgumentException>("signal",
                () => analyzer.Analyze(signal, 0));
            Assert.Equal(0, count);
        }
    }
}
