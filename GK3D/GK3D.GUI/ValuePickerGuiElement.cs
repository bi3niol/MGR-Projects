using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GK3D.GUI
{
    public class ValuePickerGuiElement : HorizontalLayout
    {
        public event EventHandler<float> OnValueChange = (s, e) => { };
        private float _value;
        public float Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                ValueChenged(_value);
            }
        }
        private float _deltaValue;
        private LabelGuiElement _label;
        public ValuePickerGuiElement(ButtonGuiElement decrementButton, ButtonGuiElement incrementButton, LabelGuiElement label, Rectangle rectangle, float deltaValue = 0.1f, float initValue = 0, bool stopPropagation = true) : base(rectangle, stopPropagation)
        {
            incrementButton.OnClick += IncrementValue;
            decrementButton.OnClick += DecrementValue;
            incrementButton.Rectangle = new Rectangle(new Point(), new Point(rectangle.Height, rectangle.Height));
            decrementButton.Rectangle = new Rectangle(new Point(), new Point(rectangle.Height, rectangle.Height));
            _label = label;
            _deltaValue = deltaValue;
            Value = initValue;
            AddChild(decrementButton);
            AddChild(new CenterAligmentGuiElement(new Rectangle(new Point(), new Point(rectangle.Width - 2 * rectangle.Height, rectangle.Height)), _label));
            AddChild(incrementButton);
        }
        protected void ValueChenged(float value)
        {
            _label.Text = value.ToString();
            OnValueChange(this, value);
        }
        private void IncrementValue(object sender, EventArgs e) =>
            Value += _deltaValue;
        private void DecrementValue(object sender, EventArgs e) =>
            Value -= _deltaValue;
    }
}
