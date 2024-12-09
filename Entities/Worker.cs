using Sokoban.Architecture;
using Sokoban;
using System.Collections.Generic;
using Avalonia.Input;
using System.Linq;

namespace Sokoban.Entities
{
    public class Worker : IEntity
    {
        private Dictionary<Key, int> deltaXByKey = new Dictionary<Key, int>()
        {
            { Key.Left, -1 },
            { Key.Right, 1 },
            { Key.A, -1 },
            { Key.D, 1 },
        };

        private Dictionary<Key, int> deltaYByKey = new Dictionary<Key, int>()
        {
            { Key.Up, -1 },
            { Key.Down, 1 },
            { Key.W, -1 },
            { Key.S, 1 },
        };

        public EntityCommand Act(int x, int y)
        {
            deltaXByKey.TryGetValue(Game.KeyPressed, out var deltaX);
            deltaYByKey.TryGetValue(Game.KeyPressed, out var deltaY);

            var command = new EntityCommand();

            var newX = x + deltaX;
            var newY = y + deltaY;

            if (deltaX == 0 && deltaY == 0 || !Game.IsValidPosition(newX, newY) ||
                Game.Map[newX, newY].Any(x => x is Wall || x is Box box && !box.CanMoveTo(newX + deltaX, newY + deltaY)))
            {
                return command;
            }

            var box = Game.Map[newX, newY].OfType<Box>().FirstOrDefault();
            if (box != null)
            {
                box.DeltaX = deltaX;
                box.DeltaY = deltaY;
            }

            command.DeltaX = deltaX;
            command.DeltaY = deltaY;
            Game.Moves++;

            return command;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Girl2.png";
        }
    }
}
