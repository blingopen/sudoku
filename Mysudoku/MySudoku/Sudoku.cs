using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySudoku
{
    class Sudoku
    {
        public void GenerateSudokuEnding2(int row)
        {
            char[,] model = new char[9, 9] {
                { 'i','g','h','c','a','b','f','d','e' },
                { 'c','a','b','f','d','e','i','g','h' },
                { 'f','d','e','i','g','h','c','a','b' },
                { 'g','h','i','a','b','c','d','e','f' },
                { 'a','b','c','d','e','f','g','h','i' },
                { 'd','e','f','g','h','i','a','b','c' },
                { 'h','i','g','b','c','a','e','f','d' },
                { 'b','c','a','e','f','d','h','i','g' },
                { 'e','f','d','h','i','g','b','c','a' }
            };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (model[i, j] == 'i') shudu[i, j] = 9;
                    else if (model[i, j] == 'g') shudu[i, j] = Program.pailie[row, 0];
                    else if (model[i, j] == 'h') shudu[i, j] = Program.pailie[row, 1];
                    else if (model[i, j] == 'c') shudu[i, j] = Program.pailie[row, 2];
                    else if (model[i, j] == 'a') shudu[i, j] = Program.pailie[row, 3];
                    else if (model[i, j] == 'b') shudu[i, j] = Program.pailie[row, 4];
                    else if (model[i, j] == 'f') shudu[i, j] = Program.pailie[row, 5];
                    else if (model[i, j] == 'd') shudu[i, j] = Program.pailie[row, 6];
                    else if (model[i, j] == 'e') shudu[i, j] = Program.pailie[row, 7];
                }
            }
        }

        public void GenerateSudokuEnding()
        {
            int[] gene59 = new int[9] { 9,5,2,7,6,1,3,8,4 };
            int[] gene1 = new int[8] { 5,2,7,3,6,8,1,4 };
            Generate33(gene1, 1);
            Generate33(gene59, 5);
            Generate33(gene59, 9);
            SolveSudoku(0);
        }

        public void Initialize()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    shudu[i, j] = 0;
                    hang[i, j] = false;
                    lie[i, j] = false;
                    sansan[i, j] = false;
                }
            }
            shudu[0, 0] = 9;
            hang[0, 9 - 1] = true;
            lie[0, 9 - 1] = true;
            sansan[0, 9 - 1] = true;
        }

        public bool SolveSudoku(int position)
        {
            int index = position;
            for (; index < 81 && 0 != shudu[index / 9, index % 9]; index++) ;

            if (index < 81)
            {
                int line = index / 9;
                int col = index % 9;
                for (int i = 1; i < 10; i++)
                {
                    if (hang[line, i - 1] || lie[col, i - 1] || sansan[line / 3 * 3 + col / 3, i - 1])
                        continue;

                    shudu[line, col] = i;
                    hang[line, i - 1] = true;
                    lie[col, i - 1] = true;
                    sansan[line / 3 * 3 + col / 3, i - 1] = true;

                    if (SolveSudoku(index + 1))
                        return true;

                    shudu[line, col] = 0;
                    hang[line, i - 1] = false;
                    lie[col, i - 1] = false;
                    sansan[line / 3 * 3 + col / 3, i - 1] = false;
                }
                return false;
            }
            return true;
        }

        private void Generate33(int[] gene, int num)
        {
            int temp = 0;
            int row;
            switch (num)
            {
                case 1: { row = 1; temp = 1; } break;
                case 5: row = 4; break;
                default: row = 7; break;
            }
            Program.Random159(gene);
            for (int i = 0; i < gene.Length; i++)
            {
                if (i + temp < 3)
                {
                    shudu[row - 1, row / 3 * 3 + (i + temp) % 3] = gene[i];
                    hang[row - 1, gene[i] - 1] = true;
                }
                else if (i + temp < 6)
                {
                    shudu[row, row / 3 * 3 + (i + temp) % 3] = gene[i];
                    hang[row, gene[i] - 1] = true;
                }
                else
                {
                    shudu[row + 1, row / 3 * 3 + (i + temp) % 3] = gene[i];
                    hang[row + 1, gene[i] - 1] = true;
                }

                lie[row / 3 * 3 + (i + temp) % 3, gene[i] - 1] = true;
                sansan[num - 1, gene[i] - 1] = true;
            }
        }

        private int[,] shudu = new int[9, 9];
        public int[,] Shudu
        {
            get { return shudu; }
            set { shudu = value; }
        }

        private bool[,] hang = new bool[9, 9];
        public bool[,] Hang
        {
            get { return hang; }
            set { hang = value; }
        }

        private bool[,] lie = new bool[9, 9];
        public bool[,] Lie
        {
            get { return lie; }
            set { lie = value; }
        }

        private bool[,] sansan = new bool[9, 9];
        public bool[,] Sansan
        {
            get { return sansan; }
            set { sansan = value; }
        }

        public Sudoku()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    shudu[i, j] = 0;
                }
            }
            shudu[0, 0] = 9;
            hang[0, 9 - 1] = true;
            lie[0, 9 - 1] = true;
            sansan[0, 9 - 1] = true;
        }

    }

}
