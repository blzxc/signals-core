using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class SignalAnalysisResult
    {
        public SingleChannelSignal Signal { get; }
        public IReadOnlyList<SignalMarker> Markers { get; }

        public SignalAnalysisResult(
            SingleChannelSignal signal, IEnumerable<SignalMarker> markers)
        {
            if (signal == null)
            {
                throw new ArgumentNullException(nameof(signal));
            }
            if (markers == null)
            {
                throw new ArgumentNullException(nameof(markers));
            }
            List<SignalMarker> list = new List<SignalMarker>(markers);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    throw new ArgumentNullException(nameof(markers), "Список маркеров содержит null");
                }
                if (list[i].SampleIndex >= signal.Data.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(markers), "Индекс маркера выходит за пределы сигнала");
                }
            }
            Signal = signal;
            Markers = list.AsReadOnly();
        }
    }
}
