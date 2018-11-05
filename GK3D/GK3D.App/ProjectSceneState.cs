using GK3D.Components;
using GK3D.Components.Game;
using GK3D.Components.SceneObjects;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GK3D.App
{
    class ProjectSceneState : IGameState
    {
        public Camera Camera { get; set; }
        public Matrix Projection { get; set; }
        public List<IGameObject> GameObjects { get; set; } = new List<IGameObject>();
        public IStateManager ParentManager { get; set; }
        public SimpleEffect Effect { get; set; }

        public void Draw(GameTime gameTime)
        {
            Effect.CameraPosition = Camera.Position;
            foreach (var model in GameObjects.OfType<IModel>())
                model.Draw(Camera.ViewMatrix, Projection);
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            foreach (var model in GameObjects)
                model.Update(gameTime);
        }
    }
}
