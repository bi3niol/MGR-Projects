using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.SceneObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GK3D.Components.Components
{
    public class CameraControllerComponent : IComponet
    {
        public CameraControllerComponent(Camera camera)
        {
            Camera = camera;
        }

        public float MovementSpeed { get; set; } = 8.0f;

        public Camera Camera { get; }

        public void Update(GameTime gameTime)
        {
            float xS, yS, zS;
            xS = yS = zS = 0f;

            var kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.W))
                zS = MovementSpeed;
            if (kState.IsKeyDown(Keys.S))
                zS = -MovementSpeed;

            if (kState.IsKeyDown(Keys.A))
                xS = MovementSpeed;
            if (kState.IsKeyDown(Keys.D))
                xS = -MovementSpeed;

            if (kState.IsKeyDown(Keys.E))
                yS = MovementSpeed;
            if (kState.IsKeyDown(Keys.Q))
                yS = -MovementSpeed;

            Camera.Position+=(new Vector3(xS,yS,zS) * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
