using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // programme pour tester sur console
            int nbChemins = 8;
            int xoverCoefficient = 7;
            int xoverPivot = 2;
            int echangeCoefficient = 8;
            int eliteCoefficient = 3;

            List<Chemin> totale = new List<Chemin>();
        

            Ville ville1 = new Ville("Nice", 642, 863);
            Ville ville2 = new Ville("Saint-laurent", 765, 254);
            Ville ville3 = new Ville("Cagnes-sur-mer", 206, 475);
            Ville ville4 = new Ville("Biot", 874, 452);
            Ville ville5 = new Ville("Antibes", 345, 345);
            Ville ville6 = new Ville("Mougins", 453, 543);
            Ville ville7 = new Ville("Grasse", 437, 938);
            Ville ville8 = new Ville("Cannes", 65, 243);
            Ville ville9 = new Ville("Valbonne", 234, 976);
            Ville ville10 = new Ville("Menton", 432, 635);


            //create a list
            List<Ville> villes = new List<Ville>();
            // Add items using Add method   
            villes.Add(ville1);
            villes.Add(ville2);
            villes.Add(ville3);
            villes.Add(ville4);
            villes.Add(ville5);
            villes.Add(ville6);
            villes.Add(ville7);
            villes.Add(ville8);
            villes.Add(ville9);
            villes.Add(ville10);


            Generateur generateur = new Generateur();
            //Generer chemins
            List<Chemin> chemins = generateur.GenererChemins(nbChemins, villes);
            Console.WriteLine("***** Chemins générés***** ");
            Console.WriteLine(String.Join("\n \n", chemins));

            //Mutation
            List<Chemin> cheminsModifies = generateur.Echanger(chemins, echangeCoefficient);
            //Thread.Sleep(3000);
            Console.WriteLine("\n \n *****liste des chemins modifiés*****");
            Console.WriteLine(String.Join("\n", cheminsModifies));
            totale.AddRange(cheminsModifies);
           // Thread.Sleep(1000);

            //xover
            List<Chemin> cheminsXover = generateur.GenererXOver(chemins, xoverPivot, xoverCoefficient);
            Thread.Sleep(3000);
            Console.WriteLine("\n \n *****Chemins générés par le xover*****");
            Console.WriteLine(String.Join("\n", cheminsXover));
            totale.AddRange(cheminsXover);
            //Thread.Sleep(1000);

            //elite
            List<Chemin> cheminsElite = generateur.Elite(chemins, eliteCoefficient);
            //Thread.Sleep(1000);
            Console.WriteLine("\n \n *****Chemins générés par le Elite");
            Console.WriteLine(String.Join("\n", cheminsElite));
            totale.AddRange(cheminsElite);
            //Thread.Sleep(1000);

            //resultat
           List<Chemin> resultat = generateur.Elite(totale, nbChemins);
            //Thread.Sleep(1000);
            Console.WriteLine("\n \n *****Resultat");
            Console.WriteLine(String.Join("\n", resultat));

            Console.ReadLine();


        }
    }
}