using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class CrossingCounter
    {
        public int UpCount { get; private set; }
        public int DownCount { get; private set; }

        public void OnMarkerDetected(object sender, MarkerDetectedEventArgs e)
        {
            if (e.Marker.Direction == CrossingDirection.Up)
            {
                UpCount++;
            }
            else
            {
                DownCount++;
            }
        }
        public void Reset()
        {
            UpCount = 0;
            DownCount = 0;
        }
    }
}
