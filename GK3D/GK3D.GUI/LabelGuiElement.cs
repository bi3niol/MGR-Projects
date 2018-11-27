using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.GUI
{
    public class LabelGuiElement : GuiElement
    {
        public string Text { get; set; } = "";

        public Point _position;
        public Point Position
        {
            get => _position;
            set
            {
                if (_position == value)
                    return;
                _position = value;
                Rectangle = new Rectangle(_position, _font.MeasureString(Text).ToPoint());
            }
        }
        private SpriteFont _font;
        public SpriteFont Font
        {
            get
            {
                return _font;
            }
            set
            {
                if (_font == value)
                    return;

                _font = value;
                Rectangle = new Rectangle(Position, _font.MeasureString(Text).ToPoint());
            }
        }
        public LabelGuiElement(string text, SpriteFont font, Vector2 position, bool stopPropagation = false) : base(stopPropagation)
        {
            Text = text;
            Font = font;
            Position = position.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            base.Draw(spriteBatch, rectangle);
            spriteBatch.DrawString(Font, Text, Rectangle.Location.ToVector2(), MaskColor);
        }
    }
}
