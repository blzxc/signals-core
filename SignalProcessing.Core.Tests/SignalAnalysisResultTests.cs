using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SignalAnalysisResultTests
    {
        [Fact] //Тест 3
        public void Constructor_NullSignal_Throws()
        {
            Assert.Throws<ArgumentNullException>("signal",
                () => new SignalAnalysisResult(null, new List<SignalMarker>()));
        }

        [Fact] //Тест 3
        public void Constructor_NullMarkers_Throws()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 1, 2, 3 }, 10);

            Assert.Throws<ArgumentNullException>("markers",
                () => new SignalAnalysisResult(signal, null));
        }

        [Fact] //Тест 3
        public void Constructor_MarkerIndexOutOfSignal_Throws()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 1, 2, 3 }, 10);
            List<SignalMarker> markers = new List<SignalMarker>();
            markers.Add(new SignalMarker(3, CrossingDirection.Up));

            Assert.Throws<ArgumentOutOfRangeException>("markers",
                () => new SignalAnalysisResult(signal, markers));
        }

        [Fact] //Тест 4
        public void Constructor_SourceListChangedAfter_MarkersNotChanged()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 1, 2, 3 }, 10);
            SignalMarker first = new SignalMarker(1, CrossingDirection.Up);
            List<SignalMarker> markers = new List<SignalMarker>();
            markers.Add(first);

            SignalAnalysisResult result = new SignalAnalysisResult(signal, markers);
            markers.Add(new SignalMarker(2, CrossingDirection.Down));

            Assert.Single(result.Markers);
            Assert.Same(first, result.Markers[0]);
            Assert.False(result.Markers is List<SignalMarker>);
        }
    }
}
