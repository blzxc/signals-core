using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class ThresholdAnalyzer
    {
        public event EventHandler<MarkerDetectedEventArgs> MarkerDetected;

        public SignalAnalysisResult Analyze(
            SingleChannelSignal signal, float threshold)
        {
            if (signal == null)
            {
                throw new ArgumentNullException(nameof(signal));
            }
            if (float.IsNaN(threshold) || float.IsInfinity(threshold))
            {
                throw new ArgumentOutOfRangeException(nameof(threshold), "Порог должен быть конечным числом");
            }
            float[] data = signal.Data;
            for (int i = 0; i < data.Length; i++)
            {
                if (float.IsNaN(data[i]) || float.IsInfinity(data[i]))
                {
                    throw new ArgumentException("Сигнал содержит NaN или бесконечность в отсчете " + i, nameof(signal));
                }
            }

            List<SignalMarker> markers = new List<SignalMarker>();
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i - 1] < threshold && data[i] >= threshold)
                {
                    SignalMarker marker = new SignalMarker(i, CrossingDirection.Up);
                    markers.Add(marker);
                    MarkerDetected?.Invoke(this,
                        new MarkerDetectedEventArgs(signal, marker));
                }
                else if (data[i - 1] >= threshold && data[i] < threshold)
                {
                    SignalMarker marker = new SignalMarker(i, CrossingDirection.Down);
                    markers.Add(marker);
                    MarkerDetected?.Invoke(this,
                        new MarkerDetectedEventArgs(signal, marker));
                }
            }
            return new SignalAnalysisResult(signal, markers);
        }
    }
}
