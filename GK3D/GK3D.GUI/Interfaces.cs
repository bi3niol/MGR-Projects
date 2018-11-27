using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.GUI
{
    public interface IGuiElement : IClicableElement
    {
        IGuiElement Parent { get; set; }
        List<IGuiElement> ChildElements { get; set; }
        void AddChild(IGuiElement element);
        int LayerDepth { get; set; }
        void Draw(SpriteBatch spriteBatch, Rectangle rectangle);
        void Draw(SpriteBatch spriteBatch);
    }
    public interface IAnimatedGuiElement : IGuiElement, IUpdateable { }
    public interface IClicableElement
    {
        event EventHandler OnMouseDown;
        event EventHandler OnMouseUp;
        event EventHandler OnClick;

        bool StopPropagation { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns>true if propagation stopped</returns>
        bool Click(Point mousePosition);
        void MouseMove(Point mousePosition);
        Rectangle Rectangle { get; set; }
    }
}
