using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
//SQLite
using System.Data.SQLite;
using ConsoleApp;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Threading;

namespace ApplicationRoute
{

    public partial class MainWindow : Window
    {
        List<Ville> Villes = new List<Ville>();
        int cpt = 0;
        int cpt2 = 0;
        //liste pour suppresion des ellipses
        List<UIElement> itemstoremove = new List<UIElement>();

        //liste d'affichage des chemins resultats
        public ObservableCollection<Chemin> ListeChemin = new ObservableCollection<Chemin>();

        //liste des villes ajoutees de SQLite
        public ObservableCollection<Ville>ListeVillesSQLite = new ObservableCollection<Ville>();

        //liste des chemins resultat
        public ObservableCollection<Chemin> ListeCheminsReslutats = new ObservableCollection<Chemin>();

        //Liste des villes 
        public ObservableCollection<Ville> ListeVilles = new ObservableCollection<Ville>();

        // dictionnaire pour mettre ville et ellipse correspondant ( pour supprimer ellipse apres suppression ville)
        private IDictionary<Ville, Ellipse> Dictionnaire_ville_ellipse = new Dictionary<Ville, Ellipse>();

        public MainWindow()
        {
            InitializeComponent();

            //initialisation liste des villes recuperes de SQLite
            grid_first.ItemsSource = ListeVilles;
            g.ItemsSource = ListeChemin;
        }

        //methode lors du click sur boutton sur carte pour aller chercher une ville sur SQLite
        public void RechercheVilleSQLite(object sender, RoutedEventArgs e)
        {
            tab_global.SelectedIndex = 1;
            ListeVillesSQLite.Clear();
        }

        public void RunProgramme(object sender, RoutedEventArgs e)
        {
            if(ListeVilles==null)
            {
                MessageBox.Show("Liste des villes vide !");
            }
            else
            {
                tab_global.SelectedIndex = 2;

                int nbChemins = 5;
                int xoverCoefficient = 7;
                int xoverPivot = 2;
                int echangeCoefficient = 8;
                int eliteCoefficient = 3;

                List<Chemin> totale = new List<Chemin>();
                List<Chemin> resultat = new List<Chemin>();

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
                //Thread.Sleep(3000);


                //Mutation
                List<Chemin> cheminsModifies = generateur.Echanger(chemins, echangeCoefficient);
                Thread.Sleep(2000);
                foreach (Chemin item in cheminsModifies)
                {
                    totale.Add(item);
                }
                //Thread.Sleep(3000);
                //cross-over
                List<Chemin> cheminsXover = generateur.GenererXOver(chemins, xoverPivot, xoverCoefficient);
                Thread.Sleep(2000);
                foreach (Chemin item in cheminsXover)
                {
                    totale.Add(item);
                }

                //elite
                List<Chemin> cheminsElite = generateur.Elite(chemins, eliteCoefficient);
                Thread.Sleep(2000);

                foreach (Chemin item in cheminsElite)
                {
                    totale.Add(item);
                }
                Thread.Sleep(2000);
     

                //resultat
                resultat = generateur.Elite(totale, nbChemins);
                Thread.Sleep(2000);

                foreach (Chemin item in resultat)
                {
                    ListeChemin.Add(item);
                }

                
            }



        }

  
                

        public void Ajouter_ville(Ville v)
        {
            ListeVilles.Add(v);
        }
        public void getPointedVille(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(image_canvas);
            double x = p.X;
            double y = p.Y;

            string name_ville="";
            
            //get ville name from coordonnees
            string cs = @"URI=file:C:\Users\DELL\Documents\Villes.db";

            var con = new SQLiteConnection(cs);
            con.Open();

            string sx =x.ToString().Replace(',', '.');
            string sy = y.ToString().Replace(',', '.');

            string stm = "SELECT name FROM Villes where "+sx+" between xmin and xmax and " + sy + " between ymin and ymax;";

            var cmd = new SQLiteCommand(stm, con);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                name_ville = rdr.GetString(0);

            }
            else
            {
                name_ville = "Ville_" + cpt;
                cpt++;
            }

            Ville v = new Ville(name_ville, (float)x, (float)y);

            
            dessiner_ville(v);
            Ajouter_ville(v);
        }
        public void dessiner_ville(Ville v)
        {
            Ellipse ellipse = new Ellipse() ;
            cpt2++;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Black;
            ellipse.Fill = mySolidColorBrush;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = Brushes.Aqua;
            ellipse.Width = 8;
            ellipse.Height = 8;

            Canvas.SetTop(ellipse, v.Y -5);
            Canvas.SetLeft(ellipse, v.X -5);
            image_canvas.Children.Add(ellipse);
            Dictionnaire_ville_ellipse.Add(v, ellipse);

        }

        
        private void Reset(object sender, RoutedEventArgs e)
        {
            foreach (Ville item in ListeVilles)
            {
                Ellipse ellipse = Dictionnaire_ville_ellipse[item];
                image_canvas.Children.Remove(ellipse);
            }
            ListeVilles.Clear();
            ListeVillesSQLite.Clear();
        }
       private void Supprimer_Ville(object sender, MouseButtonEventArgs e)
        {

            Ville a = (e.Source as ListView).SelectedItem as Ville;

            if (a!=null)
            {
                ListeVilles.Remove(a);

                Ellipse ellipse = Dictionnaire_ville_ellipse[a];
                image_canvas.Children.Remove(ellipse);
            }
        }

        private void tab_global_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


}
