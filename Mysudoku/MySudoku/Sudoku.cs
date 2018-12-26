// <copyright file="Sudoku.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MySudoku
{
    public class Sudoku
    {
        public Sudoku()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.Shudu[i, j] = 0;
                }
            }

            this.Shudu[0, 0] = 9;
            this.Hang[0, 9 - 1] = true;
            this.Lie[0, 9 - 1] = true;
            this.Sansan[0, 9 - 1] = true;
        }

        public int[,] Shudu { get; set; } = new int[9, 9];

        public bool[,] Hang { get; set; } = new bool[9, 9];

        public bool[,] Lie { get; set; } = new bool[9, 9];

        public bool[,] Sansan { get; set; } = new bool[9, 9];

        public void GenerateSudokuEnding2(int row)
        {
            char[,] model = new char[9, 9]
            {
                { 'i', 'g', 'h', 'c', 'a', 'b', 'f', 'd', 'e' },
                { 'c', 'a', 'b', 'f', 'd', 'e', 'i', 'g', 'h' },
                { 'f', 'd', 'e', 'i', 'g', 'h', 'c', 'a', 'b' },
                { 'g', 'h', 'i', 'a', 'b', 'c', 'd', 'e', 'f' },
                { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' },
                { 'd', 'e', 'f', 'g', 'h', 'i', 'a', 'b', 'c' },
                { 'h', 'i', 'g', 'b', 'c', 'a', 'e', 'f', 'd' },
                { 'b', 'c', 'a', 'e', 'f', 'd', 'h', 'i', 'g' },
                { 'e', 'f', 'd', 'h', 'i', 'g', 'b', 'c', 'a' }
            };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (model[i, j] == 'i')
                    {
                        this.Shudu[i, j] = 9;
                    }
                    else if (model[i, j] == 'g')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 0];
                    }
                    else if (model[i, j] == 'h')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 1];
                    }
                    else if (model[i, j] == 'c')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 2];
                    }
                    else if (model[i, j] == 'a')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 3];
                    }
                    else if (model[i, j] == 'b')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 4];
                    }
                    else if (model[i, j] == 'f')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 5];
                    }
                    else if (model[i, j] == 'd')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 6];
                    }
                    else if (model[i, j] == 'e')
                    {
                        this.Shudu[i, j] = Program.Pailie[row, 7];
                    }
                }
            }
        }

        public void GenerateSudokuEnding()
        {
            int[] gene59 = new int[9] { 9, 5, 2, 7, 6, 1, 3, 8, 4 };
            int[] gene1 = new int[8] { 5, 2, 7, 3, 6, 8, 1, 4 };
            this.Generate33(gene1, 1);
            this.Generate33(gene59, 5);
            this.Generate33(gene59, 9);
            this.SolveSudoku(0);
        }

        public void Initialize()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.Shudu[i, j] = 0;
                    this.Hang[i, j] = false;
                    this.Lie[i, j] = false;
                    this.Sansan[i, j] = false;
                }
            }

            this.Shudu[0, 0] = 9;
            this.Hang[0, 9 - 1] = true;
            this.Lie[0, 9 - 1] = true;
            this.Sansan[0, 9 - 1] = true;
        }

        public bool SolveSudoku(int position)
        {
            int index = position;
            for (; index < 81 && this.Shudu[index / 9, index % 9] != 0; index++)
            {
            }

            if (index < 81)
            {
                int line = index / 9;
                int col = index % 9;
                for (int i = 1; i < 10; i++)
                {
                    if (this.Hang[line, i - 1] || this.Lie[col, i - 1] || this.Sansan[(line / 3 * 3) + (col / 3), i - 1])
                    {
                        continue;
                    }

                    this.Shudu[line, col] = i;
                    this.Hang[line, i - 1] = true;
                    this.Lie[col, i - 1] = true;
                    this.Sansan[(line / 3 * 3) + (col / 3), i - 1] = true;

                    if (this.SolveSudoku(index + 1))
                    {
                        return true;
                    }

                    this.Shudu[line, col] = 0;
                    this.Hang[line, i - 1] = false;
                    this.Lie[col, i - 1] = false;
                    this.Sansan[(line / 3 * 3) + (col / 3), i - 1] = false;
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
                case 1:
                {
                    row = 1;
                    temp = 1;
                }

                break;
                case 5: row = 4; break;
                default: row = 7; break;
            }

            Program.Random159(gene);
            for (int i = 0; i < gene.Length; i++)
            {
                if (i + temp < 3)
                {
                    this.Shudu[row - 1, (row / 3 * 3) + ((i + temp) % 3)] = gene[i];
                    this.Hang[row - 1, gene[i] - 1] = true;
                }
                else if (i + temp < 6)
                {
                    this.Shudu[row, (row / 3 * 3) + ((i + temp) % 3)] = gene[i];
                    this.Hang[row, gene[i] - 1] = true;
                }
                else
                {
                    this.Shudu[row + 1, (row / 3 * 3) + ((i + temp) % 3)] = gene[i];
                    this.Hang[row + 1, gene[i] - 1] = true;
                }

                this.Lie[(row / 3 * 3) + ((i + temp) % 3), gene[i] - 1] = true;
                this.Sansan[num - 1, gene[i] - 1] = true;
            }
        }
    }
}
