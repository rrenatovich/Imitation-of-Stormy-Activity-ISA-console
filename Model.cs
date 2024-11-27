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
        private double[][] matrixTransit;
        public double time = 0;
        public Statistics statistics;
        public int done = 0;
        private List<Request>[] nodes;

        public Model()
        {
            matrixTransit = new double[numberOfNodes + 1][];
            matrixTransit[0] = new double[] { 0, 0.3, 0.7, };
            matrixTransit[1] = new double[] { 1, 0, 1.8, };
            matrixTransit[2] = new double[] { 0.1, 1, 0, };

            statistics = new Statistics(numberOfNodes);
            nodes = new List<Request>[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++) {
                nodes[i] = new List<Request> { };
            }

            Console.WriteLine($"Init array of requests in nodes -- {nodes.Length}");
    }

        public void GetInputTask()
        {
            var random = new Random();

            double p = random.NextDouble();
            for (int i = 1; i < matrixTransit.Length; i++)
            {
                p -= matrixTransit[0][i];

                if (p <= 0)
                {
                    nodes[i - 1].Add(new Request(matrixTransit[i], i));
                    /*Console.WriteLine(i);*/
                    break;
                }
            }
        }

       public Request FindCurrentTask()
        {
            
            for (int j = 0; j < nodes.Length; j++)
            {
                if (nodes[j].Count > 0) {
                    currentTask = nodes[j][0];
                    break;
                }
              
            }
            for (int i = 0;i < nodes.Length; i++)
            {
                if (nodes[i].Contains(currentTask))
                {
                    Request min = nodes[i].MinBy(p => p.time);
                    /*Console.WriteLine($"{min.time} {min.transitionWay}");*/
                    if (min.time < currentTask.time)
                    {
                        currentTask = min;
                    }

                }
               
            }
            return currentTask;
        }

        private double GetInputTime()
        {
            var random = new Random();
            return -Math.Log(random.NextDouble()) / 10; 
        }

        private void UpdateTime(double time) 
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                foreach (var request in nodes[i])
                {
                    if (request.time < time)    // РЕДКОСТНОГО ГОВНА КОСТЫЛЬ 
                    {
                        /*Console.WriteLine($"{time}, {request.time}");*/
                        request.time = 0;
                    }
                    else 
                    { request.time -= time; }
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
            UpdateTime(temp.time);
            temp.timeDone -= temp.time;
            if (temp.transitionWay != -1 && temp.transitionWay != 0)
            {
                nodes[temp.id - 1].Add(temp);
                temp.id = temp.transitionWay;
                /*Console.Write(temp.id);*/
                temp.NextState(matrixTransit[temp.id]);
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
                serviceTime = currentTask.time;
            }

            /*Console.WriteLine($"service time = {serviceTime}");
            Console.WriteLine($"arrivalTime = {arrivalTime}");*/
            for (int i = 0; i < nodes.Length; i++)
            {
                statistics.WriteState(i, Math.Min(serviceTime, arrivalTime), nodes[i].Count);
            }

            /*if (Math.Min(serviceTime, arrivalTime) < 0)
            {
                Console.WriteLine("ERROR");
            }*/
            if (serviceTime < arrivalTime)
            {
                time += serviceTime;
                ServiceRequest();
            }
            else 
            {
                time += arrivalTime;
                UpdateTime(arrivalTime);
                GetInputTask();
            }
            


            


        }


        public void GetInfo()
        {
           
        }

    }
}
