using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Statistics
    {
        private double[][] statistics;
        public Statistics(int numberOfNodes) 
        {
            statistics = new double[numberOfNodes][];
            for (int i = 0; i < numberOfNodes; i++)
            {
                statistics[i] = new double[10] ;
            }
        }

        public void WriteState(int numberOfNode, double time, int currentNumber) 
        {
            /*Console.WriteLine($"Number of node: {numberOfNode}, current number{currentNumber}");*/
            if (currentNumber == statistics[numberOfNode-1].Length) 
            {
                Array.Resize(ref statistics[numberOfNode-1],currentNumber+1);
                statistics[numberOfNode-1][currentNumber] = time;
            }
            else
            {
                statistics[numberOfNode-1][currentNumber] += time;
            }
        }

        public void GetStat(double totalTime)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < statistics.Length; i++) 
            {
                for (int j = 0; j < statistics[i].Length; j++)
                {
                    statistics[i][j] = statistics[i][j]/ totalTime;
                    stringBuilder.AppendLine(string.Join(',',statistics[i][j]));
                }
                stringBuilder.AppendLine(string.Join(',', ' '));
                stringBuilder.AppendLine(string.Join(',', ' '));
                stringBuilder.AppendLine(string.Join(',', ' '));
                stringBuilder.AppendLine(string.Join(',', ' '));
            }
            File.WriteAllText("output.csv", stringBuilder.ToString());

            for (int i = 0; i < statistics.Length; i++)
            {
                Console.WriteLine(' ');
              /*  for (int j = 0; j < statistics[i].Length; j++)
                {
                    Console.WriteLine($"{j} --- {statistics[i][j]}");
                }*/
            }
        }
    }

   
}
