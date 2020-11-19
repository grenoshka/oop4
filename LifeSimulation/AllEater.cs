using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public class AllEater : Creature<FoodAllEater>
    {
        private Creature<Food> FoodCreature;
        private bool FoundGrassToEat = false;
        public AllEater(int x, int y, Random random, Form1 form, Gender Gender, Field Field) : base(x, y, random, form, Gender, Field)
        {
        }
        protected override void EatIfPossible(int x, int y)
        {
             if (Field.Cells[x, y].ContainsCreature!=null && 
                (Field.GetCreature<FoodGrassEater>(x, y) != null || Field.GetCreature<FoodCreatureEater>(x, y) != null))
            {
                Fullness++;
                FoodCreature = null;
                if (Field.GetCreature<FoodGrassEater>(x, y) != null)
                    EatGrassEater(x, y);
                else
                    EatCreatureEater(x, y);
            }
        }

        private void EatGrassEater(int x, int y)
        {
            Field.GrassEaters.Find(creature => creature.x == x && creature.y == y).Die();

        }

        private void EatCreatureEater(int x, int y)
        {
            Field.CreatureEaters.Find(creature => creature.x == x && creature.y == y).Die();
        }

        protected override void PutBabyInCell(Creature<FoodAllEater> newCreature)
        {
            AllEater Baby = new AllEater(newCreature.x, newCreature.y, newCreature.random, newCreature.Form, newCreature.Gender, newCreature.Field);
            Field.Cells[x, y].ContainsCreature = Baby;
        }

        protected override bool SearchForFood()
        {
            if (Field.Cells[x, y].NearestFood.Any())
            {
                FoundGrassToEat = true;
                return true;
            }
            FoodCreature = FindTarget<FoodGrassEater>() as Creature<Food>;
            if (FoodCreature != null)
                return true;
            else
            {
                FoodCreature = FindTarget<FoodCreatureEater>() as Creature<Food>;
                if (FoodCreature != null)
                    return true;
            }
            return false;
        }
        protected override (int new_x, int new_y) MoveTowardsFood()
        {
            if (FoundGrassToEat)
            {
                if (Field.Cells[x, y].NearestFood.Any())
                    return MoveTowardsGrass(Field.Cells[x, y].NearestFood);
                else
                {
                    int new_x=x, new_y=y;
                    return (new_x, new_y);
                }
            }
            else
                return MoveTowardsTarget(FoodCreature.x, FoodCreature.y);
        }

        protected override Creature<FoodAllEater> MakeChild()
        {
            return new AllEater(x, y, random, Form, Field.RandomGender(), Field);
        }
    }
}
