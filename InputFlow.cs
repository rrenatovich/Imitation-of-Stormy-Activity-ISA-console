using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation_of_Stormy_Activity_ISA_console
{
    
    internal class InputFlow
    {
        double _parameter; 
        TimeDistibution _distibution;
        public double time;

        public InputFlow(double parameter, TimeDistibution distibution)
        {
            _parameter = parameter;
            _distibution = distibution;
        }

        public void GetTime(double currentTime)
        {
            time = currentTime + _distibution.GetExpTime(_parameter);
        }

    }
}
