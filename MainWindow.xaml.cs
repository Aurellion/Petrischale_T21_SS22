using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Petrischale
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<Einzeller> Zoo = new List<Einzeller>();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(17);            
            timer.Tick += animate;
        }

        private void animate(object sender, EventArgs e)
        {
            Zeichenfläche.Children.Clear();
            List<Einzeller> Kindergarten = new List<Einzeller>();
            List<Einzeller> Futter = new List<Einzeller>();
            foreach (Einzeller item in Zoo)
            {
                if (item is Bakterie)
                {
                    Futter.Add(item);
                }
            }
            Zoo.ForEach( x => 
            {
                x.Move();
                x.Draw(Zeichenfläche);
                Kindergarten.AddRange(x.Teilen());
                x.Sterben();
            });
            foreach (Einzeller itemAmöbe in Zoo)
            {
                if (itemAmöbe is Amöbe)
                {
                    foreach (Bakterie itemBakterie in Futter)
                    {
                        itemAmöbe.Fressen(itemBakterie);
                    }
                }               
            }
            Zoo.AddRange(Kindergarten);
            Zoo.RemoveAll(x => x.istTot);
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            btn_Start.IsEnabled = false;
            timer.Start();
            for (int i = 0; i < 20; i++)
            {
                Zoo.Add(new Bakterie(Zeichenfläche));
                Zoo.Add(new Amöbe(Zeichenfläche));
            }
            
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Amöbe a = new Amöbe(Zeichenfläche);
            a.Positionieren(e.GetPosition(Zeichenfläche).X, e.GetPosition(Zeichenfläche).Y);
            Zoo.Add(a);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Bakterie a = new Bakterie(Zeichenfläche);
            a.Positionieren(e.GetPosition(Zeichenfläche).X, e.GetPosition(Zeichenfläche).Y);
            Zoo.Add(a);
        }
    }
}
