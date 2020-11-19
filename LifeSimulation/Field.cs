using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulation
{
    public class Field
    {
        private const int MinAmountOfCreatures = 5000;
        private const int MaxAmountOfCreatures = 6000;

        private const int MinAmountOfFood = 1000;
        private const int MaxAmountOfFood = 2000;


        public int Width, Height;
        Random random;
        public Cell[,] Cells;
        public List<Grass> FoodList;
        public List<GrassEater> GrassEaters;
        public List<CreatureEater> CreatureEaters;
        public List<AllEater> AllEaters;
        Form1 Form;

        private bool FloodStarted = false;
        int InitialFloodX, InitialFloodY, InitialFloodWidth, InitialFloodHeight;

        public Field(int width, int height, Form1 form)
        {
            Width = width;
            Height = height;
            random = new Random();
            Cells = new Cell[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    Cells[i, j] = new Cell(i,j, BiomType.main);
            Form = form;
        }
        public void GenerateBioms(int grass_amount, int water_amount)
        {
            Biom mainBiom = new Biom(Width, Height, BiomType.main, 0, 0, Width, Height);
            Form.DrawBiom(mainBiom);
            GenerateBiom(BiomType.grass, grass_amount);
            GenerateBiom(BiomType.water, water_amount);
        }

        private void GenerateBiom(BiomType BiomType, int amount)
        {
            Biom[] NeededBiom = new Biom[amount];
            for (int i=0; i<amount; i++)
            {
                NeededBiom[i] = new Biom(Width, Height, BiomType, random, Form);
                NeededBiom[i].GenerateBiom(ref Cells);
            }
        }

        public void GenerateCreatures()
        {
            int amount_of_creatures = random.Next(MinAmountOfCreatures, MaxAmountOfCreatures);
            int amount_of_grass_eaters = random.Next(0, amount_of_creatures);
            int amount_of_creature_eaters = random.Next(0, amount_of_creatures - amount_of_grass_eaters);
            int amount_of_all_eaters = amount_of_creatures - amount_of_grass_eaters - amount_of_creature_eaters;
            GrassEaters = new List<GrassEater>();
            CreatureEaters = new List<CreatureEater>();
            AllEaters = new List<AllEater>();
            GenerateGrassEaters(amount_of_grass_eaters);
            GenerateCreatureEaters(amount_of_creature_eaters);
            GenerateAllEaters(amount_of_all_eaters);
        }

        private void GenerateGrassEaters(int amount_of_grass_eaters)
        {
            for (int i = 0; i < amount_of_grass_eaters; i++)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Cells[x, y].BiomType != BiomType.water && Cells[x, y].ContainsCreature == null)
                {
                    GrassEater newCreature = new GrassEater(x, y, random, Form, RandomGender(), this);
                    GrassEaters.Add(newCreature);
                    Cells[x, y].ContainsCreature = newCreature;
                    Form.DrawCell(ref Cells[x, y], x, y);
                }
            }
        }

        private void GenerateCreatureEaters(int amount_of_creature_eaters)
        {
            for (int i = 0; i < amount_of_creature_eaters; i++)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Cells[x, y].BiomType != BiomType.water && Cells[x, y].ContainsCreature == null)
                {
                    CreatureEater newCreature = new CreatureEater(x, y, random, Form, RandomGender(), this);
                    CreatureEaters.Add(newCreature);
                    Cells[x, y].ContainsCreature = newCreature;
                    Form.DrawCell(ref Cells[x, y], x, y);
                }
            }
        }

        private void GenerateAllEaters(int amount_of_all_eaters)
        {
            for (int i = 0; i < amount_of_all_eaters; i++)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Cells[x, y].BiomType != BiomType.water && Cells[x, y].ContainsCreature == null)
                {
                    AllEater newCreature = new AllEater(x, y, random, Form, RandomGender(), this);
                    AllEaters.Add(newCreature);
                    Cells[x, y].ContainsCreature = newCreature;
                    Form.DrawCell(ref Cells[x, y], x, y);
                }
            }
        }

        public Gender RandomGender()
        {
            int randomGender = random.Next(1, 3);
            if (randomGender == 1)
                return Gender.female;
            else
                return Gender.male;
        }

        public void MoveCreatures()
        {
            for (int i = 0; i < GrassEaters.Count; i++)
                GrassEaters[i].Move();
            for (int i = 0; i < CreatureEaters.Count; i++)
                CreatureEaters[i].Move();
            for (int i = 0; i < AllEaters.Count; i++)
                AllEaters[i].Move();
        }
        public void GenerateFood()
        {
            FoodList = new List<Grass>();
            int amount_of_food = random.Next(MinAmountOfFood, MaxAmountOfFood);
            for (int i = 0; i < amount_of_food; i++)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Cells[x, y].BiomType != BiomType.water && Cells[x, y].ContainsCreature == null)
                {
                    FoodList.Add(new Grass(x, y));
                    Cells[x, y].ContainsFood = true;
                    Form.DrawCell(ref Cells[x, y], x, y);
                    MarkSpaceAroundFood(x, y);
                }
            }
        }

        public void GenerateNewFood(int AmountOfNewGeneratedFood)
        {
            int InitialAmountOfFood = FoodList.Count;
            for (int i=0; i<InitialAmountOfFood; i++)
            {
                for (int j=0; j<AmountOfNewGeneratedFood; j++)
                {
                    int x = random.Next(FoodList[i].x - AmountOfNewGeneratedFood, FoodList[i].x + AmountOfNewGeneratedFood);
                    int y = random.Next(FoodList[i].y - AmountOfNewGeneratedFood, FoodList[i].y + AmountOfNewGeneratedFood);
                    if (CheckBorders(x,y) && !Cells[x,y].ContainsFood && Cells[x,y].ContainsCreature==null)
                    {
                        FoodList.Add(new Grass(x, y));
                        Cells[x, y].ContainsFood = true;
                        Form.DrawCell(ref Cells[x, y], x, y);
                        MarkSpaceAroundFood(x, y);
                    }
                }
            }
        }

        private void MarkSpaceAroundFood(int food_x, int food_y)
        {
            for (int y = -3, j=6; y < 0; y++, j--)
            {
                for (int i = j, x = -3; i > j-3; i--, x++)
                {
                    if (CheckBorders(food_x + x, food_y + y))
                        Cells[food_x + x, food_y + y].NearestFood.Add(new GrassPointer(food_x, food_y, i, Direction.down));
                    if (CheckBorders(food_x - x, food_y + y))
                        Cells[food_x - x, food_y + y].NearestFood.Add(new GrassPointer(food_x, food_y, i, Direction.down));
                    if (CheckBorders(food_x + x, food_y - y))
                        Cells[food_x + x, food_y - y].NearestFood.Add(new GrassPointer(food_x, food_y, i, Direction.up));
                    if (CheckBorders(food_x - x, food_y - y))
                        Cells[food_x - x, food_y - y].NearestFood.Add(new GrassPointer(food_x, food_y, i, Direction.up));
                }
            }
            for (int i = 3; i > 0; i--)
            {
                if (CheckBorders(food_x, food_y - i))
                    Cells[food_x, food_y - i].NearestFood.Add(new GrassPointer(food_x, food_y, i, Direction.down));
                if (CheckBorders(food_x - i, food_y))
                    Cells[food_x - i, food_y].NearestFood.Add(new GrassPointer(food_x, food_y, i, Direction.right));
                if (CheckBorders(food_x + i, food_y))
                    Cells[food_x + i, food_y].NearestFood.Add(new GrassPointer(food_x, food_y, i, Direction.left));
                if (CheckBorders(food_x, food_y + i))
                    Cells[food_x, food_y + i].NearestFood.Add(new GrassPointer(food_x, food_y , i, Direction.up));
            }
        }
        private bool CheckBorders(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height && Cells[x,y].BiomType!= BiomType.water)
                return true;
            return false;
        }

        public string CreatureInfo(int x, int y)
        {
            string info;
            if (Cells[x, y].ContainsCreature == null)
                info = "";
            else 
            {
                Creature<Food> Creature = (Creature<Food>)Cells[x, y].ContainsCreature;
                info = "Fulllnes: " + Creature.Fullness.ToString() +
                    "\r\nGender: " + Creature.Gender.ToString();
                if (typeof(Food)==typeof(FoodGrassEater))
                    info +="\r\nType: GrassEater \r\n";
                else if (typeof(Food)==typeof(FoodCreatureEater))
                    info += "\r\nType: CreatureEater \r\n";
                else
                    info += "\r\nType: AllEater \r\n";
            }
            return info;
        }

        public void GenerateFlood()
        {
            Biom Flood;
            bool GeneratedFiitingFlood = false;
            if (!FloodStarted)
            {
                while (!GeneratedFiitingFlood)
                {
                    InitialFloodX = random.Next(0, Width);
                    InitialFloodY = random.Next(0, Height);
                    InitialFloodWidth = random.Next(0, Width / 10);
                    InitialFloodHeight = random.Next(0, Height / 10);
                    if (InitialFloodX + InitialFloodWidth < Width && InitialFloodY + InitialFloodHeight < Height)
                        GeneratedFiitingFlood = true;
                }
                GeneratedFiitingFlood = false;
                FillCellsWithFlood(InitialFloodX, InitialFloodY, InitialFloodWidth, InitialFloodHeight);
                Flood = new Biom(Width, Height, BiomType.water, InitialFloodX, InitialFloodY, InitialFloodWidth, InitialFloodHeight);
                FloodStarted = true;
            }
            else
            {
                int newFloodX = InitialFloodX, newFloodY = InitialFloodY, newFloodWidth = InitialFloodWidth, newFloodHeight = InitialFloodHeight;
                while (!GeneratedFiitingFlood)
                {
                    newFloodX = random.Next(InitialFloodX, InitialFloodX + InitialFloodWidth);
                    newFloodY = random.Next(InitialFloodY, InitialFloodY + InitialFloodHeight);
                    newFloodWidth = random.Next(InitialFloodWidth/5, InitialFloodWidth/2);
                    newFloodHeight = random.Next(InitialFloodHeight/5, InitialFloodHeight/2);
                    if (newFloodX + newFloodWidth < Width && newFloodY + newFloodHeight < Height)
                        GeneratedFiitingFlood = true;
                }
                GeneratedFiitingFlood = false;
                FillCellsWithFlood(newFloodX, newFloodY, newFloodWidth, newFloodHeight);
                Flood = new Biom(Width, Height, BiomType.water, newFloodX, newFloodY, newFloodWidth, newFloodHeight);
            }
            Form.DrawBiom(Flood);
        }

        private void FillCellsWithFlood(int newFloodX, int newFloodY, int newFloodWidth, int newFloodHeight)
        {
            for (int x = newFloodX; x <= newFloodX + newFloodWidth; x++)
            {
                for (int y = newFloodY; y <= newFloodY + newFloodHeight; y++)
                {
                    if (Cells[x, y].ContainsCreature != null)
                        KillCreature(x, y);
                    if (Cells[x, y].ContainsFood)
                    {
                        Cells[x, y].ContainsFood = false;
                        NullSpaceAroundFood(x, y);
                    }
                    Cells[x, y].BiomType = BiomType.water;
                }
            }
        }

        private void KillCreature(int x, int y)
        {
            if (Cells[x, y].ContainsCreature is GrassEater)
                GrassEaters.Remove(GrassEaters.Find(grasseater => grasseater.x == x && grasseater.y == y));
            else if (Cells[x, y].ContainsCreature is CreatureEater)
                CreatureEaters.Remove(CreatureEaters.Find(creatureater => creatureater.x == x && creatureater.y == y));
            else
                AllEaters.Remove(AllEaters.Find(alleater => alleater.x == x && alleater.y == y));
        }

        public void NullSpaceAroundFood(int food_x, int food_y)
        {
            for (int y = -3; y < 4; y++)
                for (int x = -3; x < 4; x++)
                    if (CheckBorders(food_x + x, food_y + y))
                        for (int i = 0; i < Cells[food_x + x, food_y + y].NearestFood.Count; i++)
                            if (Cells[food_x + x, food_y + y].NearestFood[i].x == food_x &&
                                Cells[food_x + x, food_y + y].NearestFood[i].y == food_y)
                            {
                                Cells[food_x + x, food_y + y].NearestFood.RemoveAt(i);
                                break;
                            }
        }
        public void InfectCreatures()
        {
            bool GeneratedFiitingInfection = false;
            int infectionX=0, infectionY=0, infectionWidth=Width, infectionHeight=Height;
            while (!GeneratedFiitingInfection)
            {
                infectionX = random.Next(0, Width);
                infectionY = random.Next(0, Height);
                infectionWidth = random.Next(0, Width/2);
                infectionHeight = random.Next(0, Height/2);
                if (infectionX + infectionWidth < Width && infectionY + infectionHeight < Height)
                    GeneratedFiitingInfection = true;
            }
            for (int i=0; i<GrassEaters.Count; i++)
            {
                if (GrassEaters[i].x >= infectionX && GrassEaters[i].x <= infectionX + infectionWidth &&
                    GrassEaters[i].y >= infectionY && GrassEaters[i].y <= infectionY + infectionHeight)
                    GrassEaters[i].Infected = true;
            }
            for (int i = 0; i < CreatureEaters.Count; i++)
            {
                if (CreatureEaters[i].x >= infectionX && CreatureEaters[i].x <= infectionX + infectionWidth &&
                    CreatureEaters[i].y >= infectionY && CreatureEaters[i].y <= infectionY + infectionHeight)
                    CreatureEaters[i].Infected = true;
            }
            for (int i = 0; i < AllEaters.Count; i++)
            {
                if (AllEaters[i].x >= infectionX && AllEaters[i].x <= infectionX + infectionWidth &&
                    AllEaters[i].y >= infectionY && AllEaters[i].y <= infectionY + infectionHeight)
                    AllEaters[i].Infected = true;
            }
        }
        public void RemoveGrassEater(int x, int y)
        {
            GrassEaters.Remove(GrassEaters.Find(creature => creature.x==x && creature.y==y));
        }
        public void RemoveCreatureEater(int x, int y)
        {
            CreatureEaters.Remove(CreatureEaters.Find(creature => creature.x == x && creature.y == y));
        }
        public void RemoveAllEater(int x, int y)
        {
            AllEaters.Remove(AllEaters.Find(creature => creature.x == x && creature.y == y));
        }

        public Creature<Target> GetCreature<Target>(int x, int y)
            where Target:Food
        {
            if (typeof(Target) == typeof(FoodGrassEater))
            {
                return (Creature<Target>)GrassEaters.Find(creature => creature.x == x && creature.y == y);
            }
            else if (typeof(Target) == typeof(FoodCreatureEater))
            {
                return (Creature<Target>)CreatureEaters.Find(creature => creature.x == x && creature.y == y);
            }
            else
                return (Creature<Target>)AllEaters.Find(creature => creature.x == x && creature.y == y);
        }
        public Creature<Food> GetCreatureInCell(int x, int y)
        {
            Creature<Food> NeededCreature = (Creature<Food>)GrassEaters.Find(creature=> creature.x==x && creature.y==y);
            if (NeededCreature != null)
                return NeededCreature;
            else
            {
                NeededCreature = (Creature<Food>)CreatureEaters.Find(creature => creature.x == x && creature.y == y);
                if (NeededCreature != null)
                    return NeededCreature;
                else
                    return (Creature<Food>)AllEaters.Find(creature => creature.x == x && creature.y == y);
            }
        }
    }
}
