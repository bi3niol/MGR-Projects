using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.GUI
{
    public class ButtonGuiElement : GuiElement
    {
        public Texture2D HoverBackground { get; set; }
        public Texture2D NormalBackground { get; set; }
        private IGuiElement label;
        public ButtonGuiElement(Texture2D background, Rectangle rectangle, string text, SpriteFont font, Texture2D hoverBackground = null, bool stopPropagation = true) : base(background, rectangle, stopPropagation)
        {
            HoverBackground = hoverBackground;
            NormalBackground = background;

            label = new CenterAligmentGuiElement(rectangle, new LabelGuiElement(text, font, rectangle.Location.ToVector2()));
            AddChild(label);
        }
        public override void MouseMove(Point mousePosition)
        {
            if (Rectangle.Contains(mousePosition))
                Background = HoverBackground;
            else
                Background = NormalBackground;
            base.MouseMove(mousePosition);
        }
        protected override void OnRectangleChanged()
        {
            base.OnRectangleChanged();
            if (label != null)
                label.Rectangle = Rectangle;
        }
    }
}
