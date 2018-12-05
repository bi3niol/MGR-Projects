using GK3D.GUI;
using Microsoft.Xna.Framework;


namespace GK3D.Components.Game
{
    public class GameManager : DrawableGameComponent
    {
        public IStateManager StateManager { get; private set; }

        public GameManager(Microsoft.Xna.Framework.Game game, IStateManager manager) : base(game)
        {
            StateManager = manager;
        }

        public override void Draw(GameTime gameTime)
        {
            StateManager?.Draw(gameTime);
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            using (InputHandler.HandlerUpdate)
            {
                StateManager?.Update(gameTime);
                base.Update(gameTime);
            }
        }
    }
}
