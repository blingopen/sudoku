using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySudoku;

namespace SudokuUI
{
    public partial class Form1 : Form
    {
        private const int N = 9;
        TextBox[,] textBoxes = new TextBox[N, N];
        Sudoku sudoku = new Sudoku();

        public Form1()
        {
            InitializeComponent();
            btn_Reset.Enabled = false;
        }

        void GenerateAllButtons()
        {
            int x0 = 100, y0 = 10, w = 45, d = 50;
            for (int r = 0; r < N; r++)
                for (int c = 0; c < N; c++)
                {
                    TextBox txt = new TextBox
                    {
                        MaxLength = 1,
                        Top = y0 + r * d,
                        Left = x0 + c * d,
                        Width = w,
                        Height = w,
                        Visible = true,
                        Tag = r * N + c
                    };

                    int block = r / 3 * 3 + c / 3;
                    if (block % 2 == 0)
                    {
                        txt.BackColor = Color.Orange;
                    }

                    if (sudoku.Shudu[r, c] > 0)
                    {
                        txt.Text = sudoku.Shudu[r, c].ToString();
                        txt.Font = new Font("黑体", 25);
                        txt.ReadOnly = true;
                    }
                    else
                    {
                        txt.Font = new Font("宋体", 25);
                        txt.Text = "";
                    }

                    txt.KeyPress += Txt_KeyPress;
                    txt.TextChanged += Txt_TextChanged;

                    textBoxes[r, c] = txt;
                    

                    this.Controls.Add(txt);

                }
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            if (IsFilled())
            {
                if (IsCorrect())
                {
                    MessageBox.Show("恭喜你完成数独！");
                }
                else
                {
                    MessageBox.Show("再检查一下！有问题！");
                }
            }
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if ((e.KeyChar < '1') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
        }

        private bool IsFilled()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (textBoxes[i, j].Text == "")
                        return false;
                }
            }
            return true;
        }

        private bool IsCorrect()
        {
            int[,] Shudu = new int[9, 9];
            bool[,] Hang = new bool[9, 9];
            bool[,] Lie = new bool[9, 9];
            bool[,] Sansan = new bool[9, 9];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int num = int.Parse(textBoxes[i, j].Text);
                    Shudu[i, j] = num;
                    Hang[i, num - 1] = true;
                    Lie[j, num - 1] = true;
                    Sansan[i / 3 * 3 + j / 3, num - 1] = true;
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!Hang[i, j]) return false;
                    if (!Lie[i, j]) return false;
                    if (!Sansan[i, j]) return false;
                }
            }

            return true;
        }

        private void GeneratePuzzles()
        {
            Random rdm = new Random();
            int blank = rdm.Next(30, 60);

            for (int i = 0; i < N && blank > 0; i++)
            {
                for (int j = 0; j < 2 && blank > 0; j++)
                {
                    //int block = rdm.Next(9);
                    int position = rdm.Next(9);
                    int x = i / 3 * 3 + position / 3;
                    int y = i % 3 * 3 + position % 3;
                    if (sudoku.Shudu[x, y] != 0)
                    {
                        sudoku.Hang[x, sudoku.Shudu[x, y] - 1] = false;
                        sudoku.Lie[y, sudoku.Shudu[x, y] - 1] = false;
                        sudoku.Sansan[i, sudoku.Shudu[x, y] - 1] = false;
                        sudoku.Shudu[x, y] = 0;
                        blank--;
                    }
                    else
                    {
                        j--;
                        continue;
                    }
                }
            }
            while (blank > 0)
            {
                int block = rdm.Next(9);
                int position = rdm.Next(9);
                int x = block / 3 * 3 + position / 3;
                int y = block % 3 * 3 + position % 3;
                if (sudoku.Shudu[x, y] != 0)
                {
                    sudoku.Hang[x, sudoku.Shudu[x, y] - 1] = false;
                    sudoku.Lie[y, sudoku.Shudu[x, y] - 1] = false;
                    sudoku.Sansan[block, sudoku.Shudu[x, y] - 1] = false;
                    sudoku.Shudu[x, y] = 0;
                    blank--;
                }
                else
                {
                    continue;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sudoku.GenerateSudokuEnding();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            GeneratePuzzles();
            GenerateAllButtons();
            btn_Start.Enabled = false;
            btn_Reset.Enabled = true;
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
