using Imitation_of_Stormy_Activity_ISA_console;

Model model = new();
Console.WriteLine(model);

for (int i = 0; i < 10; i++)
{
    model.GetInputTask();
}