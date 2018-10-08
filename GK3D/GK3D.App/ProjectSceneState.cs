using GK3D.Components;
using GK3D.Components.Game;
using GK3D.Components.SceneObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GK3D.App
{
    class ProjectSceneState : IGameState
    {
        public Camera Camera { get; set; }
        public Matrix Projection { get; set; }
        public List<IModel> Models { get; set; } = new List<IModel>();
        public IStateManager ParentManager { get; set; }

        public void Draw(GameTime gameTime)
        {
            foreach (var model in Models)
                model.Draw(Camera.ViewMatrix, Projection);
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            foreach (var model in Models)
                model.Update(gameTime);
        }
    }
}
