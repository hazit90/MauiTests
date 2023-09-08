using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PaintOnCanvas.Drawables;

namespace PaintOnCanvas.ViewModel;

public class MainVM : INotifyPropertyChanged
{
    #region PropertyChangedSetup
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    public ICommand DragInteractionCommand { get; }
    public ICommand ClearCanvasCommand { get; }

    private List<PointF> touchList;
    private PathDrawable drawnPath;
    

    public int CanvasDim { get => 280; }

    public PathDrawable DrawnPath
    {
        get => drawnPath;
        private set
        {
            if(drawnPath != value)
            {
                drawnPath = value;
                OnPropertyChanged();
            }
        }
    }

    public BoundaryDrawable CanvasBoundary
    {
        get => new BoundaryDrawable(CanvasDim);
    }

    public MainVM()
	{
        DragInteractionCommand = new Command<TouchEventArgs>(OnDragInteraction);
        ClearCanvasCommand = new Command(ClearCanvas);

        touchList = new List<PointF>();
    } 

    private void OnDragInteraction(TouchEventArgs args)
    {
        var numTouches = args.Touches.Count();
        if (numTouches > 1)
        {
            Console.WriteLine("Touches deteected: " + numTouches);
            return;
        }

        var touchPoint = args.Touches[0];
        var X = touchPoint.X;
        var Y = touchPoint.Y;
        if (X < 0 || X > CanvasDim || Y < 0 || Y > CanvasDim)
            return;

        touchList.Add(new PointF(X, Y));

        DrawnPath = new PathDrawable(touchList);

        Console.WriteLine(X + ", " + Y);
    }

    private void ClearCanvas()
    {
        touchList.Clear();
        DrawnPath = null;
    }
}

