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
using System.Windows.Shapes;

namespace Launcher
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Controller)this.DataContext).OpenPlatformer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            ((Controller)this.DataContext).OpenTowerDef();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ((Controller)this.DataContext).OpenCasino();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window wi = new MainWindow();
            wi.Show();
            this.Close();
        }
    }
}
