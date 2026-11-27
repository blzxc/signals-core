using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class SignalGeneratorFactory
    {
        static public ISignalGenerator Create(GeneratorParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            switch (parameters)
            {
                case SineParameters sine:
                    return new SineGenerator(sine.Period, sine.Phase, sine.Amplitude);
                case LineParameters line:
                    return new LineGenerator(line.K, line.B);
                case NoiseParameters noise:
                    return new NoiseGenerator(noise.Range);
                default:
                    throw new NotSupportedException(
                        $"Неподдерживаемый тип: {parameters.GetType().Name}");
            }
        }
    }
}
