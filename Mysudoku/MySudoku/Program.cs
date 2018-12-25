using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace MySudoku
{
    class Program
    {
        public static int[,] pailie = new int[13890, 8];
        public static int row = 0;
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Sudoku sudoku = new Sudoku();

            string fileName = "sudoku.txt";
            string newPath = System.AppDomain.CurrentDomain.BaseDirectory + fileName;
            StreamWriter streamWriter = new StreamWriter(newPath, false, Encoding.Default);
            int[] order = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            int parseInt = 0;
            int amount = 0;
            Console.WriteLine(args[1].ToString());
            if (args.Length == 2)
            {
                if (args[0] == "-c")
                {
                    if (int.TryParse(args[1], out parseInt))
                    {
                        amount = parseInt;
                        Pretreat(amount);
                        GeneAndTransAndOut(amount,sudoku,order,streamWriter);
                        
                        /* while (amount > 0)
                        {
                            sudoku.Initialize();
                            sudoku.GenerateSudokuEnding();
                            OutputToTxt(sudoku.Shudu, order, streamWriter);
                            amount--;
                            //Console.WriteLine("{0}", amount);
                        }
                        */
                    }
                    else
                    {
                        Console.WriteLine("不合法输入");
                    }
                }
                else if (args[0] == "-s")
                {
                    string path = args[1];
                    string allTheText = "";
                    allTheText = InputSudoku(allTheText, args[1]);

                    string[] all = allTheText.Split(new char[4] { ' ', '\n', '\r', '\0' });

                    int allLength = all.Length;

                    for (int i = 0; i < all.Length;)
                    {
                        sudoku.Initialize();
                        int j = 0;
                        for (j = 0; j < 81; j++)
                        {
                            int hang = j / 9, lie = j % 9;
                            if (i < all.Length)
                            {
                                if (all[i] == "" || all[i] == "\n" || all[i] == "\0" || all[i] == "\r")
                                {
                                    j--;
                                    i++;
                                    continue;
                                }
                                sudoku.Shudu[hang, lie] = int.Parse(all[i]);
                                if (int.Parse(all[i]) != 0)
                                {
                                    sudoku.Hang[hang, int.Parse(all[i]) - 1] = true;
                                    sudoku.Lie[lie, int.Parse(all[i]) - 1] = true;
                                    sudoku.Sansan[hang / 3 * 3 + lie / 3, int.Parse(all[i]) - 1] = true;
                                }
                            }
                            else
                                break;
                            i++;
                        }
                        if (j == 81)
                        {
                            sudoku.SolveSudoku(0);
                            OutputToTxt(sudoku.Shudu,order, streamWriter);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("命令行没有参数！或参数个数不正确！");
                }

                streamWriter.Flush();
                streamWriter.Close();

                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                Console.WriteLine("花费{0}", ts2.TotalSeconds);
            }
        }

        static void GeneAndTransAndOut(int amount,Sudoku sudoku,int[] order,StreamWriter streamWriter)
        {
            for (int i = 0; i < row && amount > 0; i++)
            {
                sudoku.GenerateSudokuEnding2(i);
                for (int p = 0; p < 2 && amount > 0; p++)
                {
                    if (p == 0)
                    {

                    }
                    else
                    {
                        Swap(order, 1, 2);
                    }
                    for (int j = 0; j < 6 && amount > 0; j++)
                    {
                        if (j == 0)
                        {

                        }
                        else if (j % 2 == 1)
                        {
                            Swap(order, 4, 5);
                        }
                        else
                        {
                            Swap(order, 3, 4);
                        }
                        for (int k = 0; k < 6 && amount > 0; k++)
                        {
                            if (k == 0)
                            {

                            }
                            else if (k % 2 == 1)
                            {
                                Swap(order, 7, 8);
                            }
                            else
                            {
                                Swap(order, 6, 7);
                            }

                            OutputToTxt(sudoku.Shudu, order, streamWriter);
                            amount--;
                            //Console.WriteLine(amount.ToString());
                        }
                    }
                }
            }
        }

        static void OutputToTxt(int[,] juzhen, int[] order, StreamWriter streamWriter)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j < 8)
                    {
                        streamWriter.Write(juzhen[order[i], j]);
                        streamWriter.Write(" ");
                    }
                    else
                    {
                        streamWriter.WriteLine(juzhen[order[i], j]);
                    }
                }
            }
            streamWriter.WriteLine();
        }

        static string InputSudoku(string allTheText, string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    allTheText += line;
                    allTheText += " ";
                }
                return allTheText;
                //Console.WriteLine(allTheText);
            }
            else
            {
                Console.WriteLine("数独文件不存在！");
                return "";
            }
        }

        static int GetRandomSeed()
        {
            byte[] bytes = new byte[10];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static void Random159(int[] gene)
        {
            Random rand = new Random(GetRandomSeed());
            for (int i = gene.Length - 1; i > 0; i--)
            {
                int rdmchar = rand.Next(1000) % i;
                int temp = gene[rdmchar];
                gene[rdmchar] = gene[i];
                gene[i] = temp;
            }
        }

        public static void Pretreat(int amount)
        {
            int preamount = amount / 72 + 1;
            int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Permutation(nums, 0, nums.Length, preamount);
        }

        public static void Swap(int[] array,int i,int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        static int count = 0;
        public static void Permutation(int[] nums, int start, int length, int amount)
        {
            int i;
            if (count > amount)
                return;
            else
            {
                if (start < length - 1)
                {
                    Permutation(nums, start + 1, length, amount);
                    if (count > amount)
                        return;
                    for (i = start + 1; i < length; i++)
                    {
                        Swap(nums, start, i);
                        //
                        Permutation(nums, start + 1, length, amount);
                        //
                        if (count > amount)
                            return;
                        Swap(nums, start, i);
                    }
                }
                else
                {
                    count++;
                    for (int j = 0; j < nums.Length; j++)
                    {
                        pailie[row, j] = nums[j];
                    }
                    row++;
                }
            }
        }
    }


}
