﻿using System;
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
    /// Interaction logic for Eclipse.xaml
    /// </summary>
    public partial class Eclipse : Window
    {
        public Eclipse()
        {
            InitializeComponent();

            BojaComboBox.ItemsSource = typeof(Colors).GetProperties().Select(p => new ColorInfo(p));
            ColorComboBox.ItemsSource = typeof(Colors).GetProperties().Select(p => new ColorInfo(p));
            ColorComboBox.SelectedValue = Colors.AliceBlue;
            BojaComboBox.SelectedValue = Colors.AliceBlue;

            this.DataContext = this;
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
                double RadiusX = double.Parse(X.Text);
                double RadiusY = double.Parse(Y.Text);
                double Thickness = double.Parse(debljina.Text);

                if(RadiusX > 0 && RadiusY > 0 && Thickness > 0)
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
                MessageBox.Show("RadiusX, RadiusY, Border Thickness moraju biti "+poruka , "Error", MessageBoxButton.OK);
            }

        }
    }
}
