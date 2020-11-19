using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public enum BiomType
    {
        grass,
        water,
        main
    }

    public class Biom
    {
        int FieldWidth, FieldHeight;
        public int x, y, BiomWidth, BiomHeight;
        public BiomType BiomType;
        Random random;
        Form1 Form;
        public Biom(int width, int height, BiomType BiomType, Random random, Form1 form)
        {
            FieldWidth = width;
            FieldHeight = height;
            this.BiomType = BiomType;
            this.random = random;
            Form = form;
        }
        public Biom(int width, int height, BiomType biom_type, int x, int y, int biom_width, int biom_height)
        {
            FieldWidth = width;
            FieldHeight = height;
            BiomType = biom_type;
            this.x = x;
            this.y = y;
            BiomWidth = biom_width;
            BiomHeight = biom_height;
        }
        public void GenerateBiom(ref Cell[,] cells)
        {
            x = random.Next(FieldWidth);
            y = random.Next(FieldHeight);
            BiomWidth = random.Next(FieldWidth);
            BiomHeight = random.Next(FieldHeight);
            if (x + BiomWidth < FieldWidth && y + BiomHeight < FieldHeight)
            {
                Form.DrawBiom(this);
                MarkCellsAsOccupied(ref cells, x, y, BiomWidth, BiomHeight);
            }
        }
        private void MarkCellsAsOccupied(ref Cell[,] cells, int i1, int j1, int width, int height)
        {
            for (int i = i1; i < i1 + width; i++)
                for (int j = j1; j < j1 + height; j++)
                    cells[i, j].BiomType = BiomType;
        }
    }
}
