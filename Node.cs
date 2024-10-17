using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Node
    {
        List<Request> requests = new List<Request>();
        public int currentNumberTask = 0;
        public Request currentTask { get; set; } = null!;
        public int id;
        public Node(int id)
            {
            this.id = id;
        }

        public double FindCurrentTask()
        {
            currentTask = requests.MinBy(p => p.time);
            return currentTask.time;
        }

        public void AddNewTask(double[] transitMatrix)
            {
            requests.Add(new Request(transitMatrix, this.id));
            currentNumberTask++;
        }

        public void AddTask(Request task, double[] transitionMatrix)
        {
            task.NextState(transitionMatrix, this.id);
            requests.Add(task);
            currentNumberTask++;
            double t = FindCurrentTask();
        }
        public void RemoveTask() 
        {
            requests.Remove(currentTask);
            currentNumberTask--;
            if (currentNumberTask > 0)
            {
                double t = FindCurrentTask();
            }
        }
    }
    

}
