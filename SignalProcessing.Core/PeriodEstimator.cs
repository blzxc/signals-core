using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class PeriodEstimator
    {
        private int _upCount;
        private float _firstUpTime;
        private float _lastUpTime;

        public float Period
        {
            get
            {
                if (_upCount < 2)
                {
                    return float.NaN;
                }
                return (_lastUpTime - _firstUpTime) / (_upCount - 1);
            }
        }

        public void OnMarkerDetected(object sender, MarkerDetectedEventArgs e)
        {
            if (e.Marker.Direction != CrossingDirection.Up)
            {
                return;
            }
            if (_upCount == 0)
            {
                _firstUpTime = e.Time;
            }
            _lastUpTime = e.Time;
            _upCount++;
        }
        public void Reset()
        {
            _upCount = 0;
            _firstUpTime = 0;
            _lastUpTime = 0;
        }
    }
}
