using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class ExponentialSmoothingFilter
    {
        private float _alpha;
        private float _last;
        private bool _started;

        public ExponentialSmoothingFilter(float alpha)
        {
            if (alpha <= 0 || alpha > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(alpha), "Коэффициент сглаживания должен быть больше 0 и не больше 1");
            }
            _alpha = alpha;
        }
        public float[] Filter(float[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            float[] newSignal = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                if (!_started)
                {
                    _last = data[i];
                    _started = true;
                }
                else
                {
                    _last = _last + _alpha * (data[i] - _last);
                }
                newSignal[i] = _last;
            }

            return newSignal;
        }
        public void Reset()
        {
            _started = false;
        }
    }
}
