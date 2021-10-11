using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class Matrix
    {
        private double[,] _matrix;
        public int Rows => _matrix.GetLength(0);
        public int Columns => _matrix.GetLength(1);
        public double this[int i, int j]
        {
            get
            {
                if (i >= Rows || j >= Columns)
                {
                    throw new IndexOutOfRangeException("One of the indexes is out of range.");
                }

                return _matrix[i, j];
            }
            set
            {
                if (i >= Rows || j >= Columns)
                {
                    throw new IndexOutOfRangeException("One of the indexes is out of range.");
                }

                _matrix[i, j] = value;
            }
        }

        public Matrix(double[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix), "Matrix is null");
            }
            _matrix = matrix.Clone() as double[,];
        }

        public Matrix(int rows, int cols)
        {
            if (rows < 0 || cols < 0)
            {
                throw new ArgumentException("Can't initialize matrix with negative sizes.");
            }

            _matrix = new double[rows, cols];
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sb.Append(String.Format("{0, -7:F3}", _matrix[i, j]));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
        public void Fill()
        {
            Random random = new();
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _matrix[i, j] = random.Next(0, 100);
                }
            }
        }
        public MatrixEnumerator GetEnumerator()
        {
            return new MatrixEnumerator(_matrix);
        }
        public class MatrixEnumerator
        {
            public double[,] _matrix;
            private int _xPosition = 0;
            private int _yPosition = -1;

            public MatrixEnumerator(double[,] matrix)
            {
                _matrix = matrix;
            }

            public bool MoveNext()
            {
                _yPosition++;
                if (_yPosition >= _matrix.GetLength(1))
                {
                    _yPosition = 0;
                    _xPosition++;
                }

                return _xPosition < _matrix.GetLength(0) && _yPosition < _matrix.GetLength(1);
            }

            public void Reset()
            {
                _xPosition = 0;
                _yPosition = -1;
            }

            public double Current
            {
                get
                {
                    try
                    {
                        return _matrix[_matrix.GetLength(0) - _xPosition - 1, _matrix.GetLength(1) - _yPosition - 1];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }

    
}
