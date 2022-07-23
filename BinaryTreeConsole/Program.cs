// See https://aka.ms/new-console-template for more information

Console.Write("Please enter formula: ");
string? input = Console.ReadLine();
if (input != null)
{
    int output = BinaryTreeCalculator.BinaryTreeCalculator.EvaluateExpression(input);
    Console.WriteLine($"Result: {output}");
}


