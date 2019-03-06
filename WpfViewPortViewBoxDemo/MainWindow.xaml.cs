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

namespace WpfViewPortViewBoxDemo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Rectangle selectionBox;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            double currentX = e.GetPosition(originalImage).X;
            double currentY = e.GetPosition(originalImage).Y;
            double Xmax = originalImage.ActualWidth;
            double Ymax = originalImage.ActualHeight;

            double coordX = currentX / Xmax >= 0.25 ? currentX - Xmax * 0.25 : Xmax /4 - Xmax * 0.25;
            double coordY = currentY / Ymax >= 0.25 ? currentY - Ymax * 0.25 : Ymax / 4 - Ymax * 0.25;

            double scaleX = (coordX / Xmax <= 0.5) ? coordX / Xmax : 0.5;
            double scaleY = (coordY / Ymax <= 0.5) ? coordY / Ymax : 0.5;

            ((VisualBrush)zoomedImage.Fill).Viewbox =
                new Rect() { X = scaleX, Y = scaleY, Width = 0.5, Height = 0.5 };

            if (selectionBox == null)
            {
                selectionBox = (Rectangle)this.Resources["selectionBox"];
                canvas.Children.Add(selectionBox);
            }

            double newSelectionBoxTop =
                currentY - 0.5 * selectionBox.ActualHeight;
            double newSelectionBoxLeft =
                currentX - 0.5 * selectionBox.ActualWidth;

            newSelectionBoxLeft =
                (newSelectionBoxLeft >= 0) ? newSelectionBoxLeft : 0;
            newSelectionBoxLeft =
                (newSelectionBoxLeft + selectionBox.ActualWidth <= Xmax) ? newSelectionBoxLeft : Xmax - selectionBox.ActualWidth;

            newSelectionBoxTop =
                (newSelectionBoxTop >= 0) ? newSelectionBoxTop : 0;
            newSelectionBoxTop =
                (newSelectionBoxTop + selectionBox.ActualHeight <= Ymax) ? newSelectionBoxTop : Ymax - selectionBox.ActualHeight;

            Canvas.SetLeft(selectionBox, newSelectionBoxLeft);
            Canvas.SetTop(selectionBox, newSelectionBoxTop);
            

            //Console.WriteLine($"{Xmax} {Ymax}");
            // Xmax - 1
            // X    - ?

            //X = Xmax * ?

            //? = X / Xmax
        }
        private void selectionBox_MouseLeave(object sender, MouseEventArgs e)
        {
            selectionBox.Fill.Opacity = 0.0;
        }

        private void selectionBox_MouseEnter(object sender, MouseEventArgs e)
        {
            selectionBox.Fill.Opacity = 0.25;
        }
    }
}
