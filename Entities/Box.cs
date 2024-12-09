using Sokoban.Architecture;
using System.Linq;

namespace Sokoban.Entities
{
    class Box : IEntity
    {
        public int deltaX { get; set; }
        public int deltaY { get; set; }
        public bool IsInPlace { get; set; }
        public EntityCommand Act(int x, int y)
        {
            var command = new EntityCommand();

            var newX = x + deltaX;
            var newY = y + deltaY;

            if (CanMoveTo(newX, newY))
            {
                command.DeltaX = deltaX;
                command.DeltaY = deltaY;
                if (Game.Map[newX, newY].Any(x => x is StorageTargetPoint)) IsInPlace = true;
            }

            deltaX = 0;
            deltaY = 0;

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
            if (IsInPlace)
            {
                return "boxinplace.png";
            }
            return "box.png";
        }
    }
}
