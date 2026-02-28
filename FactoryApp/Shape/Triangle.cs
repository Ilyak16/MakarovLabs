using FactoryApp.Interface;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FactoryApp.Class
{
    public class Triangle : IShape
    {
        public Shape Draw(double size, Brush color)
        {
            var polygon = new Polygon
            {
                Fill = color,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Margin = new Thickness(10)
            };

            polygon.Points.Add(new Point(size / 2, 0));
            polygon.Points.Add(new Point(0, size));
            polygon.Points.Add(new Point(size, size));

            return polygon;
        }
    }
}