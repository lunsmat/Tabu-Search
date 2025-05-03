namespace TabuSearch {
    public struct Neighbor
    {
        public List<int> Solution;
        public int[] Changes;
    }

    public class Problem
    {

        public static int CalculateObjective(List<int> solution)
        {
            int[][] enterpriseProjectValues = [
                [12, 18, 15, 22, 9, 14, 20, 11, 17],
                [19, 8, 13, 25, 16, 10, 7, 21, 24],
                [6, 14, 27, 10, 12, 19, 23, 16, 8],
                [17, 11, 20, 9, 18, 13, 25, 14, 22],
                [10, 23, 16, 14, 7, 21, 12, 19, 15],
                [13, 25, 9, 17, 11, 8, 16, 22, 20],
                [21, 16, 24, 12, 20, 15, 9, 18, 10],
                [8, 19, 11, 16, 22, 17, 14, 10, 13],
                [15, 10, 18, 21, 13, 12, 22, 9, 16]
            ];

            int objective = 0;

            for (int i = 0; i < 9; i++) {
                objective += enterpriseProjectValues[i][solution[i]];
            }

            return objective;
        }

        public static List<int> GenerateRandomSolution()
        {
            var solution = new List<int>();
            var sortArray = new List<int>();

            for (int i = 0; i < 9; i++)
                sortArray.Add(i);
            
            var random = new Random();
            for (int i = 0; i < 9; i++) {
                var sortedIndex = random.Next(0, 9 - i);
                var sorted = sortArray[sortedIndex];
                sortArray.Remove(sorted);

                solution.Add(sorted);
            }

            return solution;
        }

        public static Neighbor GenerateNeighbour(List<int> solution)
        {
            var random = new Random();

            var referenceIndex = random.Next(0, 9);
            var swapIndex = random.Next(0, 9);

            while (referenceIndex == swapIndex) swapIndex = random.Next(0, 9);

            var newSolution = solution.GetRange(0, solution.Count);

            newSolution[referenceIndex] = solution[swapIndex];
            newSolution[swapIndex] = solution[referenceIndex];

            var neighbour = new Neighbor
            {
                Solution = newSolution,
                Changes = [referenceIndex, swapIndex]
            };

            return neighbour;
        }
    }
}