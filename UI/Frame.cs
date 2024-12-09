using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Sokoban.Architecture;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Globalization;
using System.IO;

namespace Sokoban.UI;

public class Frame : UserControl
{
    private readonly Dictionary<string, Bitmap> bitmaps = new();
    private readonly GameState gameState;

    public Frame()
    {
        if (Design.IsDesignMode)
            return;

        gameState = new GameState();
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

    public override void Render(DrawingContext e)
    {
        if (Design.IsDesignMode)
            return;

        foreach (var a in gameState.Animations)
            e.DrawImage(bitmaps[a.Entity.GetImageFileName()],
                new Rect(a.TargetLocation, new Size(Game.ElementSize, Game.ElementSize)));
    }

    public void Act()
    {
        gameState.Act();
        InvalidateVisual();
    }
}