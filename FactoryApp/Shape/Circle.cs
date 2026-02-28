using FactoryApp.Interface;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FactoryApp.Class
{
    public class Circle : IShape
    {
        public Shape Draw(double size, Brush color)
        {
            return new Ellipse
            {
                Width = size,
                Height = size,
                Fill = color,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Margin = new Thickness(10)
            };
        }
    }
}