using System;
using System.Drawing;

namespace ResizableRectGraphics.Drawables;

public class ResizableRectangleDrawable : IDrawable
{
    private readonly RectF _rect;

    public ResizableRectangleDrawable(RectF rectangle)
    {
        _rect = rectangle;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Colors.Red;
        canvas.FillRectangle(_rect.X, _rect.Y, _rect.Width, _rect.Height);
    }
}