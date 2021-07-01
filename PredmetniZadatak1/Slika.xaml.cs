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

namespace PredmetniZadatak1
{
    /// <summary>
    /// Interaction logic for Slika.xaml
    /// </summary>
    public partial class Slika : Window
    {
        public Slika()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string poruka = string.Empty;
            try
            {
                poruka = "Brojevi";
                double width = double.Parse(sirina.Text);
                double height = double.Parse(visina.Text);

                if (width > 0 && height > 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    poruka = "Pozitvni brojevi veci od 0";
                    throw new Exception();

                }

            }
            catch
            {
                MessageBox.Show("Width, Height moraju biti " + poruka, "Error", MessageBoxButton.OK);
            }
        }
    }
}
