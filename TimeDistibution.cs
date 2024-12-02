using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class TimeDistibution
    {
        int seed = 123;

        Random _random;
        public TimeDistibution()
        {
            _random = new Random(123);
        }
        public double GetExpTime(double parameter)
        {
            return - Math.Log(_random.NextDouble())/parameter;
        }

        public double GetGammaTime(double shape, double scale)
        {
            int ParamInt = (int)shape;
            double ParamFrac = shape - ParamInt;
            double result = 0;

            for (int i = 1; i <= ParamInt; i++)
            {
                double a = _random.NextDouble();
                while (a == 0) a = _random.NextDouble();
                result -= Math.Log(a);
            }

            if (ParamFrac > 0)
            {
                double a = _random.NextDouble();
                while (a == 0) a = _random.NextDouble();
                result += NextValueMinus1(1 - ParamFrac, ParamFrac) * Math.Log(a);
            }
            return result / scale;
        }

        public double NextValueMinus1(double Alpha, double Beta)
        {
            double a = Math.Pow(_random.NextDouble(), 1 / Alpha);
            double b = Math.Pow(_random.NextDouble(), 1 / Beta);
            while (a + b > 1)
            {
                a = Math.Pow(_random.NextDouble(), 1 / Alpha);
                b = Math.Pow(_random.NextDouble(), 1 / Beta);
            }
            return -b / (a + b);
        }
    }
}
