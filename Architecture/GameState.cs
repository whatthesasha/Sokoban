using Avalonia;
using Sokoban.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sokoban.Architecture;

public class GameState
{
    public List<EntityAnimation> Animations = new();
    
    public static bool IsOver;

    public void Act()
    {
        Animations.Clear();

        ActAndAddAnimation(Game.Worker, Game.WorkerX, Game.WorkerY);

        for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
            {
                var entities = Game.Map[x, y];

                foreach (var entity in entities)
                {
                    if (entity == null || entity is Worker)
                        continue;

                    ActAndAddAnimation(entity, x, y);
                }
            }


        Animations = Animations.OrderByDescending(z => z.Entity.GetDrawingPriority()).ToList();
        UpdateMap();

        IsOver = Animations.Select(x => x.Entity).OfType<Box>().All(x => x.IsInPlace);
    }

    private void ActAndAddAnimation(IEntity entity, int x, int y)
    {
        var command = entity.Act(x, y);

        if (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
            y + command.DeltaY >= Game.MapHeight)
            throw new Exception($"The object {entity.GetType()} falls out of the game field");

        var targetLogicalLocation = new Point(x + command.DeltaX, y + command.DeltaY);
        Animations.Add(
            new EntityAnimation
            {
                Command = command,
                Entity = entity,
                TargetLogicalLocation = targetLogicalLocation,
                TargetLocation = targetLogicalLocation * Game.ElementSize,
            });
    }
    
    private void UpdateMap()
    {
        var entities = new List<IEntity>[Game.MapWidth, Game.MapHeight];
        for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
                entities[x, y] = new List<IEntity>();
        foreach (var e in Animations)
        {
            var x = (int)e.TargetLogicalLocation.X;
            var y = (int)e.TargetLogicalLocation.Y;
            entities[x, y].Add(e.Entity);
        }

        Game.Map = entities;
    }
}