using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    public class GrassPointer
    {
        public int x, y;
        public int Steps;
        public Direction Direction = Direction.up | Direction.down | Direction.left | Direction.right;

        public GrassPointer(int x, int y, int steps, Direction direction)
        {
            this.x = x;
            this.y = y;
            Steps = steps;
            Direction = direction;
        }
    }
}
