using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.GUI
{
    public class CenterAligmentGuiElement : GuiElement
    {
        private IGuiElement _element;
        protected override void OnRectangleChanged()
        {
            base.OnRectangleChanged();
            Rectangle rect = _element.Rectangle;
            rect.X = Rectangle.X + (Rectangle.Width / 2 - rect.Width / 2);
            rect.Y = Rectangle.Y + (Rectangle.Height / 2 - rect.Height / 2);
            _element.Rectangle = rect;
        }
        public CenterAligmentGuiElement(Rectangle outerRectangle, IGuiElement element) : base(false)
        {
            _element = element;
            Rectangle = outerRectangle;
            AddChild(element);
        }
    }
}
