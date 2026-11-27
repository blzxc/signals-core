using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class MarkerDetectedEventArgsTests
    {
        [Fact] //Тест 5
        public void Constructor_Marker2Down_ReturnsTimeAndValue()
        {
            SingleChannelSignal signal = new SingleChannelSignal(new float[] { 0, 2, -3 }, 10);
            SignalMarker marker = new SignalMarker(2, CrossingDirection.Down);

            MarkerDetectedEventArgs args = new MarkerDetectedEventArgs(signal, marker);

            Assert.Equal(0.2, args.Time, 1e-6);
            Assert.Equal(-3, args.Value, 1e-6);
        }
    }
}
