using Sokoban.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sokoban.Architecture;

public static class EntityMapCreator
{
    public static IReadOnlyList<IEntity?>[,] CreateMap(string map, string separator = "\r\n")
    {
        var rows = map.Split(new[] { separator, "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var maxLength = rows.Max(x => x.Length);
        var result = new IEntity?[maxLength, rows.Length][];
        for (var y = 0; y < rows.Length; y++)
            for (var x = 0; x < maxLength; x++)
                result[x, y] =  new[] { new Flooring(), x < rows[y].Length ? CreateEntityBySymbol(rows[y][x]) : null };
        return result;
    }


    private static IEntity? CreateEntityBySymbol(char c)
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