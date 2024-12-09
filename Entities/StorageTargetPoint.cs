using Sokoban.Architecture;
using System;

namespace Sokoban.Entities
{
    class StorageTargetPoint : IEntity
    {
        public EntityCommand Act(int x, int y)
        {
            return new EntityCommand();
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return "moss.png";
        }
    }
}
