using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class SignalMarker
    {
        public int SampleIndex { get; }                  // индекс отсчёта (с 0)
        public CrossingDirection Direction { get; }      // направление

        public SignalMarker(int sampleIndex, CrossingDirection direction)
        {
            if (sampleIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleIndex), "Индекс отсчета не может быть отрицательным");
            }
            if (!Enum.IsDefined(typeof(CrossingDirection), direction))
            {
                throw new ArgumentOutOfRangeException(nameof(direction), "Недопустимое направление пересечения");
            }
            SampleIndex = sampleIndex;
            Direction = direction;
        }
    }
}
