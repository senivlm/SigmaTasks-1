using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public static class ArrayExtensions
    {
        public static string ToMatrixView(this byte[,] matrix)
        {
            StringBuilder sb = new();
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    sb.Append(String.Format("{0, -4}", matrix[i, j]));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
