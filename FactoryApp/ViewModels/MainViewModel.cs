using FactoryApp.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FactoryApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Shape> _shapes = new();
        private string _selectedTheme;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<ShapeCreator> _creators = new()
        {
            new CircleCreator(),
            new SquareCreator(),
            new TriangleCreator()
        };
        public ObservableCollection<string> Themes { get; } =
            new() { "Красный", "Синий", "Зелёный" };

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                UpdateShapes();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTheme)));
            }
        }
        public ObservableCollection<Shape> Shapes
        {
            get => _shapes;
            set
            {
                _shapes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shapes)));
            }
        }
        public MainViewModel()
        {
            SelectedTheme = Themes.First();
        }

        private Brush GetBrush()
        {
            return SelectedTheme switch
            {
                "Красный" => Brushes.Red,
                "Синий" => Brushes.Blue,
                "Зелёный" => Brushes.Green,
                _ => Brushes.Black
            };
        }

        private void UpdateShapes()
        {
            var newShapes = new ObservableCollection<Shape>();

            foreach (var creator in _creators)
            {
                newShapes.Add(creator.DrawShape(80, GetBrush()));
            }

            Shapes = newShapes;
        }
    }
}