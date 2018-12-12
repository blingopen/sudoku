using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MySudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();



            Sudoku sudoku = new Sudoku();
            sudoku.GenerateSudokuEnding();
            
            string fileName = "sudoku.txt";
            string newPath = System.AppDomain.CurrentDomain.BaseDirectory + fileName;
            Console.WriteLine(newPath);
            StreamWriter streamWriter = new StreamWriter(newPath, false, Encoding.Default);

            int parseInt = 0;
            int amount = 0;

            if (args.Length == 2)
            {
                if(args[0] == "-c")
                {
                    if(int.TryParse(args[1],out parseInt))
                    {
                        amount = parseInt;
                        while (amount > 0)
                        {
                            OutputToTxt(sudoku,streamWriter);
                            amount--;
                            Console.WriteLine("{0}", amount);
                        }
                    }
                    else
                    {
                            Console.WriteLine("不合法输入");
                    }
                }
                else if (args[0] == "-s")
                {
                    //InputSudoku(sudoku);
                    sudoku.SovleSudoku();
                    Console.WriteLine("这个功能还没做！");
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
       static void OutputToTxt(Sudoku sudoku,StreamWriter streamWriter)
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

       //static Sudoku InputSudoku(Sudoku sudoku)
        //{
            
       // }
    }
}
