using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySudoku
{
    class Sudoku
    {
        public void GenerateSudokuEnding()
        {
            //替换部分
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    shudu[i, j] = Convert.ToChar(i+65);
                }
            }
        }

        public void SovleSudoku()
        {
            //填充部分

            Console.WriteLine("求解完成");
        }

        private char[,] shudu = new char[9, 9];

        public char[,] Shudu
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
       
     }
}
