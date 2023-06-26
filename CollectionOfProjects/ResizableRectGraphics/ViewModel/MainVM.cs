using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using ResizableRectGraphics.Drawables;

namespace ResizableRectGraphics.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        #region PropertyChangedSetup
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private RectF _rectangle;
        private bool _isResizing;
        private int _touchpointMargin = 50;

        public ICommand StartInteractionCommand { get; }
        public ICommand DragInteractionCommand { get; }

        private IDrawable _rectangleDrawable;
        public IDrawable RectangleDrawable
        {
            get => _rectangleDrawable;
            private set
            {
                if (_rectangleDrawable != value)
                {
                    _rectangleDrawable = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainVM()
        {
            StartInteractionCommand = new Command<TouchEventArgs>(OnStartInteraction);
            DragInteractionCommand = new Command<TouchEventArgs>(OnDragInteraction);

            _rectangle = new RectF(250, 150, 300, 300);
            RectangleDrawable = new ResizableRectangleDrawable(_rectangle);
        }

        private void OnStartInteraction(TouchEventArgs e)
        {
            var touchPoint = e.Touches[0]; // Get the first touch point

            // Check if the touch point is near the edge of the rectangle
            if (Math.Abs(touchPoint.X - _rectangle.Right) <= _touchpointMargin || Math.Abs(touchPoint.Y - _rectangle.Bottom) <= _touchpointMargin)
            {
                _isResizing = true;
            }
        }

        private void OnDragInteraction(TouchEventArgs e)
        {
            if (_isResizing)
            {
                var touchPoint = e.Touches[0]; // Get the first touch point

                // Determine new bounds
                float newX = _rectangle.X;
                float newY = _rectangle.Y;
                float newRight = _rectangle.X + _rectangle.Width;
                float newBottom = _rectangle.Y + _rectangle.Height;

                // Check if the touch point is on the left side of the rectangle
                if (Math.Abs(touchPoint.X - _rectangle.X) < _touchpointMargin)
                {
                    newX = touchPoint.X;
                }
                // Check if the touch point is on the right side of the rectangle
                else if (Math.Abs(touchPoint.X - (_rectangle.X + _rectangle.Width)) < _touchpointMargin)
                {
                    newRight = touchPoint.X;
                }

                // Check if the touch point is on the top side of the rectangle
                if (Math.Abs(touchPoint.Y - _rectangle.Y) < _touchpointMargin)
                {
                    newY = touchPoint.Y;
                }
                // Check if the touch point is on the bottom side of the rectangle
                else if (Math.Abs(touchPoint.Y - (_rectangle.Y + _rectangle.Height)) < _touchpointMargin)
                {
                    newBottom = touchPoint.Y;
                }

                // Update the rectangle's position and dimensions
                _rectangle.X = newX;
                _rectangle.Y = newY;
                _rectangle.Width = newRight - newX;
                _rectangle.Height = newBottom - newY;

                RectangleDrawable = new ResizableRectangleDrawable(_rectangle);
            }
        }
    }
}
