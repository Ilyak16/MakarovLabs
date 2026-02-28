using FactoryApp.Interface;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FactoryApp.Class
{
    public class Square : IShape
    {
        public Shape Draw(double size, Brush color)
        {
            return new Rectangle
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