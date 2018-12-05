using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.GUI
{
    public class VerticalLayout : GuiElement
    {
        public int Margin { get; set; } = 7;

        public VerticalLayout(Texture2D background, Rectangle rectangle, bool stopPropagation = false) : base(background, rectangle, stopPropagation)
        {
        }

        public VerticalLayout(Rectangle rectangle, bool stopPropagation = false) : base(rectangle, stopPropagation)
        {
        }
        public void MoveToFront(IGuiElement element)
        {
            ChildElements.Remove(element);
            ChildElements.Insert(0, element);
        }
        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            int yoffset = Margin + rectangle.Y;
            ChildElements?.Where(e=>e.IsVisible).ToList().ForEach((e) =>
            {
                var rect = e.Rectangle;
                Rectangle targetRect = new Rectangle(rectangle.X+Margin, yoffset, rectangle.Width-2*Margin, rect.Height);
                yoffset += (Margin + targetRect.Height);
                e.Rectangle = targetRect;
            });
            Rectangle = new Rectangle(Rectangle.Location, new Point(Rectangle.Width,yoffset-rectangle.Y));
            base.Draw(spriteBatch, Rectangle);
        }
    }
}
