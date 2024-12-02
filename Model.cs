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
        /*public StreamWriter writer = new StreamWriter("Test.txt");*/
        Request currentTask;
        private int numberOfNodes = 2;
        private Dictionary<int, double>[] matrixTransit;
        public double modelTime = 0;
        public Statistics statistics;
        public int done = 0;
        private List<Request>[] nodes;
        Random random = new Random();
        double minTime = 0;
        double arrivalTime;
        Dictionary<int, int> reqs_stat = new Dictionary<int, int>()
            {
                {1, 0 },
                {2, 0},

            };

        List<Request> reqs = new List<Request>();
        public Model()
        {
            arrivalTime = GetInputTime();
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

            
            /*Console.WriteLine($"Init array of requests in nodes -- {nodes.Length}");*/
    }

        public void GetInputTask()
        {


            arrivalTime = GetInputTime();
            double p = random.NextDouble();
            foreach (var prop in matrixTransit[0])
            {
                {
                    p -= prop.Value;

                    if (p <= 0)
                    {
                        /*nodes[prop.Key - 1].Add(new Request(matrixTransit[prop.Key], prop.Key, td));*/

                        Request toAddReq = new Request(matrixTransit[prop.Key], prop.Key, td);
                        /*UpdateTime(minTime);*/
                        reqs.Add(toAddReq);
                        reqs_stat[prop.Key]++;
                        break;
                    }
                }
            }
        }

       public Request FindCurrentTask()
        {
            /*double minTime = 666.666;
            for (int j = 0; j < nodes.Length; j++)
            {
                if (nodes[j].Count > 0) {
                    Request min = nodes[j].MinBy(p => p.currentTime);
                    *//*Console.WriteLine($"{min.time} {min.transitionWay}");*//*
                    if (min.currentTime < minTime)
                    {
                        currentTask = min;
                        minTime = min.currentTime;
                    }
                }
              
            }*/

            var r = reqs.MinBy(p => p.currentTime);

            return r;
        }

        private double GetInputTime()
        {
           
            return td.GetExpTime(100); 
        }

        private void UpdateTime(double time) 
        {
            /*for (int i = 0; i < nodes.Length; i++)
            {
                foreach (var request in nodes[i])
                {
                    request.timeDone -= time;
                    request.NextState(matrixTransit[request.id], td);
                }
            }*/

            foreach (Request req in reqs) 
            {
                /*req.currentTime -= time;*/
                req.timeDone -= time;
                req.NextState(matrixTransit[req.id], td);
            }
        }
        private void ServiceRequest()
        {
            
            /*for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Remove(currentTask);
            }*/
            
            if (currentTask.transitionWay >0)
            {
                reqs_stat[currentTask.id] --;
                currentTask.id = currentTask.transitionWay;
                reqs_stat[currentTask.id]++;
                
                currentTask.NextState(matrixTransit[currentTask.id], td);
                
            }
            else { reqs.Remove(currentTask); reqs_stat[currentTask.id]--; }

            /*UpdateTime(minTime);*/

        }
        public void MainCycle()
        {
            /*int currentTasks = 0;
            for (int i = 0; i < nodes.Length; i++) {
                currentTasks += nodes[i].Count;
                
            }*/
            
            
            double serviceTime = 99999999;
            if (reqs.Count > 0) { 
                currentTask = FindCurrentTask();
                serviceTime = currentTask.currentTime;
            }

            minTime = Math.Min(serviceTime, arrivalTime);
            modelTime += Math.Min(serviceTime, arrivalTime);
            for (int i = 0; i < numberOfNodes; i++)
            {
                statistics.WriteState(i, Math.Min(serviceTime, arrivalTime), reqs_stat[i+1]);
            }

            if (serviceTime < arrivalTime)
            {
                
                ServiceRequest();
               /* arrivalTime-= serviceTime;*/
            }
            else 
            {
                GetInputTask();
            }
            UpdateTime(minTime);

        }


        public void GetInfo()
        {
           
        }

    }
}
