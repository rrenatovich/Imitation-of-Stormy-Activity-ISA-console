using Imitation_of_Stormy_Activity_ISA_console;

Model model = new();
Console.WriteLine(model);

for (int i = 0; i < 1000; i++)
{
    model.GetInputTask();
}
Console.WriteLine(' ');
model.GetInfo();