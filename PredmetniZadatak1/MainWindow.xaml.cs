using Microsoft.Win32;
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

namespace PredmetniZadatak1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selektovano = string.Empty;
        Nullable<bool> dialogResult;
        private System.Windows.Shapes.Polygon poligon = new System.Windows.Shapes.Polygon();
        private List<UIElement> ObrisaniElementi = new List<UIElement>();
        private Dictionary<int, List<UIElement>> recnikElemenataKojiSuClearovani = null;
        private int brojacKljuc = 0;


        public MainWindow()
        {
            InitializeComponent();
            recnikElemenataKojiSuClearovani = new Dictionary<int, List<UIElement>>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selektovano = "rectangle";
            DugmeElipsa.BorderBrush = Brushes.White;
            DugmeImage.BorderBrush = Brushes.White;
            DugmePolygon.BorderBrush = Brushes.White;
            DugmeRectangle.BorderBrush = Brushes.Red;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            selektovano = "elipsa";
            DugmeElipsa.BorderBrush = Brushes.Red;
            DugmeImage.BorderBrush = Brushes.White;
            DugmePolygon.BorderBrush = Brushes.White;
            DugmeRectangle.BorderBrush = Brushes.White;
            
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            poligon = new System.Windows.Shapes.Polygon();
            selektovano = "polygon";
            DugmeElipsa.BorderBrush = Brushes.White;
            DugmeImage.BorderBrush = Brushes.White;
            DugmePolygon.BorderBrush = Brushes.Red;
            DugmeRectangle.BorderBrush = Brushes.White;
            

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            selektovano = "image";
            DugmeElipsa.BorderBrush = Brushes.White;
            DugmeImage.BorderBrush = Brushes.Red;
            DugmePolygon.BorderBrush = Brushes.White;
            DugmeRectangle.BorderBrush = Brushes.White;
            
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(canvas);

            switch (selektovano)
            {
                case "elipsa":
                    Eclipse prozor = new Eclipse();
                    dialogResult = prozor.ShowDialog();

                    if (dialogResult.Value)
                    {
                        System.Windows.Shapes.Ellipse elipsa = new Ellipse();

                        elipsa.Width = double.Parse(prozor.X.Text);
                        elipsa.Height = double.Parse(prozor.Y.Text);
                        elipsa.StrokeThickness = double.Parse(prozor.debljina.Text);
                        elipsa.Stroke = new SolidColorBrush(((ColorInfo)prozor.ColorComboBox.SelectedItem).Color);
                        elipsa.Fill = new SolidColorBrush(((ColorInfo)prozor.BojaComboBox.SelectedItem).Color);

                        elipsa.MouseLeftButtonDown += OnElipseMouseLeftButtonDown;

                        Canvas.SetTop(elipsa, p.Y);
                        Canvas.SetLeft(elipsa, p.X);
                        canvas.Children.Add(elipsa);
                       
                    }
                    break;

                case "rectangle":
                    Rectangle prozor1 = new Rectangle();
                    dialogResult = prozor1.ShowDialog();

                    if (dialogResult.Value)
                    {
                        System.Windows.Shapes.Rectangle rec = new System.Windows.Shapes.Rectangle();
                        rec.Width = double.Parse(prozor1.sirina.Text);
                        rec.Height = double.Parse(prozor1.visina.Text);
                        rec.Stroke = new SolidColorBrush(((ColorInfo)prozor1.ColorComboBox.SelectedItem).Color);
                        rec.StrokeThickness = double.Parse(prozor1.debljina.Text);
                        rec.Fill = new SolidColorBrush(((ColorInfo)prozor1.BojaComboBox.SelectedItem).Color);

                        rec.MouseLeftButtonDown += OnRectangleMouseLeftButtonDown;

                        Canvas.SetTop(rec, p.Y);
                        Canvas.SetLeft(rec, p.X);
                        canvas.Children.Add(rec);
                       
                    }
                    break;
                case "polygon":
                    poligon.Points.Add(p);
                    break;

                case "image":
                    OpenFileDialog op = new OpenFileDialog();
                    op.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
                                "|PNG Portable Network Graphics (*.png)|*.png" +
                                "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
                                "|BMP Windows Bitmap (*.bmp)|*.bmp" +
                                "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
                                "|GIF Graphics Interchange Format (*.gif)|*.gif";
                    op.ShowDialog();
                    
                    string putanja = op.FileName;
                    Slika slika = new Slika();
                    dialogResult = slika.ShowDialog();

                    if (dialogResult.Value)
                    {
                        System.Windows.Shapes.Rectangle image = new System.Windows.Shapes.Rectangle();
                        image.Width = double.Parse(slika.sirina.Text);
                        image.Height = double.Parse(slika.visina.Text);

                        image.Fill = new ImageBrush { ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(putanja) };

                        canvas.Children.Add(image);
                        Canvas.SetTop(image, p.Y);
                        Canvas.SetLeft(image, p.X);
                        
                    }
                    break;

                default:
                    break;

            }

        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (selektovano.Equals("polygon") && poligon != null)
            {
                // za poligon su potrebne 4 tacke, uslov prolazi kad je Count 3, a 4 treba da bude zapravo prva tacka a to sama klasa
                // Poligon dodaje
                if (poligon.Points.Count > 2)
                {
                    Polygon prozor = new Polygon();
                    dialogResult = prozor.ShowDialog();

                    if (dialogResult.Value)
                    {
                        poligon.StrokeThickness = double.Parse(prozor.debljina.Text);
                        poligon.Stroke = new SolidColorBrush(((ColorInfo)prozor.ColorComboBox.SelectedItem).Color);
                        poligon.Fill = new SolidColorBrush(((ColorInfo)prozor.BojaComboBox.SelectedItem).Color);

                        poligon.MouseLeftButtonDown += OnPolygonMouseLeftButtonDown;

                        // sam postavlja poligon na odgovarajucu poziciju 
                        canvas.Children.Add(poligon);

                        poligon = new System.Windows.Shapes.Polygon();

                    }
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            DugmeElipsa.BorderBrush = Brushes.White;
            DugmeImage.BorderBrush = Brushes.White;
            DugmePolygon.BorderBrush = Brushes.White;
            DugmeRectangle.BorderBrush = Brushes.White;
            selektovano = string.Empty;

            //Clear
            if (canvas.Children.Count > 0)
            {
                List<UIElement> temp = new List<UIElement>();
                foreach (UIElement element in canvas.Children)
                {
                    temp.Add(element);
                }
                recnikElemenataKojiSuClearovani.Add(brojacKljuc, temp);
                brojacKljuc++;
                canvas.Children.Clear();
            }
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DugmeElipsa.BorderBrush = Brushes.White;
            DugmeImage.BorderBrush = Brushes.White;
            DugmePolygon.BorderBrush = Brushes.White;
            DugmeRectangle.BorderBrush = Brushes.White;
            selektovano = string.Empty;

            //undo
            if (canvas.Children.Count > 0)
            {
                ObrisaniElementi.Add(canvas.Children[canvas.Children.Count - 1]);
                canvas.Children.Remove(canvas.Children[canvas.Children.Count - 1]);
            }
            else if (canvas.Children.Count == 0 && recnikElemenataKojiSuClearovani.Count > 0)
            {

                if (recnikElemenataKojiSuClearovani.ContainsKey(--brojacKljuc))
                {
                    recnikElemenataKojiSuClearovani[brojacKljuc].ForEach(element => canvas.Children.Add(element));

                    recnikElemenataKojiSuClearovani.Remove(brojacKljuc);
                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            DugmeElipsa.BorderBrush = Brushes.White;
            DugmeImage.BorderBrush = Brushes.White;
            DugmePolygon.BorderBrush = Brushes.White;
            DugmeRectangle.BorderBrush = Brushes.White;
            selektovano = string.Empty;

            //redo
            if (ObrisaniElementi.Count > 0)
            {
                canvas.Children.Add(ObrisaniElementi[ObrisaniElementi.Count - 1]);
                ObrisaniElementi.RemoveAt(ObrisaniElementi.Count - 1);
            }
        }

        void OnRectangleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ( !selektovano.Equals("polygon") || poligon.Points.Count <= 2)
            {
                System.Windows.Shapes.Rectangle shape = (System.Windows.Shapes.Rectangle)e.OriginalSource;
                if (canvas.Children.Contains(shape))
                {
                    int index = canvas.Children.IndexOf(shape);

                    Rectangle prozor = new Rectangle();
                    //popuni sa vec postojecim atributima
                    prozor.sirina.Text = shape.Width.ToString();
                    prozor.sirina.IsReadOnly = true;
                    prozor.visina.Text = shape.Height.ToString();
                    prozor.visina.IsReadOnly = true;
                    prozor.debljina.Text = shape.StrokeThickness.ToString();
                    prozor.debljina.IsReadOnly = true;
                    prozor.BojaComboBox.SelectedValue = (Color)ColorConverter.ConvertFromString(shape.Fill.ToString());
                    prozor.ColorComboBox.SelectedValue = (Color)ColorConverter.ConvertFromString(shape.Stroke.ToString());

                    dialogResult = prozor.ShowDialog();
                    //promeni atribute
                    if (dialogResult.Value)
                    {
                        shape.Fill = new SolidColorBrush(((ColorInfo)prozor.BojaComboBox.SelectedItem).Color);
                        shape.Stroke = new SolidColorBrush(((ColorInfo)prozor.ColorComboBox.SelectedItem).Color);

                        canvas.Children.RemoveAt(index);
                        canvas.Children.Insert(index, shape);
                    }

                }
            }

        }

        void OnElipseMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!selektovano.Equals("polygon") || poligon.Points.Count <= 2)
            {
                System.Windows.Shapes.Ellipse shape = (System.Windows.Shapes.Ellipse)e.OriginalSource;
                if (canvas.Children.Contains(shape))
                {
                    int index = canvas.Children.IndexOf(shape);

                    Eclipse prozor = new Eclipse();
                    //popuni sa vec postojecim atributima
                    prozor.X.Text = shape.Width.ToString();
                    prozor.X.IsReadOnly = true;
                    prozor.Y.Text = shape.Height.ToString();
                    prozor.Y.IsReadOnly = true;
                    prozor.debljina.Text = shape.StrokeThickness.ToString();
                    prozor.debljina.IsReadOnly = true;
                    prozor.BojaComboBox.SelectedValue = (Color)ColorConverter.ConvertFromString(shape.Fill.ToString());
                    prozor.ColorComboBox.SelectedValue = (Color)ColorConverter.ConvertFromString(shape.Stroke.ToString());

                    dialogResult = prozor.ShowDialog();
                    //promeni atribute
                    if (dialogResult.Value)
                    {
                        shape.Fill = new SolidColorBrush(((ColorInfo)prozor.BojaComboBox.SelectedItem).Color);
                        shape.Stroke = new SolidColorBrush(((ColorInfo)prozor.ColorComboBox.SelectedItem).Color);
                        
                        canvas.Children.RemoveAt(index);
                        canvas.Children.Insert(index, shape);
                    }

                }
            }
        }

        void OnPolygonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!selektovano.Equals("polygon") || poligon.Points.Count <= 2)
            {
                System.Windows.Shapes.Polygon shape = (System.Windows.Shapes.Polygon)e.OriginalSource;
                if (canvas.Children.Contains(shape))
                {
                    int index = canvas.Children.IndexOf(shape);

                    Polygon prozor = new Polygon();
                    //popuni sa vec postojecim atributima
                    prozor.debljina.Text = shape.StrokeThickness.ToString();
                    prozor.debljina.IsReadOnly = true;
                    prozor.BojaComboBox.SelectedValue = (Color)ColorConverter.ConvertFromString(shape.Fill.ToString());
                    prozor.ColorComboBox.SelectedValue = (Color)ColorConverter.ConvertFromString(shape.Stroke.ToString());

                    dialogResult = prozor.ShowDialog();
                    //promeni atribute
                    if (dialogResult.Value)
                    {
                        shape.Fill = new SolidColorBrush(((ColorInfo)prozor.BojaComboBox.SelectedItem).Color);
                        shape.Stroke = new SolidColorBrush(((ColorInfo)prozor.ColorComboBox.SelectedItem).Color);
                        
                        canvas.Children.RemoveAt(index);
                        canvas.Children.Insert(index, shape);
                    }
                }
            }
        }
    }
}
