using Sokoban.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sokoban.Architecture;

public static class EntityMapCreator
{
    public static IReadOnlyList<IEntity>[,] CreateMap(string map, string separator = "\r\n")
    {
        var rows = map.Split(new[] { separator, "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var maxRowLength = rows.Max(x => x.Length);

        var result = new IReadOnlyList<IEntity>[maxRowLength, rows.Length];

        for (var x = 0; x < maxRowLength; x++)
            for (var y = 0; y < rows.Length; y++)
            {
                var entities = new List<IEntity>() { new Flooring() };
                var entity = x < rows[y].Length ? CreateEntityBySymbol(rows[y][x]) : null;
                if (entity != null)
                    entities.Add(entity);

                result[x, y] = entities;
            }

        return result;
    }


    private static IEntity CreateEntityBySymbol(char c)
    {
        return c switch
        {
            '@' => new Worker(),
            '$' => new Box(),
            '#' => new Wall(),
            '.' => new StorageTargetPoint(),
            '-' => null,
            ' ' => null,
            _ => throw new Exception($"wrong character for IEntity {c}")
        };
    }
}