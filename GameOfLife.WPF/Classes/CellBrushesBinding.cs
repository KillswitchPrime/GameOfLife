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
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private static readonly PropertyChangedEventArgs PropertyName = new(nameof(BrushColor));

        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, PropertyName);
        }
    }
}
