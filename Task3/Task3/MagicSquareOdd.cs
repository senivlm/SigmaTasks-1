using System;
using System.Text;

namespace Task3
{
    public class MagicSquareOdd
    {
        private int size;
        public int Size
        {
            get => size;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(value)} is less or equal to 0.", $"{nameof(Size)} must be positive number.");
                }
                if (value % 2 == 0)
                {
                    throw new ArgumentException($"{nameof(value)} is even number.", $"{nameof(Size)} must be odd number");
                }
                size = value;
            }

        }
        public MagicSquareOdd(int size)
        {
            this.Size = size;
        }
        public int[,] BuildSquare()
        {
            int[,] square = new int[Size, Size];
            int x = Size / 2, y = Size - 1;
            for (int i = 1; i <= Size * Size;)
            {
                if (x < 0 && y == Size)
                {
                    x = 0;
                    y = Size - 2;
                }
                else
                {
                    if (y == Size)
                    {
                        y = 0;
                    }
                    if (x < 0)
                    {
                        x = Size - 1;
                    }
                }
                if (square[x, y] != 0)
                {
                    y -= 2;
                    x++;
                    continue;
                }
                else
                {
                    square[x, y] = i;
                    i++;
                }
                x--;
                y++;
            }
            return square;
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            int[,] square = BuildSquare();
            for (int i = 0; i < square.GetLength(0); ++i)
            {
                for (int j = 0; j < square.GetLength(1); ++j)
                {
                    sb.Append(square[i, j].ToString().PadRight(8));
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
