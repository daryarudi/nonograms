using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nonograms
{
    class Solver
    {
        private List<List<int>> ah, av;
        private List<List<List<bool>>> eqh, eqv;
        private List<List<bool>> check;
        private List<List<bool>> solve;
        private int m, n;

        public Solver(List<List<int>> horizontal, List<List<int>> vertical)
        {
            ah = horizontal;
            av = vertical;
            n = ah.Count();
            m = av.Count();
        }

        private void buildEquations() // пробегает по всем строкам и столбцам
        {
            eqh = new List<List<List<bool>>>();
            eqv = new List<List<List<bool>>>();
            for (int i = 0; i < n; i++)
                eqh.Add(eqHorizontal(i));
            for (int i = 0; i < m; i++)
                eqv.Add(eqVertical(i));
        }

        private List<List<bool>> eqHorizontal(int num) // строит набор дизъюнктов для одной строки
        {
            List<List<bool>> res = new List<List<bool>>();
            // составление первой позиции
            List<bool> cur = new List<bool>();
            for (int i = 0; i < ah[num].Count(); i++)
            {
                for (int j = 0; j < ah[num][i]; j++)
                    cur.Add(true);
                if (i != ah[num].Count() - 1)
                    cur.Add(false);
            }
            int tmp = cur.Count;
            for (int i = 0; i < m - tmp; i++)
                cur.Add(false);
            // составление уравнения
            do
            {
                res.Add(cur);
                cur = nextDis(cur);
            } while (cur != null);
            return res;
        }
        private List<List<bool>> eqVertical(int num) // строит набор дизъюнктов для одной строки
        {
            List<List<bool>> res = new List<List<bool>>();
            // составление первой позиции
            List<bool> cur = new List<bool>();
            for (int i = 0; i < av[num].Count(); i++)
            {
                for (int j = 0; j < av[num][i]; j++)
                    cur.Add(true);
                if (i != av[num].Count() - 1)
                    cur.Add(false);
            }
            int tmp = cur.Count;
            for (int i = 0; i < n - tmp; i++)
                cur.Add(false);
            // составление уравнения
            do
            {
                res.Add(cur);
                cur = nextDis(cur);
            } while (cur != null);
            return res;
        }

        private List<bool> nextDis(List<bool> dis) // составление следующей позиции элементов
        {
            List<bool> res = dis.GetRange(0, dis.Count);
            //поиск последней единицы
            int pos = -1;
            int i = res.Count() - 1;
            while (i >= 0 && pos == -1)
            {
                if (res[i] && i != res.Count() - 1 && ((i == res.Count() - 2 && res[i + 1] == false) || (res[i + 1] == false && res[i + 2] == false)))
                        pos = i;
                else
                    i--;
            }
            if (i == -1)
                return null;
            while(i >= 0 && res[i] == true)
            {
                res[i + 1] = true;
                res[i] = false;
                i--;
            }
            pos+=3;
            i = pos;
            while (i < res.Count())
            {
                if (res[i] == true)
                {
                    while (i < res.Count() && res[i] == true)
                    {
                        res[i] = false;
                        res[pos] = true;
                        pos++;
                        i++;
                    }
                    pos++;
                }
                else
                    i++;
            }
            return res;
        }

        public List<List<bool>> getSolve()
        {
            solver();
            return solve;
        }
        private void solver()
        {
            // инициализация матрицы решения
            check = new List<List<bool>>();
            solve = new List<List<bool>>();
            for (int i = 0; i < n; i++)
            {
                List<bool> tmp1 = new List<bool>();
                List<bool> tmp2 = new List<bool>();
                for (int j = 0; j < m; j++)
                {
                    tmp1.Add(false);
                    tmp2.Add(false);
                }
                check.Add(tmp1);
                solve.Add(tmp2);
            }

            buildEquations();

            bool tmp;
            bool delta = true;
            while (!checkSolve() && delta) // пока решение не найдено
            {
                delta = false;
                // для горизонтальных
                for (int i = 0; i < n; i++)
                {
                    if (eqh[i].Count() == 1)
                    {
                        solveHorizontal(i);
                        delta = true;
                    }
                    else if (eqh[i].Count() > 1)
                        for (int j = 0; j < m; j++)
                            if (!check[i][j])
                            {
                                tmp = true;
                                int k = 0;
                                while (tmp == true && k < eqh[i].Count - 1)
                                {
                                    if (eqh[i][k][j] != eqh[i][k + 1][j])
                                        tmp = false;
                                    k++;
                                }
                                if (tmp)
                                {
                                    solveCell(i, j, eqh[i][0][j]);
                                    delta = true;
                                }
                            }
                }
                // для вертикальных
                for (int i = 0; i < m; i++)
                {
                    if (eqv[i].Count() == 1)
                    {
                        solveVertical(i);
                        delta = true;
                    }
                    else if (eqv[i].Count() > 1)
                        for (int j = 0; j < n; j++)
                            if (!check[j][i])
                            {
                                tmp = true;
                                int k = 0;
                                while (tmp == true && k < eqv[i].Count - 1)
                                {
                                    if (eqv[i][k][j] != eqv[i][k + 1][j])
                                        tmp = false;
                                    k++;
                                }
                                if (tmp)
                                {
                                    solveCell(j, i, eqv[i][0][j]);
                                    delta = true;
                                }
                            }
                }
                // исключение неподходящиъ дизъюнктов из уравнений
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        if (check[i][j])
                        {
                            int k = 0;
                            while (k < eqh[i].Count)
                                if (eqh[i][k][j] != solve[i][j])
                                    eqh[i].RemoveAt(k);
                                else
                                    k++;
                            k = 0;
                            while (k < eqv[j].Count)
                                if (eqv[j][k][i] != solve[i][j])
                                    eqv[j].RemoveAt(k);
                                else
                                    k++;
                        }
                    }

                // проверка дельты на 0 (если ни одного изменения, заканчиваем цикл, начинаем бэктрек)
            }
        }

        private bool checkSolve()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (!check[i][j])
                        return false;
            return true;
        }
        private void solveHorizontal(int num)
        {
            for (int i = 0; i < m; i++)
                solveCell(num, i, eqh[num][0][i]);
            eqh[num].Clear();
        }
        private void solveVertical(int num)
        {
            for (int i = 0; i < n; i++)
                solveCell(i, num, eqv[num][0][i]);
            eqv[num].Clear();
        }
        private void solveCell(int x, int y, bool value)
        {
            // здесь должна быть проверка на коллизии
            check[x][y] = true;
            solve[x][y] = value;
        }
    }
}
