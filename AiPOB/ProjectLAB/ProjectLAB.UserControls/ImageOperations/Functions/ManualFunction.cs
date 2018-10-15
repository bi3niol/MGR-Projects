using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectLAB.UserControls.ImageOperations.Functions
{
    public class ManualFunction : FunctionOperation
    {
        public override string Name => "ManualFunction";
        public byte[] Values { get; set; }
        public ManualFunction(byte[] values):base()
        {
            Values = values;
            Random r = new Random();
            r.NextBytes(Values);
            this.TransformFunc = GetColor;
        }
        private Color GetColor(Color color)
        {
            return Color.FromRgb(Values[color.R], Values[color.G], Values[color.B]);
        }
    }
}
