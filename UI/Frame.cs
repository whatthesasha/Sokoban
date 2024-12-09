using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Sokoban.Architecture;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sokoban.UI;

public class Frame : UserControl
{
    private readonly Dictionary<string, Bitmap> bitmaps = new();

    public Frame()
    {
        // Otherwise Avalonia designer fails with a null reference exception
        if (Design.IsDesignMode)
            return;

        var imagesDirectory = new DirectoryInfo("Images");
        foreach (var e in imagesDirectory.GetFiles("*.png"))
            bitmaps[e.Name] = new Bitmap(e.FullName);
    }

    public void Initialize()
    {
        Height = Game.MapHeight * Game.ElementSize;
        Width = Game.MapWidth * Game.ElementSize;

        Act();
    }

    public override void Render(DrawingContext drawingContext)
    {
        // Otherwise Avalonia designer fails with a null reference exception
        if (Design.IsDesignMode)
            return;

        for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
            {
                var entities = Game.Map[x, y].OrderByDescending(x => x.GetDrawingPriority());
                foreach (var entity in entities)
                {
                    drawingContext.DrawImage(bitmaps[entity.GetImageFileName()], new Rect(x, y, 1, 1) * Game.ElementSize);
                }
            }
    }

    public void Act()
    {
        GameState.Act();
        InvalidateVisual();
    }
}