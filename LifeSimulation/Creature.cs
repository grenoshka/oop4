using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LifeSimulation
{

    public enum Gender
    {
        female,
        male
    }

    public class Creature<T> : FieldObject
        where T:Food
    {
        const int InitialFullness = 150;
        const int HungerVerge = 50;
        const int FertilityRestriction = 200;

        const double NormalHungerDecreasement = 0.5;
        const int InfectedHungerDecreasment = 2;

        public bool Infected = false;
        int FertilityCount = FertilityRestriction;
        public Field Field;
        public Gender Gender;
        public Form1 Form;
        public double Fullness;
        public Random random;
        protected Creature<T> Partner=null;
        public Creature(int x, int y, Random random, Form1 form, Gender Gender, Field Field):base(x, y)
        {
            Fullness = InitialFullness;
            this.random = random;
            Form = form;
            this.Gender = Gender;
            this.Field = Field;
        }
        public void Move()
        {
            if (Infected)
                InfectSurroundingCreatures();
            (int new_x, int new_y) = ChooseNextCoordinates();
            Field.Cells[x, y].ContainsCreature = null;
            Form.DrawCell(ref Field.Cells[x, y], x, y);
            EatIfPossible(new_x, new_y);
            x = new_x;
            y = new_y;
            if (!Infected)
                Fullness-=NormalHungerDecreasement;
            else
                Fullness-=InfectedHungerDecreasment;
            if (Fullness <= 0)
            {
                Die();
                return;
            }
            FertilityCount++;
            Field.Cells[x, y].ContainsCreature = this;
            Form.DrawCell(ref Field.Cells[x,y], x, y);
        }

        protected virtual void EatIfPossible(int x, int y)
        {

        }

        public void Die()
        {
            Field.Cells[x, y].ContainsCreature = null;
            Form.DrawCell(ref Field.Cells[x, y], x, y);
            if (this is Creature<FoodGrassEater>)
                Field.RemoveGrassEater(x, y);
            else if (this is Creature<FoodCreatureEater>)
                Field.RemoveCreatureEater(x, y);
            else
                Field.RemoveAllEater(x,y);
        }

        protected (int new_x, int new_y) ChooseNextCoordinates()
        {
            int new_x = x, new_y = y;
            if (Fullness > HungerVerge)
            {
                if (FertilityCount > FertilityRestriction)
                {
                    if (Partner == null)
                        FindPartner();
                    if (Partner != null)
                    {
                        if (Partner.x == x && Partner.y == y)
                            GiveBirth();
                        else
                            (new_x, new_y) = MoveTowardsTarget(Partner.x, Partner.y);
                    }
                    else
                        (new_x, new_y) = GetRandomCoordinates();
                }
                else
                    (new_x, new_y) = GetRandomCoordinates();
            }
            else if (!SearchForFood())
                (new_x, new_y) = GetRandomCoordinates();
            else
                (new_x, new_y) = MoveTowardsFood();
            return (new_x, new_y);
        }

        protected virtual bool SearchForFood()
        {
            return true;
        }

        protected void InfectSurroundingCreatures()
        {
            for (int i=x-1; i<x+2; i+=2)
            {
                if (CheckBorders(i, y) && Field.Cells[i, y].ContainsCreature != null)
                    InfectCreatureIfPossible(i, y);
            }
            for (int i = y - 1; i < y + 2; i += 2)
            {
                if (CheckBorders(x, i) && Field.Cells[x, i].ContainsCreature != null)
                    InfectCreatureIfPossible(x, i);
            }
        }

        private void InfectCreatureIfPossible(int x, int y)
        {
            if (Field.Cells[x, y].ContainsCreature is GrassEater)
            {
                if (!Field.GrassEaters.Find(grasseater => grasseater.x == x && grasseater.y == y).Infected)
                    Field.GrassEaters.Find(grasseater => grasseater.x == x && grasseater.y == y).Infected = true;
            }
            else if (Field.Cells[x, y].ContainsCreature is CreatureEater)
            {
                if (!Field.CreatureEaters.Find(creatureater => creatureater.x == x && creatureater.y == y).Infected)
                    Field.CreatureEaters.Find(creatureater => creatureater.x == x && creatureater.y == y).Infected = true;
            }
            else if (!Field.AllEaters.Find(alleater => alleater.x == x && alleater.y == y).Infected)
                Field.AllEaters.Find(alleater => alleater.x == x && alleater.y == y).Infected = true;
        }

        protected virtual (int new_x, int new_y) MoveTowardsFood()
        {
            int new_x = x, new_y = y;
            return (new_x, new_y);
        }


        protected void MoveRandomly()
        {
            (int new_x, int new_y) = GetRandomCoordinates();
            if (CheckBorders(new_x, new_y) &&
                Field.Cells[new_x, new_y].BiomType != BiomType.water &&
                Field.Cells[new_x, new_y].ContainsCreature == null)
            {
                Field.Cells[x, y].ContainsCreature = null;
                Form.DrawCell(ref Field.Cells[x, y], x, y);
                x = new_x;
                y = new_y;
                Field.Cells[x, y].ContainsCreature = this;
                Form.DrawCell(ref Field.Cells[x, y], x, y);
            }
        }

        protected void FindPartner()
        {
            for (int i = x - 10; i <= x + 10; i++)
            {
                for (int j = y - 10; j <= y + 10; j++)
                {
                    if (CheckBorders(i, j) && i != x && j != y && 
                        Field.Cells[i,j].ContainsCreature!=null && 
                        CheckCompatibility(i,j))
                    {
                        Creature<T> MyPartner = Field.GetCreature<T>(i,j) as Creature<T>;
                        Partner = MyPartner;
                        MyPartner.Partner = this;
                        return;
                    }
                }
            }
        }

        protected bool CheckCompatibility(int x, int y)
        {
            if (typeof(T)==typeof(FoodGrassEater))
            {
                if (Field.GrassEaters.Find(creature => creature.x == x && creature.y == y) != null &&
                    Field.GrassEaters.Find(creature => creature.x == x && creature.y == y).Gender!=Gender)
                    return true;
                return false;
            }
            else if (typeof(T) == typeof(FoodCreatureEater))
            {
                if (Field.CreatureEaters.Find(creature => creature.x == x && creature.y == y) != null &&
                    Field.CreatureEaters.Find(creature => creature.x == x && creature.y == y).Gender!=Gender)
                    return true;
                return false;
            }
            else
            {
                if (Field.AllEaters.Find(creature => creature.x == x && creature.y == y) != null &&
                    Field.AllEaters.Find(creature => creature.x == x && creature.y == y).Gender!=Gender)
                    return true;
                return false;
            }
        }

        protected virtual FieldObject FindTarget<Target>()
            where Target:Food
        {
            int count = 1;
            while (count < 4)
            {
                for (int i = x - count; i <= x + count; i++)
                {
                    if (CheckBorders(i, y - count) &&
                        Field.Cells[i, y - count].ContainsCreature != null)
                    {
                        FieldObject PotentialTarget = Field.GetCreature<Target>(i, y - count);
                        if (PotentialTarget != null)
                            return PotentialTarget;
                    }
                }
                for (int i = y - count + 1; i <= y + count; i++)
                {
                    if (CheckBorders(x + count, i) &&
                        Field.Cells[x + count, i].ContainsCreature != null)
                    {
                        FieldObject PotentialTarget = Field.GetCreature<Target>(x+count, i);
                        if (PotentialTarget != null)
                            return PotentialTarget;
                    }
                }
                for (int i = x + count - 1; i >= x - count; i--)
                {
                    if (CheckBorders(i, y + count) &&
                        Field.Cells[i, y + count].ContainsCreature != null)
                    {
                        FieldObject PotentialTarget = Field.GetCreature<Target>(i, y + count);
                        if (PotentialTarget != null)
                            return PotentialTarget;
                    }
                }
                for (int i = y + count - 1; i > y - count; i--)
                {
                    if (CheckBorders(x - count, i) &&
                        Field.Cells[x - count, i].ContainsCreature != null)
                    {
                        FieldObject PotentialTarget = Field.GetCreature<Target>(x - count, i);
                        if (PotentialTarget != null)
                            return PotentialTarget;
                    }
                }
                count++;
            }
            return null;
        }

        protected (int new_x, int new_y) MoveTowardsTarget(int target_x, int target_y)
        {
            int new_x = x, new_y = y;
            if (target_x > x)
            {
                if (Field.Cells[new_x + 1, new_y].BiomType != BiomType.water)
                    new_x += 1;
            }
            else if (target_x < x)
            {
                if (Field.Cells[new_x - 1, new_y].BiomType != BiomType.water)
                    new_x -= 1;
            }
            else
            {
                if (target_y > y)
                {
                    if (Field.Cells[new_x, new_y + 1].BiomType != BiomType.water)
                        new_y += 1;
                }
                else
                    if (Field.Cells[new_x, new_y - 1].BiomType != BiomType.water)
                    new_y -= 1;
            }
            return (new_x, new_y);
        }
        protected virtual (int new_x, int new_y) MoveTowardsGrass(List<GrassPointer> nearest_food)
        {
            int min = 10, min_ind = -1, new_x, new_y;
            for (int i = 0; i < nearest_food.Count; i++)
                if (nearest_food[i].Steps < min)
                {
                    min = nearest_food[i].Steps;
                    min_ind = i;
                }
            switch (min)
            {
                case 1:
                    new_x = nearest_food[min_ind].x;
                    new_y = nearest_food[min_ind].y;
                    break;
                default:
                    new_x = x;
                    new_y = y;
                    switch (nearest_food[min_ind].Direction)
                    {
                        case Direction.left:
                            new_x--;
                            break;
                        case Direction.up:
                            new_y--;
                            break;
                        case Direction.right:
                            new_x++;
                            break;
                        case Direction.down:
                            new_y++;
                            break;
                    }
                    break;
            }
            return (new_x, new_y);
        }

        protected void GiveBirth()
        {
            FertilityCount=0;
            Creature<T> newCreature = MakeChild();
            newCreature.FertilityCount = 0;
            AddBabyToField(newCreature);
            Partner.Partner = null;
            Partner = null;
            PutBabyInCell(newCreature);
        }

        protected virtual void PutBabyInCell(Creature<T> newCreature)
        {
            Field.Cells[x, y].ContainsCreature = newCreature;
        }

        private void AddBabyToField(Creature<T> Baby)
        {
            if (typeof(T) == typeof(FoodGrassEater))
                Field.GrassEaters.Add(Baby as GrassEater);
            else if (typeof(T) == typeof(FoodCreatureEater))
                Field.CreatureEaters.Add(Baby as CreatureEater);
            else
                Field.AllEaters.Add(Baby as AllEater);
        }

        protected virtual Creature<T> MakeChild()
        {
            return new Creature<T>(x,y,random,Form,Field.RandomGender(),Field);
        }

        protected (int new_x, int new_y) GetRandomCoordinates()
        {
            int new_x = x, new_y = y, direction;
            direction = random.Next(4);
            switch (direction)
            {
                case 0:
                    new_y -= 1;
                    break;
                case 1:
                    new_x += 1;
                    break;
                case 2:
                    new_y += 1;
                    break;
                case 3:
                    new_x -= 1;
                    break;
            }
            if (CheckBorders(new_x, new_y) &&
                Field.Cells[new_x, new_y].BiomType != BiomType.water &&
                Field.Cells[new_x, new_y].ContainsCreature == null)
                return (new_x, new_y);
            else
                return (x, y);
        }
        
        protected bool CheckBorders(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Field.Width && y < Field.Height)
                return true;
            return false;
        }

        public static explicit operator Creature<T>(GrassEater v)
        {
            if (v == null)
                return null;
            return new Creature<T>(v.x, v.y, v.random, v.Form, v.Gender, v.Field);
        }

        public static explicit operator Creature<T>(CreatureEater v)
        {
            if (v == null)
                return null;
            return new Creature<T>(v.x, v.y, v.random, v.Form, v.Gender, v.Field);
        }

        public static explicit operator Creature<T>(AllEater v)
        {
            if (v == null)
                return null;
            return new Creature<T>(v.x, v.y, v.random, v.Form, v.Gender, v.Field);
        }
    }
}
