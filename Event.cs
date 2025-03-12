using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Event
    {
        public double currentTime;
        public double timeDone;
        public int transitionWay;
        public int id;
        public Dictionary<int, double> tTime;
  
        public Event(Dictionary<int, double> transitMatrix, int id, TimeDistibution td, double modelsTime)
        {
            this.id = id;
            timeDone = modelsTime + td.GetGammaTime(0.7, 0.1); // alpha, betha
            transitionWay = -1;
            NextState(transitMatrix, td, modelsTime);
        }
       
        public void NextState(Dictionary<int, double> transitMatrix, TimeDistibution td, double modelsTime)
        {
            currentTime = timeDone;
            transitionWay = -1;
            foreach (var value in transitMatrix)
            {
                double time = modelsTime + td.GetExpTime(value.Value);
                if (time <= currentTime)
                {
                    currentTime = time;
                    transitionWay = value.Key;
                }
               
            }  
            } 
        }
    }

