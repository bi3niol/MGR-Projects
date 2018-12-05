using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GK3D.GUI
{
    public class SelectOptionGuiElement<TValue> : CenterAligmentGuiElement
    {
        public TValue Value { get; set; }
        public SelectOptionGuiElement(Rectangle outerRectangle, LabelGuiElement label, TValue value) : base(outerRectangle, label)
        {
            Value = value;
        }
    }
}
