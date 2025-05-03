namespace TabuSearch
{
    public class Algorithm
    {
        public const int MAX_ITERATIONS_WITHOUT_IMPROVEMENT = 50;

        public const int MAX_NEIGHBORS_GENERATED = 5;

        public const int MAX_TABU_LIST_SIZE = 10;

        public List<int> BestSolution = [];

        public List<int> CurrentSolution = [];

        private List<int[]> TabuList = [];

        public int Iterations = 0;

        public int BestIteration = 0;


        public static Algorithm Run()
        {
            var algorithm = new Algorithm();

            algorithm.BestSolution = Problem.GenerateRandomSolution();
            algorithm.CurrentSolution = algorithm.BestSolution.ToList();

            algorithm.TabuList = new List<int[]>();

            do {
               algorithm.RunIteration(); 
            } while((algorithm.Iterations - algorithm.BestIteration) < MAX_ITERATIONS_WITHOUT_IMPROVEMENT);

            return algorithm;
        }

        private void RunIteration()
        {
            Iterations++;

            var neighbors = new List<Neighbor>();

            for (int i = 0; i < MAX_NEIGHBORS_GENERATED; i++) {
                neighbors.Add(Problem.GenerateNeighbour(BestSolution));
            }

            var sortedNeighbors = from n in neighbors
                orderby Problem.CalculateObjective(n.Solution) ascending
                select n;


            foreach (var neighbor in sortedNeighbors) {
                if (IsInTabuList(TabuList, neighbor.Changes) && !(Problem.CalculateObjective(neighbor.Solution) < Problem.CalculateObjective(BestSolution))) {
                    continue;
                }

                CurrentSolution = neighbor.Solution;

                TabuList.Add(neighbor.Changes);
                if (TabuList.Count > MAX_TABU_LIST_SIZE) {
                    TabuList.RemoveAt(0);
                }

                if (Problem.CalculateObjective(neighbor.Solution) < Problem.CalculateObjective(BestSolution)) {
                    BestSolution = neighbor.Solution;
                    BestIteration = Iterations;
                }

                break;
            }
        }

        private bool IsInTabuList(List<int[]> TabuList, int[] values)
        {
            for (int i = 0; i < TabuList.Count; i++) 
            {
                if (TabuList[i][0] == values[0] && TabuList[i][1] == values[1]) return true;
                if (TabuList[i][1] == values[0] && TabuList[i][0] == values[1]) return true;
            }

            return false;
        }
    }
}