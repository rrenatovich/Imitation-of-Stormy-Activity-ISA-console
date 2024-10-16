using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    internal class Node
    {
        List<Request> requests = new List<Request>();
        public int _currentNumberTask = 0;
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

        public void AddTask()
        {
            requests.Add(new Request());
            _currentNumberTask++;
        }
    }
    
}
