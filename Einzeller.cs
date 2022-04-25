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
        public int posX { get; private set; }
        public int posY { get; private set; }

        static protected Random rnd = new Random();

        Ellipse e = new Ellipse();
        
        public Einzeller(Canvas Zeichenfläche, int radius, Brush color)
        {
            posX = rnd.Next(0, Convert.ToInt32(Zeichenfläche.ActualWidth));
            posY = rnd.Next(0, Convert.ToInt32(Zeichenfläche.ActualHeight));
            e.Width = radius;
            e.Height = radius;
            e.Fill = color;
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

        public abstract List<Einzeller> Teilen();
    }

    class Bakterie : Einzeller
    {
        public Bakterie(Canvas zeichenfläche) : base(zeichenfläche, 5, Brushes.Green) { }
        public Bakterie(Bakterie b) : base(b) { }
        public override List<Einzeller> Teilen()
        {
            List<Einzeller> töchter = new List<Einzeller>();
            if (rnd.NextDouble() < 0.2)
            {
                töchter.Add(new Bakterie(this));
            }
            return töchter;
        }
    }

    class Amöbe : Einzeller
    {
        public Amöbe(Canvas zeichenfläche) : base(zeichenfläche, 10, Brushes.Red) { }
        public Amöbe(Amöbe b) : base(b) { }
        public override List<Einzeller> Teilen()
        {
            List<Einzeller> töchter = new List<Einzeller>();
            if (rnd.NextDouble() < 0.02)
            {
                töchter.Add(new Amöbe(this));
            }
            return töchter;
        }
    }
}
