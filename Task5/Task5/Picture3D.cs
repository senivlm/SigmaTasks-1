using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public enum ProjectionType { Top, Front, Side }
    public class Picture3D
    {
        public readonly byte[,,] Picture;
        public int Height => Picture.GetLength(0);
        public int Depth => Picture.GetLength(1);
        public int Width => Picture.GetLength(2);
        public Picture3D(int height, int depth, int width)
        {
            if (height <= 0 || depth <= 0 || width <= 0)
            {
                throw new ArgumentException("Dimensions must be greater than 0.");
            }

            Picture = new byte[height, depth, width];
            FillPicture();
        }

        public Picture3D(byte[,,] picture)
        {
            if (picture == null)
            {
                throw new ArgumentNullException(nameof(picture), "Picture is null.");
            }
            Picture = picture.Clone() as byte[,,];
        }

        public byte[][,] GetProjections()
        {
            byte[][,] projections = new byte[3][,];
            projections[0] = GetProjection(ProjectionType.Top);
            projections[1] = GetProjection(ProjectionType.Front);
            projections[2] = GetProjection(ProjectionType.Side);
            return projections;
        }

        public byte[,] GetProjection(ProjectionType type)
        {
            byte[,] projection;
            switch (type)
            {
                case ProjectionType.Top:
                    projection = new byte[Depth, Width];
                    for (int i = 0; i < Height; ++i)
                    {
                        for (int j = 0; j < Depth; ++j)
                        {
                            for (int k = 0; k < Width; ++k)
                            {
                                projection[Depth - j - 1, k] =
                                    Picture[i, k, j] == 1 ? (byte) 1 : projection[Depth - j - 1, k];
                            }
                        }
                    }
                    break;
                case ProjectionType.Front:
                    projection = new byte[Height, Width];
                    for (int i = 0; i < Depth; ++i)
                    {
                        for (int j = 0; j < Height; ++j)
                        {
                            for (int k = 0; k < Width; ++k)
                            {
                                projection[j, k] = Picture[j, k, i] == 1 ? (byte)1 : projection[j, k];
                            }
                        }
                    }
                    break;
                default:
                    projection = new byte[Height, Depth];
                    for (int i = 0; i < Width; ++i)
                    {
                        for (int j = 0; j < Height; ++j)
                        {
                            for (int k = 0; k < Depth; ++k)
                            {
                                projection[j, Depth - k - 1] =
                                    Picture[j, i, k] == 1 ? (byte) 1 : projection[j, Depth - k - 1];
                            }
                        }
                    }
                    break;
            }

            return projection;
        }

        public void FillPicture()
        {
            Random random = new();
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Depth; ++j)
                {
                    for (int k = 0; k < Width; ++k)
                    {
                        Picture[i, j, k] = (byte)random.Next(0, 2);
                    }
                }
            }
        }
    }
}
