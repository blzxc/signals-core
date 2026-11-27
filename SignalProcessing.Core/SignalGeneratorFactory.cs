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
                case SawParameters saw:
                    return new SawGenerator(saw.Period, saw.Phase, saw.Amplitude);
                case TriangleParameters triangle:
                    return new TriangleGenerator(triangle.Period, triangle.Phase, triangle.Amplitude);
                case MeanderParameters meander:
                    return new MeanderGenerator(meander.Period, meander.Phase, meander.Amplitude, meander.DutyCycle);
                default:
                    throw new NotSupportedException(
                        $"Неподдерживаемый тип: {parameters.GetType().Name}");
            }
        }
    }
}
