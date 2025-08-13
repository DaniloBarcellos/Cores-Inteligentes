using System;
using System.Collections.Generic;

namespace GreedyColors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configuração do Grafo
            Cor initialColor = new Cor(8, 19, 141);
            Cor finalColor = new Cor(236, 76, 18);
            int nSteps = 4; //número de camadas intermediárias
            Grafo graph = new Grafo(initialColor, finalColor, nSteps);

            Console.WriteLine("Veja o grafo gerado:");
            Console.WriteLine(graph.GraphToString());

            GreedyBestFirst greedy = new GreedyBestFirst();
            greedy.Search(graph);

            List<Cor> greedyPath = greedy.GetSolutionList();
            double greedyCost = greedy.GetTotalCost();
            Console.WriteLine(greedy.SolutionToString());
            Console.WriteLine("Cost: " + greedyCost);
        }
    }
}
