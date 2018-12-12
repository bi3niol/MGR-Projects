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
        public List<IComponet> Components { get; set; } = new List<IComponet>();
        public IStateManager ParentManager { get; set; }
        public SimpleEffect Effect { get; set; }

        public void Draw(GameTime gameTime)
        {
            Draw(gameTime, Camera);
        }

        public void Draw(GameTime gameTime, Camera camera)
        {
            Effect.CameraPosition = camera.Position;
            Effect.CameraUp = camera.Up;
            foreach (var model in Components.OfType<IRenderableComponent>())
                model.Draw(camera.ViewMatrix, Projection);
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            foreach (var model in Components.OfType<IUpdateableComponent>())
                model.Update(gameTime);
        }
    }
}
