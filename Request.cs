using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Request
    {
        public double time;
        public double transitionTime;
        public int transitionWay;
        public Request(double[] transitMatrix, int id)
        {
            var random = new Random();
            for (int i = 0; i < transitMatrix.Length; i++)
            {
                transitMatrix[i] = -Math.Log(random.NextDouble()) / transitMatrix[i];
            }
            transitMatrix[id] = 9999;
            /*time = 0;*/

            double timeDone = -Math.Log(random.NextDouble()) / 1;

            double minmumTransition = transitMatrix.Min();

            if (timeDone < minmumTransition)
            {
                time = timeDone;
                transitionWay = -1;
            }
            else
            {
                time = minmumTransition;
                for (int i = 0; i < transitMatrix.Length; i++)
                {
                    if (time == transitMatrix[i]) { transitionWay = i; }
                }
            }

            /*Console.WriteLine($"Текущей узел: {id}. Через {time} заявка перейдет в состояние {transitionWay}");*/
        }

        public void NextState(double[] transitMatrix, int id)
        {
            var random = new Random();
            for (int i = 0; i < transitMatrix.Length; i++)
            {
                transitMatrix[i] = -Math.Log(random.NextDouble()) / transitMatrix[i];
            }
            transitMatrix[id] = 9999;

            double minmumTransition = transitMatrix.Min();
            if (minmumTransition < time )
            {
                time = minmumTransition;
                for (int i = 0; i < transitMatrix.Length; i++)
                {
                    if (time == transitMatrix[i]) { transitionWay = i; }
                }
            }
        }
    }
}
