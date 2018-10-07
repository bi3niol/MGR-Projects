using Microsoft.Xna.Framework;


namespace GK3D.Components.Game
{
    public class GameManager
    {
        public Microsoft.Xna.Framework.Game Game { get; private set; }
        public IStateManager StateManager { get; private set; }

        public GameManager(Microsoft.Xna.Framework.Game game, IStateManager manager)
        {
            Game = game;
            StateManager = manager;
        }

        public void Draw(GameTime gameTime)
        {
            StateManager?.Draw(gameTime);
        }
        public void Update(GameTime gameTime)
        {
            StateManager?.Update(gameTime);
        }
    }
}
