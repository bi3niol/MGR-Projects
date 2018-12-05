using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.GUI
{
    public class SelectGuiElement<TValue> : GuiElement
    {
        public event EventHandler<TValue> OnSelectionChanged = (s, e) => { Debug.Print($"{s} on Selection changed"); };
        public TValue SelectedValue
        {
            get
            {
                if (SelectedOption != null)
                    return SelectedOption.Value;
                return default(TValue);
            }
        }

        private SelectOptionGuiElement<TValue> _selectedOption;
        private SelectOptionGuiElement<TValue> SelectedOption
        {
            get
            {
                return _selectedOption;
            }
            set
            {
                OptionsLayout.IsVisible = !OptionsLayout.IsVisible;

                if (_selectedOption == value)
                    return;

                _selectedOption = value;
                SelectionChanged(SelectedValue);
            }
        }
        private VerticalLayout OptionsLayout;
        public SelectGuiElement(Texture2D background, Rectangle rectangle, SelectOptionGuiElement<TValue> defaultElement,
            params SelectOptionGuiElement<TValue>[] options) : base(rectangle)
        {
            OptionsLayout = new VerticalLayout(background, rectangle, true);
            if (options != null)
                foreach (var opt in options)
                {
                    opt.OnClick += OnOptionClick;
                    OptionsLayout.AddChild(opt);
                }
            SelectedOption = defaultElement;
            OptionsLayout.IsVisible = false;
            AddChild(OptionsLayout);
        }

        protected override void OnRectangleChanged()
        {
            base.OnRectangleChanged();
            if (OptionsLayout != null)
                OptionsLayout.Rectangle = Rectangle;
        }
        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            SelectedOption?.Draw(spriteBatch, rectangle);
            OptionsLayout?.Draw(spriteBatch);
        }

        public override bool Click(Point mousePosition)
        {
            if (OptionsLayout.IsVisible)
                return OptionsLayout.Click(mousePosition);
            else
                return SelectedOption.Click(mousePosition);
        }

        private void OnOptionClick(object sender, EventArgs e)
        {
            var option = (SelectOptionGuiElement<TValue>)sender;
            SelectedOption = option;
            OptionsLayout.MoveToFront(SelectedOption);
        }
        protected virtual void SelectionChanged(TValue value)
        {
            OnSelectionChanged(this, value);
        }
    }
}
