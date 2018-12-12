using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GK3D.Components.Components
{
    public class TelebimTextureControllerComponent : IUpdateableComponent
    {
        private EkranModel bilboard;
        public TelebimTextureControllerComponent(EkranModel model)
        {
            bilboard = model;
        }
        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.OemPlus))
                bilboard.TextureScales += new Vector2((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (state.IsKeyDown(Keys.OemMinus))
                bilboard.TextureScales -= new Vector2((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (state.IsKeyDown(Keys.Up))
                bilboard.TextureTranslation += new Vector2(0,(float)gameTime.ElapsedGameTime.TotalSeconds);
            if (state.IsKeyDown(Keys.Down))
                bilboard.TextureTranslation -= new Vector2(0, (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (state.IsKeyDown(Keys.Left))
                bilboard.TextureTranslation -= new Vector2((float)gameTime.ElapsedGameTime.TotalSeconds,0);
            if (state.IsKeyDown(Keys.Right))
                bilboard.TextureTranslation += new Vector2((float)gameTime.ElapsedGameTime.TotalSeconds, 0);
        }
    }
}
