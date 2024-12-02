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
  
        public Request(Dictionary<int, double> transitMatrix, int id, TimeDistibution td)
        {
            this.id = id;
            
            tTime = new Dictionary<int, double>();

            foreach (var value in transitMatrix)
            {
                tTime[value.Key] = td.GetExpTime(value.Value);
            }
            timeDone =td.GetGammaTime(1, 0.5);
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
       
        public void NextState(Dictionary<int, double> transitMatrix, TimeDistibution td)
        {

            foreach (var value in transitMatrix)
            {
                tTime[value.Key] = td.GetExpTime(value.Value);
            }

            
            currentTime = timeDone;
            transitionWay = -1;
            foreach (var value in tTime)
            {

                if (value.Value < currentTime)
                {
                    currentTime = value.Value;
                    transitionWay = value.Key;
                }

               
            }

            
        }
    }
}
