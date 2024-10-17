using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Model
    {
        private int numberOfNodes = 2;
        List<Node> nodes;
        private double[][] matrixTransit;
        private double time = 0;

        public Model()
        {
            nodes = new List<Node>();
            for (int i = 0; i < numberOfNodes; i++)
        {
                nodes.Add(new Node(i + 1));
            }
            matrixTransit = new double[numberOfNodes + 1][];
            matrixTransit[0] = new double[] { 0, 0.5, 0.5, };
            matrixTransit[1] = new double[] { 0.01, 0.0001, 10, };
            matrixTransit[2] = new double[] { 0.01, 0.5, 0.000001, };
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
                    nodes[i - 1].AddNewTask(matrixTransit[i]);
                    /*Console.WriteLine(i);*/
                    break;
                }
            }
        }

        public void GetTransition(Node nodeOut, Node nodeIn)
        {
            nodeIn.AddTask(nodeOut.currentTask, matrixTransit[nodeIn.id]);
            nodeOut.RemoveTask();
        }

        public void GetOut(Node node)
        {
            node.RemoveTask();
        }

        public void MainCycle() 
        {
            int numberOfTasks = 0;
            double minimumTime = 999.999;
            int curNodeId = 0;
            foreach(Node node in nodes) 
            {
                numberOfTasks += node.currentNumberTask;
                if (node.currentNumberTask > 0)
                {
                    double t = node.FindCurrentTask();
                    if (t <= minimumTime)
                    {
                        minimumTime = t;
                        curNodeId = node.id;
                    }
                }

            }
            var random = new Random();
            if (numberOfTasks == 0)
            {
                double a = -Math.Log(random.NextDouble()) / 10;

                time += a;
                GetInputTask();
            }
            else 
            {
                double a = -Math.Log(random.NextDouble()) / 10;
                if (a <= minimumTime)
                {
                    minimumTime = a;
                    time += minimumTime;
                    GetInputTask();
                }
                else
                {
                    time += minimumTime;
                    if (nodes[curNodeId - 1].currentTask.transitionWay == 0 || nodes[curNodeId - 1].currentTask.transitionWay == -1)
                    {
                        GetOut(nodes[curNodeId - 1]);
                    }
                    else
                    {
                        GetTransition(nodes[curNodeId - 1], nodes[nodes[curNodeId - 1].currentTask.transitionWay - 1]);
                    }
                }
            }
        }

        public void GetInfo()
        {
            Console.WriteLine($"Model Time: {time}");
            foreach (Node node in nodes)
            {
                
                Console.WriteLine($"Node id: {node.id} -- Number of tasks: {node.currentNumberTask}");
            }
    }

}
}
