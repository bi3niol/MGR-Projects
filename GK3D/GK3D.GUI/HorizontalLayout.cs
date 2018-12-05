using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.GUI
{
    public class HorizontalLayout : GuiElement
    {
        public int Margin { get; set; } = 7;
        public HorizontalLayout(Rectangle rectangle, bool stopPropagation = false) : base(rectangle, stopPropagation)
        {
        }
        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            int xoffset = Margin + rectangle.X;
            ChildElements?.ForEach((e) =>
            {
                var rect = e.Rectangle;
                Rectangle targetRect = new Rectangle(xoffset, rectangle.Y, rect.Width, rectangle.Height);
                xoffset += (Margin + targetRect.Width);
                e.Rectangle = targetRect;
            });
            Rectangle = new Rectangle(Rectangle.Location, new Point(xoffset - rectangle.X, Rectangle.Height));
            base.Draw(spriteBatch, Rectangle);
        }
    }
}
