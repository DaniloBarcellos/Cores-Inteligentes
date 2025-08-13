using System.Collections.Generic;
using System.Linq;

namespace GreedyColors
{
    internal class GreedyBestFirst
    {
        private List<Cor> _solution = new List<Cor>();
        private double _totalCost = 0;

        public void Search(Grafo graph)
        {
            _solution.Clear();
            _totalCost = 0;

            List<Cor> keyColors = graph.GetGraph().Keys.ToList();
            int i = 0;
            _solution.Add(keyColors[i]);

            Cor iterationColor = keyColors[i];
            while (graph.GetGraph()[iterationColor].Count != 1)
            {
                List<double> eudclidianDistances = new List<double>();
                for (int c = 0; c < 3; ++c)
                {
                    eudclidianDistances.Add(iterationColor.CalculateEuclidianDistance(graph.GetGraph()[iterationColor].ElementAt(c)));
                }
                int position = GetMinimumElementIndex(eudclidianDistances);

                iterationColor = graph.GetGraph()[iterationColor].ElementAt(position);
                _solution.Add(iterationColor);
            }
            _solution.Add(graph.GetGraph()[iterationColor].ElementAt(0));
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {
            int size = _solution.Count;
            for (int i = 1; i < size; ++i)
            {
                _totalCost += _solution.ElementAt(i - 1).CalculateEuclidianDistance(_solution.ElementAt(i));
            }
        }

        #region Helpful Methods
        private int GetMinimumElementIndex(List<double> elements)
        {
            int size = elements.Count;
            double minimum = elements.ElementAt(0);
            int position = 0;

            for (int i = 1; i < size; ++i)
            {
                if (minimum > elements[i])
                {
                    position = i;
                    minimum = elements[i];
                }
            }

            return position;
        }

        public string SolutionToString()
        {
            string text = "begin: ";
            int size = _solution.Count;
            int i = 0;
            foreach (Cor c in _solution)
            {
                if (i == size - 1)
                {
                    text += c.ColorToString() + ": end";
                }
                else
                {
                    text += c.ColorToString() + "->";
                }
                ++i;
            }
            text += "\n";
            return text;
        }

        public List<Cor> GetSolutionList()
        {
            return new List<Cor>(_solution);
        }

        public double GetTotalCost()
        {
            return _totalCost;
        }

        public void CleanSolution()
        {
            _solution = new List<Cor>();
            _totalCost = 0;
        }
        #endregion
    }
}
