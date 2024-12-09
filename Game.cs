
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Input;
using Avalonia.Logging;
using Sokoban.Architecture;
using Sokoban.Entities;

namespace Sokoban;

public static class Game
{
    public const int ElementSize = 32;

    private static IReadOnlyList<IEntity>[,] _map = new IReadOnlyList<IEntity>[0, 0];

	public static IReadOnlyList<IEntity>[,] Map 
	{ 
		get => _map;
		set
		{
			_map = value;
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
				{
                    var worker = Game.Map[x, y].OfType<Worker>().FirstOrDefault();
					if (worker != null)
					{
						Worker = worker;
						WorkerX = x;
						WorkerY = y;
						break;
                    }
				}
        }
	}

    public static bool IsOver;

    public static int WorkerX { get; private set; }
	public static int WorkerY { get; private set; }
	public static Worker Worker { get; private set; }

	public static Key KeyPressed;

	public static int Moves;
	public static int MapWidth => Map.GetLength(0);
	public static int MapHeight => Map.GetLength(1);

	public static void CreateMap(string levelLayout)
	{
		Map = EntityMapCreator.CreateMap(levelLayout);
	}

	public static bool IsValidPosition(int x, int y)
	{
        return x >= 0 && y >= 0 && y < MapHeight && x < MapWidth;
    }
}