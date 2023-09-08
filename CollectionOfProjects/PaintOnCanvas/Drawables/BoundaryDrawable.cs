using System;
namespace PaintOnCanvas.Drawables;

public class BoundaryDrawable : IDrawable
{
    private int canvasDim = 280;
	public BoundaryDrawable(int canvasDim)
	{
        this.canvasDim = canvasDim;
	}

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.DarkBlue;
        canvas.StrokeSize = 4;
        canvas.DrawRectangle(-1, -1, canvasDim+1, canvasDim+1);
    }
}

