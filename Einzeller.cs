using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petrischale
{
    abstract class Einzeller
    {
        public double posX { get; protected set; }
        public double posY { get; protected set; }
        public bool istTot { get; protected set; }

        static protected Random rnd = new Random();

        Ellipse e = new Ellipse();
        
        public Einzeller(Canvas Zeichenfläche, int radius, Brush color)
        {
            posX = rnd.Next(0, Convert.ToInt32(Zeichenfläche.ActualWidth));
            posY = rnd.Next(0, Convert.ToInt32(Zeichenfläche.ActualHeight));
            e.Width = radius;
            e.Height = radius;
            e.Fill = color;
            istTot = false;
        }

        public Einzeller(Einzeller ez)
        {
            posX = ez.posX;
            posY = ez.posY;
            e.Width = ez.e.Width;
            e.Height = ez.e.Height;
            e.Fill = ez.e.Fill;
        }


        public void Draw(Canvas Zeichenfläche)
        {
            Zeichenfläche.Children.Add(e);
            Canvas.SetTop(e,posY);
            Canvas.SetLeft(e,posX);
        }

        public void Move()
        {
            posX += 2 * rnd.Next(-1, 2);
            posY += 2 * rnd.Next(-1, 2);
        }

        public void Färben(Brush color)
        {
            e.Fill = color;
        }

        public abstract List<Einzeller> Teilen();
        public abstract void Sterben();
        public abstract void Sterben(bool stirb);
        public abstract void Fressen(Bakterie b);
    }

    class Bakterie : Einzeller
    {
        public Bakterie(Canvas zeichenfläche) : base(zeichenfläche, 5, Brushes.Green) { }
        public Bakterie(Bakterie b) : base(b) { }
        public override List<Einzeller> Teilen()
        {
            List<Einzeller> töchter = new List<Einzeller>();
            if (rnd.NextDouble() < 0.02)
            {
                töchter.Add(new Bakterie(this));
            }
            return töchter;
        }

        public override void Sterben()
        {            
            if (rnd.NextDouble() < 0.01)
            {
                this.istTot = true;
            }            
        }
        public override void Sterben(bool stirb)
        {
            this.istTot = stirb;
        }

        public override void Fressen(Bakterie b) { }

        public void Positionieren(double x, double y)
        {
            posX = x;
            posY = y;
        }
    }

    class Amöbe : Einzeller
    {
        public bool hatgefressen { get; private set; }
        public Amöbe(Canvas zeichenfläche) : base(zeichenfläche, 10, Brushes.Red)
        {
            hatgefressen = false;
        }
        public Amöbe(Amöbe b) : base(b)
        {
            hatgefressen = false;
            this.Färben(Brushes.Red);
        }
        public override List<Einzeller> Teilen()
        {
            List<Einzeller> töchter = new List<Einzeller>();
            if (rnd.NextDouble() < 0.2 && hatgefressen)
            {
                töchter.Add(new Amöbe(this));
            }
            return töchter;
        }

        public override void Sterben()
        {
            if (rnd.NextDouble() < 0.01)
            {
                this.istTot = true;
            }
        }
        public override void Sterben(bool stirb)
        {
            this.istTot = stirb;
        }

        public override void Fressen(Bakterie futter)
        {
            double Abstand = Math.Sqrt(Math.Pow(posX-futter.posX, 2)+Math.Pow(posY-futter.posY, 2));
            if (Abstand < 10)
            {
                futter.Sterben(true);
                this.Färben(Brushes.Blue);
                hatgefressen = true;
            }
        }

        public void Positionieren(double x, double y)
        {
            posX = x;
            posY = y;
        }
    }
}
