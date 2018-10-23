using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class ArrayFilter : IImageOperation
    {
        public string Name => throw new NotImplementedException();
        public float[,] Data { get; set; } = new float[3, 3];
        public bool EditEnable { get; set; }

        public string a00 { get=>this[0,0]; set=>this[0,0]=value; }
        public string a01 { get=>this[0,1]; set=>this[0,1]=value; }
        public string a02 { get=>this[0,2]; set=>this[0,2]=value; }
        public string a10 { get=>this[1,0]; set=>this[1,0]=value; }
        public string a11 { get=>this[1,1]; set=>this[1,1]=value; }
        public string a12 { get=>this[1,2]; set=>this[1,2]=value; }
        public string a20 { get=>this[2,0]; set=>this[2,0]=value; }
        public string a21 { get=>this[2,1]; set=>this[2,1]=value; }
        public string a22 { get=>this[2,2]; set=>this[2,2]=value; }

        public string this[int i, int j]
        {
            get
            {
                return Data[i, j].ToString();
            }
            set
            {
                float v;
                if (float.TryParse(value, out v))
                    Data[i, j] = v;
            }
        }
        public ArrayFilter(float[,] data)
        {
            Data = data;
        }
        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {

            int dx = Data.GetLength(0) / 2;

            float d = 0;
            for (int i = 0; i < Data.GetLength(0); i++)
                for (int j = 0; j < Data.GetLength(1); j++)
                    d += Data[i, j];
            d = d != 0 ? 1 / d : 1;

            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    Color c = Colors.Black;

                    for (int i = -dx; i <= dx; i++)
                        for (int j = -dx; j <= dx; j++)
                            c += sourceImage[x + i, y + j] * Data[i + dx, j + dx];
                    c *= d;
                    targetImage[x, y] = c;
                }
        }


        public static float[,] LOWERPROOFFILTER_BLUR_N => _LOWERPROOFFILTER_BLUR_N.Clone() as float[,];
        public static float[,] LOWERPROOFFILTER_GAUSS => _LOWERPROOFFILTER_GAUSS.Clone() as float[,];
        public static float[,] UPPERPROOFFILTER_MEAN_REMOVAL => _UPPERPROOFFILTER_MEAN_REMOVAL.Clone() as float[,];
        public static float[,] EDGE_DETECT_VERTICAL_FILTER => _EDGE_DETECT_VERTICAL_FILTER.Clone() as float[,];
        public static float[,] EDGE_DETECT_LAPLACE_FILTER => _EDGE_DETECT_LAPLACE_FILTER.Clone() as float[,];
        public static float[,] EDGE_DETECT_HORIZONTAL_FILTER => _EDGE_DETECT_HORIZONTAL_FILTER.Clone() as float[,];
        public static float[,] EDGE_DETECT_DIAGONAL_FILTER => _EDGE_DETECT_DIAGONAL_FILTER.Clone() as float[,];
        public static float[,] SCULPTURE_EAST_FILTER => _SCULPTURE_EAST_FILTER.Clone() as float[,];
        public static float[,] SCULPTURE_SOUTH_EAST_FILTER => _SCULPTURE_SOUTH_EAST_FILTER.Clone() as float[,];
        public static float[,] DEFAULT_FILTER => _DEFAULT_FILTER.Clone() as float[,];


        private static float[,] _DEFAULT_FILTER = new float[,]{
            {0, 0, 0 },
            {0, 1, 0 },
            {0, 0, 0 },
        };

        private static float[,] _LOWERPROOFFILTER_BLUR_N = new float[,]{
            {1,1,1,1,1 },
            {1,1,1,1,1 },
            {1,1,1,1,1 },
            {1,1,1,1,1 },
            {1,1,1,1,1 },
        };

        private static float[,] _LOWERPROOFFILTER_GAUSS = new float[,]{
            {0, 1, 2, 1, 0 },
            {1, 4, 8, 4, 1 },
            {2 ,8, 16, 8, 2},
            {1, 4, 8, 4, 1 },
            {0, 1, 2, 1, 0 },
        };

        private static float[,] _UPPERPROOFFILTER_MEAN_REMOVAL = new float[,]{
            { -1, -1, -1 },
            { -1, 9, -1 },
            { -1, -1, -1 },
        };

        private static float[,] _EDGE_DETECT_LAPLACE_FILTER = new float[,]{
            { 0, -1, 0 },
            { -1, 4, -1 },
            { 0, -1, 0 },
        };

        private static float[,] _EDGE_DETECT_VERTICAL_FILTER = new float[,]{
            { 0, -1, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 },
        };

        private static float[,] _EDGE_DETECT_HORIZONTAL_FILTER = new float[,]{
            { 0, 0, 0 },
            { -1, 1, 0 },
            { 0, 0, 0 },
        };

        private static float[,] _EDGE_DETECT_DIAGONAL_FILTER = new float[,]{
            { -1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 },
        };

        //sculpture
        private static float[,] _SCULPTURE_EAST_FILTER = new float[,]
        {
            {-1, 0, 1 },
            {-1, 1, 1 },
            {-1, 0, 1 },
        };

        private static float[,] _SCULPTURE_SOUTH_EAST_FILTER = new float[,]
        {
            {-1, -1, 0 },
            {-1, 1, 1 },
            {0 , 1, 1 },
        };
    }
}
