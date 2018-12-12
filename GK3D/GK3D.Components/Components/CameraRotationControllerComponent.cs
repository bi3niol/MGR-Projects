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
    public class CameraRotationControllerComponent : IUpdateableComponent
    {
        public Camera Camera { get; }
        public float RotationtSpeed { get; set; } = 0.5f*2;

        public CameraRotationControllerComponent(Camera camera)
        {
            Camera = camera;
        }
        public void Update(GameTime gameTime)
        {
            float xS, yS, zS;
            xS = yS = zS = 0f;

            var kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.U))
                xS = RotationtSpeed;
            if (kState.IsKeyDown(Keys.J))
                xS = -RotationtSpeed;

            if (kState.IsKeyDown(Keys.H))
                zS = RotationtSpeed;
            if (kState.IsKeyDown(Keys.K))
                zS = -RotationtSpeed;

            if (kState.IsKeyDown(Keys.I))
                yS = -RotationtSpeed;
            if (kState.IsKeyDown(Keys.Y))
                yS = RotationtSpeed;

            Camera.Rotate(new Vector3(xS, yS, zS) * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
