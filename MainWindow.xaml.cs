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
            Zoo.ForEach( x => {x.Move(); x.Draw(Zeichenfläche); });
            List<Einzeller> Kindergarten = new List<Einzeller>();
            foreach (Einzeller item in Zoo)
            {
                Kindergarten.AddRange(item.Teilen());
            }
            Zoo.AddRange(Kindergarten);
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            for (int i = 0; i < 5; i++)
            {
                Zoo.Add(new Bakterie(Zeichenfläche));
                Zoo.Add(new Amöbe(Zeichenfläche));
            }
            
        }
    }
}
