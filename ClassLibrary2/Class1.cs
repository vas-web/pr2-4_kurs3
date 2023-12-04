using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 

namespace LibMas1
{
    public class Class1
    {
        public int massiv(int m, int n, int rnd, int OT, int DO, int[,] mass)
        {

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mass[i, j] = rnd.Next(OT, DO);
                }
            }
        }
    }
}
