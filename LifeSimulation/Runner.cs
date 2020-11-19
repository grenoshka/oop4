using System;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LifeSimulation
{
    class Runner
    {
        const int GrassBioms = 30;
        const int WaterBioms = 7;

        Random random;

        const int MaxIteration = 100;
        const int FoodGenerationIteration = 300;
        int FloodIterationNumber, InfectionIterationNumber;

        public int AmountOfNewGeneratedFood = 5;

        int FoodIterationCount = 0;
        int FloodIterationCount = 0;
        int InfectionIterationCount = 0;
        Form1 Form;

        public Field LifeField;
        public Runner (Form1 form)
        {
            random = new Random();
            Form = form;
            LifeField = new Field(form.pictureBox1.Width, form.pictureBox1.Height, form);
        }
        public void StartLife()
        {
            FloodIterationNumber = random.Next(0, MaxIteration);
            InfectionIterationNumber = random.Next(0, MaxIteration);
            LifeField.GenerateBioms(GrassBioms, WaterBioms);
            LifeField.GenerateCreatures();
            LifeField.GenerateFood();
            Form.RefreshField();
        }
        public void SimulateLife()
        {
            if (FoodIterationCount==FoodGenerationIteration)
            {
                LifeField.GenerateNewFood(AmountOfNewGeneratedFood);
                FoodIterationCount = 0;
            }
            if (FloodIterationCount==FloodIterationNumber)
            {
                LifeField.GenerateFlood();
                FloodIterationCount = 0;
                FloodIterationNumber = random.Next(0, MaxIteration);
            }
            if (InfectionIterationCount==InfectionIterationNumber)
            {
                LifeField.InfectCreatures();
                InfectionIterationCount = 0;
                InfectionIterationNumber = random.Next(0, MaxIteration);
            }
            FoodIterationCount++;
            FloodIterationCount++;
            InfectionIterationCount++;
            LifeField.MoveCreatures();
            Form.RefreshField();
        }
    }
}
