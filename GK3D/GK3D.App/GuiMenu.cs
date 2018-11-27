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
    internal class GuiMenu : DrawableGameComponent
    {
        private CenterAligmentGuiElement _guiElement;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        public GuiMenu(SpriteBatch spriteBatch, SpriteFont font, Game game) : base(game)
        {
            InputHandler.IsPointInGameWindow = game.Window.ClientBounds.Contains;
            _spriteBatch = spriteBatch;
            _font = font;
            Rectangle rec = new Rectangle(100, 100, 200, 40);
            IGuiElement guiElement = new VerticalLayout(game.Content.Load<Texture2D>("menu/TAK11"), new Rectangle(100, 100, 200, 200), false);
            guiElement.AddChild(new GuiElement(game.Content.Load<Texture2D>("menu/TAK13"), rec));
            guiElement.AddChild(new GuiElement(game.Content.Load<Texture2D>("menu/TAK15"), rec));
            guiElement.AddChild(new ButtonGuiElement(game.Content.Load<Texture2D>("menu/TAK15"), rec, "test", _font, game.Content.Load<Texture2D>("menu/TAK17")));
            _guiElement = new CenterAligmentGuiElement(game.Window.ClientBounds, guiElement);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (!Visible)
                return;

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, DepthStencilState.Default);
            _guiElement?.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mstate = Mouse.GetState();
            using (InputHandler.HandlerUpdate)
            {
                if (InputHandler.CheckKeyClick(Keys.Space))
                    Visible = !Visible;

                UpdateAnimationsOfGuiElement(_guiElement, gameTime);
                _guiElement.MouseMove(mstate.Position);
                if (InputHandler.CheckKeyClick(MouseButtons.Left))
                    _guiElement.Click(mstate.Position);
                base.Update(gameTime);
            }
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
