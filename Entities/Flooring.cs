using Sokoban.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Entities
{
    internal class Flooring : IEntity
    {
        public EntityCommand Act(int x, int y)
        {
            return new EntityCommand();
        }

        public int GetDrawingPriority()
        {
            return 10;
        }

        public string GetImageFileName()
        {
            return "floor.png";
        }
    }
}
