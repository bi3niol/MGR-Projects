using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectLAB.UserControls.Extensions
{
    public static class ColorExtensions
    {
        public static Color GetGrey(this Color c)
        {
            byte val = (byte)((3 * c.R + 5 * c.G + c.B) / 9);
            return Color.FromRgb(val, val, val);
        }

        public static Color BlacOrWite(this Color c, int threshold = 126)
        {
            byte val = (byte)((3 * c.R + 5 * c.G + c.B) / 9);
            return val < threshold ? Colors.Black : Colors.White;
        }

        //public static Color Add(this Color c, Color other)
        //{
        //    return val < 126 ? Colors.Black : Colors.White;
        //}
        //public static Color Sub(this Color c, Color other)
        //{
        //    byte val = (byte)((3 * c.R + 5 * c.G + c.B) / 9);
        //    return val < 126 ? Colors.Black : Colors.White;
        //}
    }
}
