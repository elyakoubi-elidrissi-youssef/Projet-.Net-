
using System;

namespace ConsoleApp
{
    public class Ville : IEquatable<Ville>
    {
        public String Nom { get; }
        public float X { get; }
        public float Y { get; }

        public Ville(string nom, float x, float y)
        {
            this.Nom = nom;
            this.X = x;
            this.Y = y;
        }
        public bool Equals( Ville other)
        {
            return
              this.Nom.Equals(other.Nom) &&
              this.X.Equals(other.X) &&
              this.Y.Equals(other.Y);
        }

        public override String ToString()
        {
            return $"{Nom}";
        }
    }
}