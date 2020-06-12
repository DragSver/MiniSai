using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniSai
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool click = true;
        public double maxW = DrawingAttributes.MaxWidth;
        public double minW = DrawingAttributes.MinWidth;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            InkC.EditingMode = InkCanvasEditingMode.Select;
        }

        private void Brush_Click(object sender, RoutedEventArgs e)
        {
            InkC.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            InkC.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Clr.Fill = new SolidColorBrush(Color.FromRgb((byte)Red.Value, (byte)Green.Value, (byte)Blue.Value));
            InkC.DefaultDrawingAttributes.Color = Color.FromRgb((byte)Red.Value, (byte)Green.Value, (byte)Blue.Value);
        }

        private void Width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // = WidthSlider.Value;
        }

        private void Marker_Checked(object sender, RoutedEventArgs e)
        {
            InkC.DefaultDrawingAttributes.IsHighlighter = true;
        }

        private void Marker_Unchecked(object sender, RoutedEventArgs e)
        {
            InkC.DefaultDrawingAttributes.IsHighlighter = false;
        }

        private void Smoothing_Unchecked(object sender, RoutedEventArgs e)
        {
            InkC.DefaultDrawingAttributes.FitToCurve = false;
        }

        private void Smoothing_Checked(object sender, RoutedEventArgs e)
        {
            InkC.DefaultDrawingAttributes.FitToCurve = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            FileStream fileStream = new FileStream(@"../../InkPaint.bin", FileMode.Create);
            InkC.Strokes.Save(fileStream);
            fileStream.Close();
            MessageBox.Show("Сохранено");
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            FileStream fileStream = new FileStream(@"../../InkPaint.bin", FileMode.Open, FileAccess.Read);
            StrokeCollection strokes = new StrokeCollection(fileStream);
            InkC.Strokes = strokes;
        }

        private void Stylus_Click(object sender, RoutedEventArgs e)
        {
            if (click)
            {
                InkC.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
                click = false;
            }
            else
            {
                InkC.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                click = true;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (WidthTextBox.Text == "" || double.Parse(WidthTextBox.Text) == 0) InkC.DefaultDrawingAttributes.Width = 1;
            else InkC.DefaultDrawingAttributes.Width = double.Parse(WidthTextBox.Text);
            if (HeightTextBox.Text == "" || double.Parse(HeightTextBox.Text) == 0) InkC.DefaultDrawingAttributes.Height = 1;
            else InkC.DefaultDrawingAttributes.Height = double.Parse(HeightTextBox.Text);
        }
    }
}
