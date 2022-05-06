using System.ComponentModel;
using System.Windows.Media;

namespace GameOfLifeWPF.Classes
{
    internal class CellBrushesBinding : INotifyPropertyChanged
    {
        public CellBrushesBinding(SolidColorBrush brushColor)
        {
            _BrushColor = brushColor;
        }

        private SolidColorBrush _BrushColor;

        public SolidColorBrush BrushColor
        {
            get { return _BrushColor; }
            set
            {
                _BrushColor = value;
                OnPropertyChanged(nameof(BrushColor));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
