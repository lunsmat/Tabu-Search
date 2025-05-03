using System.Diagnostics;
using System.Diagnostics.SymbolStore;

namespace TabuSearch
{
    class Program
    {
        public static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            var Result = Algorithm.Run();
            watch.Stop();

            Console.WriteLine("O melhor resultado foi: " + Problem.CalculateObjective(Result.BestSolution) + " com  " + Result.Iterations + " Iterações");

            Console.Write("A melhor solução foi: ");

            for (int i = 0; i < 9; i++) 
            {
                Console.Write(Result.BestSolution[i] + 1 + " ");
            }

            Console.WriteLine();
            Console.WriteLine("A Solução foi alcançada em " + watch.ElapsedMilliseconds + "ms");
        }
    }
}