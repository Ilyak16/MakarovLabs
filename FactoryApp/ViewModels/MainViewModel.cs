using FactoryApp.Class;
using FactoryApp.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FactoryApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IShapeFactory _currentFactory;
        private string _selectedTheme;
        private ObservableCollection<Shape> _shapes = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Themes { get; }

        public MainViewModel()
        {
            Themes = new ObservableCollection<string> { "Красный", "Синий", "Зелёный" };
            SelectedTheme = Themes[0];
        }

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    _currentFactory = ShapeFactoryProvider.GetFactory(_selectedTheme);
                    UpdateShapes();
                    OnPropertyChanged(nameof(SelectedTheme));
                }
            }
        }

        public ObservableCollection<Shape> Shapes
        {
            get => _shapes;
            set
            {
                _shapes = value;
                OnPropertyChanged(nameof(Shapes));
            }
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
            var newShapes = new ObservableCollection<Shape>
            {
                _currentFactory.CreateCircle().Draw(80, GetBrush()),
                _currentFactory.CreateSquare().Draw(80, GetBrush()),
                _currentFactory.CreateTriangle().Draw(80, GetBrush())
            };

            Shapes = newShapes;
        }

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}