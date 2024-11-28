using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Request
    {
        public double currentTime;
        public double timeDone;
        public int transitionWay;
        public int id;
        public Dictionary<int, double> tTime;
        Random random = new Random();
        public Request(Dictionary<int, double> transitMatrix, int id)
        {
            this.id = id;
            tTime = new Dictionary<int, double>();

            foreach (var value in transitMatrix)
            {
                tTime[value.Key] = -Math.Log(random.NextDouble()) / value.Value;
            }
            timeDone = -Math.Log(random.NextDouble()) / 1;
                /*GammaDistribution(0.1, 1/2)*/;
            /*-Math.Log(random.NextDouble()) / 1;*/

            currentTime = 999.999;
            transitionWay = 0;
            foreach (var value in tTime) 
            {
                if (value.Value < currentTime)
                {
                    currentTime = value.Value;
                    transitionWay = value.Key;
                }
            }

            if (timeDone < currentTime)
            {
                currentTime = timeDone;
                transitionWay = -1;
            }
            /*else
            {
                time = minmumTransitionTime;
                for (int i = 0; i < transitMatrix.Length; i++)
                {
                    if (time == tTime[i]) { transitionWay = i; }
                }
            }*/

            /*Console.WriteLine($"Текущей узел: {id}. Через {time} заявка перейдет в состояние {transitionWay}");*/
        }
        private double GammaDistribution(double shape, double scale)
        {
            int ParamInt = (int)shape;
            double ParamFrac = shape - ParamInt;
            double result = 0;
            
            for (int i = 1; i <= ParamInt; i++)
            {
                double a = random.NextDouble();
                while (a == 0) a = random.NextDouble();
                result -= Math.Log(a);
            }

            if (ParamFrac > 0)
            {
                double a = random.NextDouble();
                while (a == 0) a = random.NextDouble();
                result += NextValueMinus1(1 - ParamFrac, ParamFrac) * Math.Log(a);
            }
            return result / scale;
        }

        public double NextValueMinus1(double Alpha, double Beta)
        {
            double a = Math.Pow(random.NextDouble(), 1 / Alpha);
            double b = Math.Pow(random.NextDouble(), 1 / Beta);
            while (a + b > 1)
            {
                a = Math.Pow(random.NextDouble(), 1 / Alpha);
                b = Math.Pow(random.NextDouble(), 1 / Beta);
            }
            return -b / (a + b);
        }
        public void NextState(Dictionary<int, double> transitMatrix)
        {

            foreach (var value in transitMatrix)
            {
                tTime[value.Key] = -Math.Log(random.NextDouble()) / value.Value;
            }
            
            transitionWay = 0;
            foreach (var value in tTime)
            {
                if (value.Value < currentTime)
                {
                    currentTime = value.Value;
                    transitionWay = value.Key;
                }
            }

            if (timeDone < currentTime)
            {
                currentTime = timeDone;
                transitionWay = -1;
            }
        }
    }
}
