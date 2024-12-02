using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Statistics
    {
        private Dictionary<int, double>[] statistics;
        public Statistics(int numberOfNodes) 
        {
            statistics = new Dictionary<int, double>[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                statistics[i] = new Dictionary<int, double>();
            }
        }

        public void WriteState(int numberOfNode, double time, int currentNumber) 
        {
            /*Console.WriteLine($"Number of node: {numberOfNode}, current number{currentNumber}");*/
            if (statistics[numberOfNode].ContainsKey(currentNumber)) 
            {
                statistics[numberOfNode][currentNumber] += time;
            }
            else
            {
                statistics[numberOfNode][currentNumber]=time ;
            }
        }

        public void GetStat(double totalTime)
        {
            foreach (var stat in statistics)
            {
                double mean = 0;
                double s = 0;
                foreach (var value in stat)
                {
                    mean += value.Key * value.Value / totalTime;
                    s+= value.Value / totalTime;
                    
                }
                Console.WriteLine($"summ = {s}");
                Console.WriteLine($"mean = {mean}");
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < statistics.Length; i++)
            {
                for (int j = 0; j < statistics[i].Count; j++)
                {
                    statistics[i][j] = statistics[i][j] / totalTime;
                    stringBuilder.AppendLine(string.Join(',', statistics[i][j]));
                }
                stringBuilder.AppendLine(string.Join(',', ' '));
                stringBuilder.AppendLine(string.Join(',', ' '));
                stringBuilder.AppendLine(string.Join(',', ' '));
                stringBuilder.AppendLine(string.Join(',', ' '));
            }
            File.WriteAllText("output.csv", stringBuilder.ToString());

         /*   for (int i = 0; i < statistics.Length; i++)
            {
                Console.WriteLine(' ');
                for (int j = 0; j < statistics[i].Count; j++)
                {
                    Console.WriteLine($"{j} --- {statistics[i][j]}");
                }
            }*/
        }
    }

   
}
