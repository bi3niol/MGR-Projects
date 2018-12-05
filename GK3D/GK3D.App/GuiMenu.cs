using GK3D.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.App
{

    internal struct WindowResolution
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsFullScreen { get; set; }
        public WindowResolution(int width, int height, bool isFullScreen = false)
        {
            Width = width;
            Height = height;
            IsFullScreen = isFullScreen;
        }
        public static WindowResolution Window800x600 { get; } = new WindowResolution(800, 600);
        public static WindowResolution Window1900x1024 { get; } = new WindowResolution(1900, 1024);
        public static WindowResolution Window1440x900 { get; } = new WindowResolution(1440, 900, false);
    }

    internal class GuiMenu : DrawableGameComponent
    {
        private CenterAligmentGuiElement _guiElement;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        public event EventHandler<bool> MultiSampleAntiAliasingChange
        {
            add
            {
                MultiSamplingChange.OnCheckChanged += value;
            }
            remove
            {
                MultiSamplingChange.OnCheckChanged -= value;
            }
        }

        public CheckBoxGuiElement MultiSamplingChange;
        public CheckBoxGuiElement LinearMagFilterCheckBox;
        public CheckBoxGuiElement LinearMipFilterCheckBox;
        public ValuePickerGuiElement MinMapLevelOfDetailsBiasPicker;
        public SelectGuiElement<WindowResolution> Select;
        public GuiMenu(SpriteBatch spriteBatch, SpriteFont font, Game game) : base(game)
        {
            //InputHandler.IsPointInGameWindow = game.Window.ClientBounds.Contains;
            _spriteBatch = spriteBatch;
            _font = font;
            LabelGuiElement multisamplingLabel = new LabelGuiElement("MSAA", _font, new Vector2());
            LabelGuiElement LinearFilterLabel = new LabelGuiElement("Linear Mag Filter", _font, new Vector2());
            LabelGuiElement TrilinearMipmapLabel = new LabelGuiElement("Linear Mip Filter", _font, new Vector2());
            MultiSamplingChange = new CheckBoxGuiElement(game.GraphicsDevice, false, multisamplingLabel, new Rectangle(0, 0, 100, 20));
            LinearMagFilterCheckBox = new CheckBoxGuiElement(game.GraphicsDevice, false, LinearFilterLabel, new Rectangle(0, 0, 100, 20));
            LinearMipFilterCheckBox = new CheckBoxGuiElement(game.GraphicsDevice, false, TrilinearMipmapLabel, new Rectangle(0, 0, 100, 20));
            Rectangle rec = new Rectangle(100, 100, 200, 40);

            ButtonGuiElement incrementButton = new ButtonGuiElement(game.Content.Load<Texture2D>("menu/TAK15"), rec, "+", _font, game.Content.Load<Texture2D>("menu/TAK17"));
            ButtonGuiElement decrementButton = new ButtonGuiElement(game.Content.Load<Texture2D>("menu/TAK15"), rec, "-", _font, game.Content.Load<Texture2D>("menu/TAK17"));
            LabelGuiElement valueLabel = new LabelGuiElement("", _font, new Vector2());
            MinMapLevelOfDetailsBiasPicker = new ValuePickerGuiElement(decrementButton, incrementButton, valueLabel, rec, 1);

            IGuiElement guiElement = new VerticalLayout(game.Content.Load<Texture2D>("menu/TAK11"), new Rectangle(100, 100, 235, 200), false);

            GuiElement element = new VerticalLayout(game.Content.Load<Texture2D>("menu/TAK15"), new Rectangle());
            ////
            Rectangle optRect = rec;
            optRect.Height = 20;
            SelectOptionGuiElement<WindowResolution> defW = new SelectOptionGuiElement<WindowResolution>(optRect,
                new LabelGuiElement("Domyslny rozmiar", _font, new Vector2()), new WindowResolution(game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight,
                game.GraphicsDevice.PresentationParameters.IsFullScreen));
            SelectOptionGuiElement<WindowResolution> opt1 = new SelectOptionGuiElement<WindowResolution>(optRect,
                new LabelGuiElement("800 x 600", _font, new Vector2()), WindowResolution.Window800x600);
            SelectOptionGuiElement<WindowResolution> opt2 = new SelectOptionGuiElement<WindowResolution>(optRect,
                new LabelGuiElement("1900 x 1024", _font, new Vector2()), WindowResolution.Window1900x1024);
            SelectOptionGuiElement<WindowResolution> opt3 = new SelectOptionGuiElement<WindowResolution>(optRect,
                new LabelGuiElement("1440 x 900", _font, new Vector2()), WindowResolution.Window1440x900);

            //////
            Select = new SelectGuiElement<WindowResolution>(game.Content.Load<Texture2D>("menu/TAK15")
                , rec, defW, defW, opt1, opt2,
                opt3);

            guiElement.AddChild(new CenterAligmentGuiElement(rec, new LabelGuiElement("MinMapLevelOfDetailsBias", _font, new Vector2())));
            guiElement.AddChild(MinMapLevelOfDetailsBiasPicker);

            element.AddChild(MultiSamplingChange);
            element.AddChild(LinearMipFilterCheckBox);
            element.AddChild(LinearMagFilterCheckBox);
            //LinearFilterCheckBox.IsVisible = false;
            guiElement.AddChild(element);
            element.AddChild(Select);
            _guiElement = new CenterAligmentGuiElement(new Rectangle(new Point(), game.Window.ClientBounds.Size), guiElement);

            game.GraphicsDevice.DeviceReset += (s, e) =>
            {
                _guiElement.Rectangle = game.GraphicsDevice.PresentationParameters.Bounds;
            };

            game.Window.ClientSizeChanged += (s, e) =>
            {
                _guiElement.Rectangle = new Rectangle(new Point(), game.Window.ClientBounds.Size);
            };
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (!Visible)
                return;

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, GraphicsDevice.SamplerStates[0], DepthStencilState.Default);
            _guiElement?.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mstate = Mouse.GetState();

            if (InputHandler.CheckKeyClick(Keys.Space))
                Visible = !Visible;


            UpdateAnimationsOfGuiElement(_guiElement, gameTime);
            if (!Visible) return;

            _guiElement.MouseMove(mstate.Position);
            if (InputHandler.CheckKeyClick(MouseButtons.Left))
                _guiElement.Click(mstate.Position);
            base.Update(gameTime);
        }

        void UpdateAnimationsOfGuiElement(IGuiElement guiElement, GameTime gameTime)
        {
            if (guiElement is IAnimatedGuiElement)
                ((IAnimatedGuiElement)guiElement).Update(gameTime);
            if (guiElement?.ChildElements != null)
                foreach (var el in guiElement.ChildElements)
                    UpdateAnimationsOfGuiElement(el, gameTime);
        }


        protected override void OnVisibleChanged(object sender, EventArgs args)
        {
            base.OnVisibleChanged(sender, args);
        }
    }
}
