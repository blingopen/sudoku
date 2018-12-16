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
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Sudoku sudoku = new Sudoku();

            string fileName = "sudoku.txt";
            string newPath = System.AppDomain.CurrentDomain.BaseDirectory + fileName;
            StreamWriter streamWriter = new StreamWriter(newPath, false, Encoding.Default);


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
                        while (amount > 0)
                        {
                            sudoku.Initialize();
                            sudoku.GenerateSudokuEnding();
                            //GetAnswer(0, sudoku1);
                            OutputToTxt(sudoku, streamWriter);
                            amount--;
                            //Console.WriteLine("{0}", amount);
                        }
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
                            OutputToTxt(sudoku, streamWriter);
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

        static void OutputToTxt(Sudoku sudoku, StreamWriter streamWriter)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j < 8)
                    {
                        streamWriter.Write(sudoku.Shudu[i, j] + " ");
                    }
                    else
                    {
                        streamWriter.WriteLine(sudoku.Shudu[i, j]);
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
            //Thread.Sleep(1);
            Random rand = new Random(GetRandomSeed());
            for (int i = gene.Length - 1; i > 0; i--)
            {
                int rdmchar = rand.Next(1000) % i;
                int temp = gene[rdmchar];
                gene[rdmchar] = gene[i];
                gene[i] = temp;
            }
        }

        /*static public bool Solve(Sudoku sudoku, int position)
        {
            int index = position;
            for (; index < 81 && 0 != sudoku.Shudu[index / 9,index % 9]; index++) ;

            if (index < 81)
            {
                int line = index / 9;
                int col = index % 9;
                for (int i = 1; i < 10; i++)
                {
                    if (sudoku.Hang[line,i - 1] || sudoku.Lie[col,i - 1] || sudoku.Sansan[line / 3 * 3 + col / 3,i - 1])
                        continue;

                    sudoku.Shudu[line,col] = i;
                    sudoku.Hang[line,i - 1] = true;
                    sudoku.Lie[col,i - 1] = true;
                    sudoku.Sansan[line / 3 * 3 + col / 3,i - 1] = true;

                    if (Solve(sudoku, index + 1))
                        return true;

                    sudoku.Shudu[line, col] = 0;
                    sudoku.Hang[line, i - 1] = false;
                    sudoku.Lie[col, i - 1] = false;
                    sudoku.Sansan[line / 3 * 3 + col / 3, i - 1] = false;
                }
                return false;
            }
            return true;
        }
        */
    }


}
