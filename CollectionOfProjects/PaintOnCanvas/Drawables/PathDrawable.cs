using System;
namespace PaintOnCanvas.Drawables;

public class PathDrawable : IDrawable
{
    List<PointF> pathPoints = null;

    public PathDrawable(List<PointF> pathPoints) 
	{
        this.pathPoints = pathPoints;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (pathPoints == null || pathPoints.Count < 2)
            return;

        PathF path = new PathF();


        path.MoveTo(pathPoints[0].X, pathPoints[0].Y);

        for(int i=1; i<pathPoints.Count; i++)
        {
            path.LineTo(pathPoints[i].X, pathPoints[i].Y);
        }

        canvas.StrokeColor = Colors.Green;
        canvas.StrokeSize = 6;
        canvas.DrawPath(path);
    }
}

