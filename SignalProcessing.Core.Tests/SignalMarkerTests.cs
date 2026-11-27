using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SignalMarkerTests
    {
        [Fact] //Тест 1
        public void Constructor_IndexAndDirection_StoresValues()
        {
            SignalMarker marker = new SignalMarker(3, CrossingDirection.Up);

            Assert.Equal(3, marker.SampleIndex);
            Assert.Equal(CrossingDirection.Up, marker.Direction);
        }

        [Fact] //Тест 2
        public void Constructor_NegativeIndex_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>("sampleIndex",
                () => new SignalMarker(-1, CrossingDirection.Up));
        }

        [Fact] //Тест 2
        public void Constructor_UndefinedDirection_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>("direction",
                () => new SignalMarker(0, (CrossingDirection)42));
        }
    }
}
