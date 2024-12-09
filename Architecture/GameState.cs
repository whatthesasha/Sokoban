using Sokoban.Entities;
using System;
using System.Collections.Generic;

namespace Sokoban.Architecture;

public static class GameState
{
    public static void Act()
    {
        var updatedMap = new List<IEntity>[Game.MapWidth, Game.MapHeight];
        var gameIsOver = true;

        ActAndAddToMap(Game.WorkerX, Game.WorkerY, Game.Worker, updatedMap);

        for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
                foreach (var entity in Game.Map[x, y])
                {
                    if (entity is Worker)
                        continue;

                    ActAndAddToMap(x, y, entity, updatedMap);

                    if (entity is Box box && !box.IsInPlace)
                        gameIsOver = false;
                }

        Game.Map = updatedMap;
        Game.IsOver = gameIsOver;
    }

    private static void ActAndAddToMap(int x, int y, IEntity entity, List<IEntity>[,] map)
    {
        var command = entity.Act(x, y);

        int newX = x + command.DeltaX;
        var newY = y + command.DeltaY;

        if (!Game.IsValidPosition(newX, newY))
            throw new Exception($"The object {entity.GetType()} falls out of the game field");

        map[newX, newY] ??= new List<IEntity>();
        map[newX, newY].Add(entity);
    }
}