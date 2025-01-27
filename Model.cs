using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Model
    {
        TimeDistibution td = new TimeDistibution();
        InputFlow _inputFlow;
       
        Event currentTask;
        private int numberOfNodes = 3;
        private Dictionary<int, double>[] matrixTransit;
        public double modelTime = 0;
        public Statistics statistics;
        
       
        Random random = new Random();
        double minTime = 0;
        Dictionary<int, int> events_stat = new Dictionary<int, int>()
            {
                {1, 0 },
                {2, 0},
                {3, 0}

            };

        List<Event> events = new List<Event>();
        public Model()
        {
            matrixTransit = new Dictionary<int, double>[numberOfNodes+1];
            // вероятности переходов
            matrixTransit[0] = new Dictionary<int, double>()    
            {
                {1, 0.4},
                {2, 0.3 },
                {3, 0.3}
            };

            // интенсивности переходов 
            matrixTransit[1] = new Dictionary<int, double>()
            {
                {0, 0.1},
                {2, 1 },
                {3, 1 }
            };
            matrixTransit[2] = new Dictionary<int, double>()
            {
                {0, 0.1},
                {1, 1},
                {3, 1 }
            };
            matrixTransit[3] = new Dictionary<int, double>()
            {
                {0, 0.1},
                {1, 1},
                {2, 1 }
            };

            statistics = new Statistics(numberOfNodes);

            _inputFlow = new InputFlow(100, td);
            _inputFlow.GetTime(modelTime);
        }

        public void MainCycle()
        {
            double eventTime = 999999;
            int numberOfEvents = events_stat.Sum(x => x.Value);
            if (numberOfEvents > 0) 
            { 
                currentTask = events.MinBy(p => p.currentTime);
                eventTime = currentTask.currentTime;
            }
            minTime = Math.Min(_inputFlow.time, eventTime);
            for (int i = 0; i < numberOfNodes; i++)
            {
                statistics.WriteState(i, minTime-modelTime, events_stat[i + 1]);
            }
            modelTime = minTime;
            if (modelTime == eventTime)
            {
                GetEvent();
               
            }
            else
            {
                GetInputEvent();
               
            }
        }

        public void GetInputEvent()
        {
            double p = random.NextDouble();
            foreach (var prop in matrixTransit[0])
            {
                p -= prop.Value;
                if (p <= 0)
                {
                    events.Add(new Event(matrixTransit[prop.Key], prop.Key, td, modelTime));
                    events_stat[prop.Key]++;
                    break;
                }
            }
            _inputFlow.GetTime(modelTime);
        }

        public void GetEvent()
        {
            events_stat[currentTask.id]--; 
            if (currentTask.transitionWay > 0)
            {
                currentTask.id = currentTask.transitionWay;
                events_stat[currentTask.id]++;
                currentTask.NextState(matrixTransit[currentTask.id], td, modelTime);
            }
            else
            {
                events.Remove(currentTask);
            }
        } 
    }
}
