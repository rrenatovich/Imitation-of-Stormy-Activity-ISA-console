using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Model
    {
        Request currentTask;
        private int numberOfNodes = 2;
        private Dictionary<int, double>[] matrixTransit;
        public double modelTime = 0;
        public Statistics statistics;
        public int done = 0;
        private List<Request>[] nodes;
        Random random = new Random();
        public Model()
        {
            matrixTransit = new Dictionary<int, double>[numberOfNodes+1];
            // вероятности переходов
            matrixTransit[0] = new Dictionary<int, double>()    
            {
                {1, 0.3},
                {2, 0.7 }
            };

            // интенсивности переходов 
            matrixTransit[1] = new Dictionary<int, double>()
            {
                {0, 1},
                {2, 1.8 }
            };
            matrixTransit[2] = new Dictionary<int, double>()
            {
                {0, 0.1},
                {1, 1}
            };
           
            statistics = new Statistics(numberOfNodes);
            nodes = new List<Request>[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++) {
                nodes[i] = new List<Request> { };
            }

            Console.WriteLine($"Init array of requests in nodes -- {nodes.Length}");
    }

        public void GetInputTask()
        {
            

            double p = random.NextDouble();
            foreach (var prop in matrixTransit[0])
            {
                {
                    p -= prop.Value;

                    if (p <= 0)
                    {
                        nodes[prop.Key - 1].Add(new Request(matrixTransit[prop.Key], prop.Key-1));
                        /*Console.WriteLine(i);*/
                        break;
                    }
                }
            }
        }

       public Request FindCurrentTask()
        {
            double minTime = 666.666;
            for (int j = 0; j < nodes.Length; j++)
            {
                if (nodes[j].Count > 0) {
                    Request min = nodes[j].MinBy(p => p.currentTime);
                    /*Console.WriteLine($"{min.time} {min.transitionWay}");*/
                    if (min.currentTime < minTime)
                    {
                        currentTask = min;
                        minTime = min.currentTime;
                    }
                }
              
            }
            return currentTask;
        }

        private double GetInputTime()
        {
           
            return -Math.Log(random.NextDouble()) / 100; 
        }

        private void UpdateTime(double time) 
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                foreach (var request in nodes[i])
                {
                    request.timeDone -= time;
                    request.NextState(matrixTransit[request.id]);
                }
            }
        }
        private void ServiceRequest()
        {
            Request temp = currentTask;
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Remove(currentTask);
            }
            /*UpdateTime(temp.time);*/
            /*temp.timeDone -= temp.time;*/
            if (temp.transitionWay >0)
            {
                nodes[temp.transitionWay-1].Add(temp);
                temp.id = temp.transitionWay;
                /*Console.Write(temp.id);*/
            }
            
            
        }
        public void MainCycle()
        {
            int currentTasks = 0;
            for (int i = 0; i < nodes.Length; i++) {
                currentTasks += nodes[i].Count;
            }

            /*Console.WriteLine(currentTasks);*/
            double arrivalTime = GetInputTime();
            double serviceTime = 9999.9999;
            if (currentTasks != 0) { 
                var currentTask = FindCurrentTask();
                serviceTime = currentTask.currentTime;
            }

            /*Console.WriteLine($"service time = {serviceTime}");
            Console.WriteLine($"arrivalTime = {arrivalTime}");*/
            modelTime += Math.Min(serviceTime, arrivalTime);
            for (int i = 0; i < nodes.Length; i++)
            {
                statistics.WriteState(i, Math.Min(serviceTime, arrivalTime), nodes[i].Count);
                /*Console.WriteLine($"node {i} -- {nodes[i].Count}");*/
            }

            /*if (Math.Min(serviceTime, arrivalTime) < 0)
            {
                Console.WriteLine("ERROR");
            }*/
            if (serviceTime < arrivalTime)
            {
                
                ServiceRequest();
            }
            else 
            {
                GetInputTask();
            }
            UpdateTime(Math.Min(serviceTime, arrivalTime));






        }


        public void GetInfo()
        {
           
        }

    }
}
