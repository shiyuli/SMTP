using System.ComponentModel;
using System.Windows;

namespace SMTP.UI
{
    internal class ControlStyle : INotifyPropertyChanged
    {
        private int _buttonWidth;
        private int _textBoxWidth;
        private int _popupBoxWidth;

        private Thickness _margin;

        public int ButtonWidth
        {
            get => _buttonWidth;
            set
            {
                _buttonWidth = value;
                OnPropertyChanged(nameof(ButtonWidth));
            }
        }

        public int TextBoxWidth
        {
            get => _textBoxWidth;
            set
            {
                _textBoxWidth = value;
                OnPropertyChanged(nameof(TextBoxWidth));
            }
        }

        public int PopupBoxWidth
        {
            get => _popupBoxWidth;
            set
            {
                _popupBoxWidth = value;
                OnPropertyChanged(nameof(PopupBoxWidth));
            }
        }

        public Thickness Margin
        {
            get => _margin;
            set
            {
                _margin = value;
                OnPropertyChanged(nameof(Margin));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
