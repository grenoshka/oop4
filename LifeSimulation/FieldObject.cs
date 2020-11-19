using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public abstract class FieldObject
    {
        public int x, y;
        public FieldObject(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
