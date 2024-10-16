using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
<<<<<<< HEAD
    internal class Model
    {
        private int numberOfNodes = 2;
        List<Node> nodes;
        private double[][] matrixTransit;

        public Model()
        {
            nodes = new List<Node>();
            for (int i = 0; i < numberOfNodes; i++)
            {
                nodes.Add(new Node(i+1));
            }
            matrixTransit = new double[numberOfNodes + 1][];
            matrixTransit[0] = new double[] { 0, 0.5, 0.5, };
            matrixTransit[1] = new double[] { 0.5, 0.0001, 0.5, };
            matrixTransit[2] = new double[] { 0.5, 0.5, 0.000001, };
        }

        public void GetInputTask()
        {
            var random = new Random();
            double a = -Math.Log(random.NextDouble()) / 2;

            double p = random.NextDouble();
            for (int i = 1; i < matrixTransit.Length; i++)
            {
                p -= matrixTransit[0][i];

                if (p <= 0)
                {
                    nodes[i-1].AddTask();
                    /*Console.WriteLine(i);*/
                    break;
                }
            }
        }

        public void GetInfo() 
        {
            for (int i = 0;i < numberOfNodes; i++)
            {
                Console.WriteLine(nodes[i]._currentNumberTask);
            }
        }
=======
    internal class Node
    {
        List<Request> requests { get; set; } = null!;
        private int _currentNumberTask = 0;
        Request currentTask { get; set; } = null!;
        public int id;
        public Node(int id)
        {
            this.id = id;
        }

        public Request FindCurrentTask()
        {
            currentTask = requests.MinBy(p => p.time);
            return currentTask;
        }

>>>>>>> c317897cee21611c9fca54d2e08a13b360d4bd02
    }

}
