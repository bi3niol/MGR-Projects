using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.GUI
{
    public class GuiElement : IGuiElement
    {
        public static Color MaskColor { get; set; } = Color.White;

        public Texture2D Background { get; internal set; }
        public bool StopPropagation { get; set; }
        public IGuiElement Parent { get; set; }
        public List<IGuiElement> ChildElements { get; set; } = new List<IGuiElement>();
        public int LayerDepth { get; set; }

        private Rectangle _rectangle;
        public Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
            set
            {
                if (value == _rectangle)
                    return;

                _rectangle = value;
                OnRectangleChanged();
            }
        }
        public event EventHandler OnMouseDown = (s, e) => { };
        public event EventHandler OnMouseUp = (s, e) => { };
        public event EventHandler OnClick = (s, e) => { Debug.Print($"{s} clicked"); };

        public GuiElement(bool stopPropagation = true)
        {
            StopPropagation = stopPropagation;
        }
        public GuiElement(Rectangle rectangle, bool stopPropagation = true) : this(null, rectangle, stopPropagation)
        { }

        public GuiElement(Texture2D background, Rectangle rectangle, bool stopPropagation = true) : this(stopPropagation)
        {
            Rectangle = rectangle;
            Background = background;
        }
       
        protected virtual void OnRectangleChanged()
        { }

        public bool Click(Point mousePosition)
        {
            if (ChildElements.Any(e => e.Click(mousePosition)))
                return true;

            if (Rectangle.Contains(mousePosition))
            {
                OnClick(this, EventArgs.Empty);
                return StopPropagation;
            }

            return false;
        }

        public void AddChild(IGuiElement element)
        {
            element.Parent = this;
            ChildElements.Add(element);
        }

        public static Texture2D CreateDefaultBackground(GraphicsDevice graphicsDevice, Rectangle rectangle, Color color)
        {
            Texture2D texture2D = new Texture2D(graphicsDevice, rectangle.Size.X, rectangle.Size.Y);
            texture2D.SetData<Color>(Enumerable.Repeat<Color>(color, rectangle.Size.X * rectangle.Size.Y).ToArray());
            return texture2D;
        }
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            if (Background != null)
                spriteBatch.Draw(Background, rectangle, MaskColor);
            ChildElements?.ForEach((e) =>
            {
                e.Draw(spriteBatch);
            });
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Rectangle);
        }

        public virtual void MouseMove(Point mousePosition)
        {
            Debug.Print(mousePosition.ToString());
            ChildElements?.ForEach((e) =>
            {
                e.MouseMove(mousePosition);
            });
        }
    }
}
