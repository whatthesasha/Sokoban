using Sokoban.Architecture;
using System;

namespace Sokoban.Entities
{
    class Wall : IEntity
    {
        public EntityCommand Act(int x, int y)
        {
            return new EntityCommand();
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "wall.png";
        }
    }
}
