using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibMas1
{
    public class ArrayHelper
    {
        public static void AddArrayItems(int[,] MasArray, int minVal, int maxVal)
        {
            Random rnd = new Random();

            for (int i = 0; i < MasArray.GetLength(0); i++)
            {
                for (int j = 0; j < MasArray.GetLength(1); j++)
                {
                    MasArray[i, j] = rnd.Next(minVal, maxVal);
                }
            }
        }

        public static void ClearArrayItems(int[,] MasArray, int minVal, int maxVal)
        {
            for (int i = 0; i < MasArray.GetLength(0); i++)
            {
                for (int j = 0; j < MasArray.GetLength(1); j++)
                {
                    MasArray[i, j] = 0;
                }
            }
            minVal = 0;
            maxVal = 0;
        }

        public static void CalculatePlus(int[,] MasArray, out string[] StrMas, out int summa)
        {
            summa = 0;
            int count = MasArray.GetLength(0) * MasArray.GetLength(1);
            StrMas = new string[count];
            int index = 0;
            for (int i = 0; i < MasArray.GetLength(0); i++)
            {
                for (int j = 0; j < MasArray.GetLength(1); j++)
                {
                    StrMas[index] = Convert.ToString(MasArray[i, j]);
                    index++;
                }
            }
            for (int i = 0; i < StrMas.GetLength(0); i++)
            {
                summa += Convert.ToInt32(StrMas[i]);
            }
        }

        public static double CalculateSrAr(int[,] MasArray)
        {
            int count = MasArray.GetLength(0) * MasArray.GetLength(1);
            double SrAr = 0;
            for (int i = 0; i < MasArray.GetLength(0); i++)
            {
                for (int j = 0; j < MasArray.GetLength(1); j++)
                {
                    SrAr += MasArray[i, j];
                }
            }
            SrAr = SrAr / count;
            return SrAr;
        }

        public static void CalculateMinus(int[,] MasArray, out string[] StrMasMin, out int raznost)
        {
            raznost = 0;
            int count = MasArray.GetLength(0) * MasArray.GetLength(1);
            StrMasMin = new string[count];
            int index = 0;
            for (int i = 0; i < MasArray.GetLength(0); i++)
            {
                for (int j = 0; j < MasArray.GetLength(1); j++)
                {
                    StrMasMin[index] = Convert.ToString(MasArray[i, j]);
                    index++;
                }
            }
            for (int i = 0; i < StrMasMin.GetLength(0); i++)
            {
                raznost -= Convert.ToInt32(StrMasMin[i]);
            }
        }

        public static void CalculateUmn(int[,] MasArray, out string[] StrMasUmn, out int proizved)
        {
            int count = MasArray.GetLength(0) * MasArray.GetLength(1);
            StrMasUmn = new string[count];
            int index = 0;
            proizved = 1;
            for (int i = 0; i < MasArray.GetLength(0); i++)
            {
                for (int j = 0; j < MasArray.GetLength(1); j++)
                {
                    StrMasUmn[index] = Convert.ToString(MasArray[i, j]);
                    index++;
                }
            }
            for (int i = 0; i < StrMasUmn.GetLength(0); i++)
            {
                proizved *= Convert.ToInt32(StrMasUmn[i]);
            }
        }

        public static int FindMaxOfMinInRows(int[,] MasArray)
        {
            int rows = MasArray.GetLength(0);
            int cols = MasArray.GetLength(1);
            int maxOfMin = int.MinValue;
            for (int i = 0; i < rows; i++)
            {
                int minInRow = int.MaxValue;
                for (int j = 0; j < cols; j++)
                {
                    if (MasArray[i, j] < minInRow)
                    {
                        minInRow = MasArray[i, j];
                    }
                }

                if (minInRow > maxOfMin)
                {
                    maxOfMin = minInRow;
                }
            }
            return maxOfMin;
        }
    }
}