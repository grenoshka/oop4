using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public class GrassEater : Creature<FoodGrassEater>
    {
        public GrassEater(int x, int y, Random random, Form1 form, Gender Gender, Field Field) : base(x, y, random, form, Gender, Field)
        {
        }

        protected override void EatIfPossible(int x, int y)
        {
            if (Field.Cells[x, y].ContainsFood == true)
            {
                Fullness++;
                Field.Cells[x, y].ContainsFood = false;
                Field.FoodList.RemoveAll(Food => Food.x == x && Food.y == y);
                Form.DrawCell(ref Field.Cells[x, y], x, y);
                Field.NullSpaceAroundFood(x, y);
            }
        }

        protected override void PutBabyInCell(Creature<FoodGrassEater> newCreature)
        {
            GrassEater Baby = new GrassEater(newCreature.x, newCreature.y, newCreature.random, newCreature.Form, newCreature.Gender, newCreature.Field);
            Field.Cells[x, y].ContainsCreature = Baby;
        }

        protected override bool SearchForFood()
        {
            if (Field.Cells[x, y].NearestFood.Any())
                return true;
            return false;
        }
        protected override (int new_x, int new_y) MoveTowardsFood()
        {
            return MoveTowardsGrass(Field.Cells[x, y].NearestFood);
        }

        protected override Creature<FoodGrassEater> MakeChild()
        {
            return new GrassEater(x,y,random, Form, Field.RandomGender(), Field);
        }
    }

}
