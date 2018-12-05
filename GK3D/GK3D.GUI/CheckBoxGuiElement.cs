using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.GUI
{
    public class CheckBoxGuiElement : HorizontalLayout
    {
        public event EventHandler<bool> OnCheckChanged = (s, e) => { };
        private bool _checked;
        public bool Checked
        {
            get => _checked; set
            {
                if (value == _checked)
                    return;

                _checked = value;
                OnCheckedChange(_checked);
            }
        }
        protected Texture2D CheckTexture;
        protected Texture2D UncheckTexture;
        GuiElement _checkElement;
        CenterAligmentGuiElement _labelCenter;
        LabelGuiElement _label;
        public CheckBoxGuiElement(GraphicsDevice device, bool ischecked, LabelGuiElement label, Rectangle rectangle, bool stopPropagation = true) : base(rectangle, stopPropagation)
        {
            var rect = new Rectangle(0, 0, rectangle.Height, rectangle.Height);
            _checkElement = new GuiElement(rect,false);
            _label = label;
            CheckTexture = CreateDefaultBackground(device, rect, Color.Black);
            UncheckTexture = CreateDefaultBackground(device, rect, Color.White);
            Checked = ischecked;

            ///
            OnClick += (s, e) => Checked = !Checked;
            _labelCenter = new CenterAligmentGuiElement(rectangle, label);
            AddChild(_checkElement);
            AddChild(_label);
            OnCheckedChange(ischecked);
        }

        protected void OnCheckedChange(bool newValue)
        {
            _checkElement.Background = newValue ? CheckTexture : UncheckTexture;
            OnCheckChanged(this, newValue);
        }
    }
}
