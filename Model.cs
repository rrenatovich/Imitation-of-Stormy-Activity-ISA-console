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
        private int numberOfNodes = 2;
        private double[][] matrixTransit;
        public double time = 0;
        public Statistics statistics;
        public int done = 0;
        private int[] nodes;

        public Model()
        {
            matrixTransit = new double[numberOfNodes + 1][];
            matrixTransit[0] = new double[] { 0, 0.3, 0.7, };
            matrixTransit[1] = new double[] { 1, 0.0001, 1.8, };
            matrixTransit[2] = new double[] { 0.1, 1, 0.000001, };

            statistics = new Statistics(numberOfNodes);
            nodes = new int[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++) { nodes[i] = 0; }

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
                    nodes[i - 1]+=1;
                    /*Console.WriteLine(i);*/
                    break;
                }
            }
        }

       

        

        public void MainCycle()
        {
            
        }
       /* public void GeStat()
        {
            statistics.GetStat(time);
        }*/
        public void GetInfo()
        {
           
        }

    }
}
