using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class MarkerDetectedEventArgs : EventArgs
    {
        public SingleChannelSignal Signal { get; }
        public SignalMarker Marker { get; }
        public float Time { get; }      // момент времени отсчёта, с
        public float Value { get; }     // значение сигнала в отсчёте

        public MarkerDetectedEventArgs(
            SingleChannelSignal signal, SignalMarker marker)
        {
            if (signal == null)
            {
                throw new ArgumentNullException(nameof(signal));
            }
            if (marker == null)
            {
                throw new ArgumentNullException(nameof(marker));
            }
            if (marker.SampleIndex >= signal.Data.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(marker), "Индекс маркера выходит за пределы сигнала");
            }
            Signal = signal;
            Marker = marker;
            Time = marker.SampleIndex / signal.Frequency;
            Value = signal.Data[marker.SampleIndex];
        }
    }
}
