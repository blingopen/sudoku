using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[10];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
