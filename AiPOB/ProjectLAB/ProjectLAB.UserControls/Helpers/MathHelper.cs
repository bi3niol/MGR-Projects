using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLAB.UserControls.Helpers
{
    public static class MathHelper
    {
        public static int Limit(int number, int lower = 0, int upper = 255)
        {
            number = number < lower ? lower : number > upper ? upper : number;
            return number;
        }
    }
}
