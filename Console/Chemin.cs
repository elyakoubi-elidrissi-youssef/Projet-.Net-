
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class Chemin : IEquatable<Chemin>
    {
        public double Score { get; set; }
        public List<Ville> Villes { get; set; }

        public Chemin(List<Ville> villes)
        {
            this.Villes = villes;
            this.Score = CalculateScore();

        }
        //Calculer le score
        private double CalculateScore()
        {
            int nbVilles = Villes.Count;
            double score = 0;
            for (int i = 0; i < nbVilles - 1; i++)
            {
                Ville ville1 = Villes[i];
                Ville ville2 = Villes[i + 1];
                double Xtotal = Math.Abs(ville1.X - ville2.X);
                double Ytotal = Math.Abs(ville1.Y - ville2.Y);
                double calcul = Math.Sqrt(Math.Pow(Xtotal, 2) + Math.Pow(Ytotal, 2));
                score += calcul;
            }
            return score;
        }

        public bool ContientDoublons()
        {
            return Villes.Count != Villes.Distinct().Count();
        }

        // Comparer deux chemins
        public bool Equals(Chemin other)
        {
            return
               this.Villes.Equals(other.Villes) && this.Score.Equals(other.Score);
        }

        public override String ToString()
        {
            return $"---------------- \n Score:{Score} \n Villes:{String.Join("-", Villes)}";

        }
    }
}