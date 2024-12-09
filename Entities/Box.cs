using Sokoban.Architecture;
using System.Linq;

namespace Sokoban.Entities
{
    class Box : IEntity
    {
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
        public bool IsInPlace { get; private set; }

        public EntityCommand Act(int x, int y)
        {
            var command = new EntityCommand();

            var newX = x + DeltaX;
            var newY = y + DeltaY;

            if (CanMoveTo(newX, newY))
            {
                command.DeltaX = DeltaX;
                command.DeltaY = DeltaY;
                if (Game.Map[newX, newY].Any(x => x is StorageTargetPoint)) IsInPlace = true;
            }

            DeltaX = 0;
            DeltaY = 0;

            return command;
        }

        public bool CanMoveTo(int newX, int newY)
        {
            return Game.IsValidPosition(newX, newY) && !Game.Map[newX, newY].Any(x => x is Wall or Box);
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return IsInPlace ? "boxinplace.png" : "box.png";
        }
    }
}
