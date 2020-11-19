using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{

    public class Cell
    {
        public FieldObject ContainsCreature = null;
        public bool ContainsFood = false;
        public int x, y;
        public BiomType BiomType = BiomType.main | BiomType.water | BiomType.grass;
        public List<GrassPointer> NearestFood = new List<GrassPointer>();
        public Cell(int x, int y, BiomType biom_type)
        {
            this.x = x;
            this.y = y;
            BiomType = biom_type;
        }
    }
}
