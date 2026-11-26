using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class ThresholdCutter
    {
        private SingleChannelSignal _signal;

        
        public void SetSignal(SingleChannelSignal signal)
        {
            _signal = signal;

        }
        public SingleChannelSignal GetSignal()
        {
            return _signal;
        }
        public void CutUp(float upThr)
        {
            for(int i = 0; i < _signal.Value.Count(); i++)
            {
                if (_signal.Value[i] > upThr) {
                    _signal.Value[i] = upThr;
                }
            }
        }
        public void CutDown(float downThr)
        {
            for (int i = 0; i < _signal.Value.Count(); i++)
            {
                if (_signal.Value[i] < downThr)
                {
                    _signal.Value[i] = downThr;
                }
            }
        }
        public void CutUpAndDown(float upThr, float downThr)
        {
            for (int i = 0; i < _signal.Value.Count(); i++)
            {
                if (_signal.Value[i] > upThr)
                {
                    _signal.Value[i] = upThr;
                }
                else if (_signal.Value[i] < downThr)
                {
                    _signal.Value[i] = downThr;
                }
            }
        }
    }
}
