using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public class CreatureEater : Creature<FoodCreatureEater>
    {
        private GrassEater FoodTarget;
        public CreatureEater(int x, int y, Random random, Form1 form, Gender Gender, Field Field) : base(x, y, random, form, Gender, Field)
        {
        }
        protected override void EatIfPossible(int x, int y)
        {
            if (Field.Cells[x, y].ContainsCreature!=null && Field.GetCreature<FoodGrassEater>(x, y) != null)
            {
                Fullness++;
                FoodTarget = null;
                EatCreature(x, y);
            }
        }
        private void EatCreature(int x, int y)
        {
            Field.GrassEaters.Find(creature => creature.x == x && creature.y == y).Die();
        }

        protected override void PutBabyInCell(Creature<FoodCreatureEater> newCreature)
        {
            CreatureEater Baby = new CreatureEater(newCreature.x, newCreature.y, newCreature.random, newCreature.Form, newCreature.Gender, newCreature.Field);
            Field.Cells[x, y].ContainsCreature = Baby;
        }

        protected override bool SearchForFood()
        {
            FoodTarget = FindTarget<FoodGrassEater>() as GrassEater;
            if (FoodTarget != null)
                return true;
            return false;
        }

        protected override (int new_x, int new_y) MoveTowardsFood()
        {
            return MoveTowardsTarget(FoodTarget.x, FoodTarget.y);
        }

        protected override Creature<FoodCreatureEater> MakeChild()
        {
            return new CreatureEater(x, y, random, Form, Field.RandomGender(), Field);
        }
    }
}
