using Imitation_of_Stormy_Activity_ISA_console;
using System.Diagnostics;
using System.Text;


var sw = new Stopwatch();
sw.Start();
Model model = new();
Console.WriteLine(model);
/*model.MainCycle();*/

int i = 0;
while (model.modelTime < 1000)
{
    /*Console.WriteLine(model.modelTime);*/
    model.MainCycle();
}
/*for (int i = 0; i < 10000; i++)
{
    model.MainCycle();
}
Console.WriteLine(' ');*/
/*model.GetInfo();*/


model.statistics.GetStat(model.modelTime);

sw.Stop();
Console.WriteLine($"------{sw.Elapsed} sec--------");

