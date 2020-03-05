using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using System.

namespace nonograms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(400, 400);
        }

        private Graphics g;
        private Bitmap b;
        private int n = 0, m = 0;
        private int size1 = 5;
        private void drawCanvas()
        {
            b = new Bitmap(1500, 1500);
            g = Graphics.FromImage(b);

            Pen pen = new Pen(Color.White);
            g.FillRectangle(new SolidBrush(Color.White), 8, 48, size1 * m + 5, size1 * n + 5);
           
            pen = new Pen(Color.Black);
            for (int i = 0; i <= m; i++)
                g.DrawLine(pen, size1 * i + 10, 50, size1 * i + 10, size1 * n + 50);
            for (int i = 0; i <= n; i++)
                g.DrawLine(pen, 10, i * size1 + 50, size1 * m + 10, size1 * i + 50);

            //this.Size.Height = 60 + size1 * n;
            //this.Size.Width = 20 + size1 * m;
            this.Size = new Size(37 + size1 * m, 100 + size1 * n);
            this.BackgroundImage = b;
        }

        private void bResolution_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver(ah, av);

            DateTime dt1 = DateTime.Now;
            List<List<bool>> solve = solver.getSolve();
            DateTime dt2 = DateTime.Now;

            drawSolve(solve);

            MessageBox.Show("Время выполнения алгоритма: " + (dt2 - dt1).ToString());

        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            openFile();
            drawCanvas();
        }

        private void drawSolve(List<List<bool>> solve)
        {
            Brush brush = new SolidBrush(Color.Black);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (solve[i][j])
                        g.FillRectangle(brush, size1 * j + 12, size1 * i + 52, size1 - 3, size1 - 3);
            this.BackgroundImage = b;
            this.Refresh();
        }

        List<List<int>> ah;
        List<List<int>> av;
        private void openFile()
        {
            StreamReader sr = new StreamReader(@"C:\VS\nonograms\nonograms\2.txt");
            string s = sr.ReadLine();
            string[] tmp = s.Split(' ');
            n = int.Parse(tmp[0]);
            m = int.Parse(tmp[1]);
            ah = new List<List<int>>(n);
            av = new List<List<int>>(n);

            for (int i = 0; i < n; i++)
            {
                tmp = sr.ReadLine().Trim().Split(' ');
                ah.Add(new List<int>());
                for (int j = 0; j < tmp.Length; j++)
                {
                    ah[i].Add(int.Parse(tmp[j]));
                    Console.Write(ah[i][j].ToString(), " ");
                }
            }
            for (int i = 0; i < m; i++)
            {
                tmp = sr.ReadLine().Split(' ');
                av.Add(new List<int>());
                for (int j = 0; j < tmp.Length; j++)
                    av[i].Add(int.Parse(tmp[j]));
            }

        }
    }
}
