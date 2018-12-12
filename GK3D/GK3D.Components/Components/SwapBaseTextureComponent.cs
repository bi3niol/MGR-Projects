using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.Models;
using GK3D.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GK3D.Components.Components
{
    public class SwapBaseTextureComponent : IUpdateableComponent
    {
        private List<Texture> _textures = new List<Texture>();
        private int textureId = 0;
        private XnaModel xnaModel;

        public SwapBaseTextureComponent(XnaModel model, params Texture[] textures)
        {
            xnaModel = model;
            if (textures == null)
                throw new ArgumentNullException("you must pass at least 2 textures");

            _textures = textures.ToList();
        }

        public void Update(GameTime gameTime)
        {
            if (_textures.Count > 0)
                if (InputHandler.CheckKeyClick(Keys.N))
                {
                    xnaModel.Texture = _textures[textureId];
                    textureId = (textureId + 1) % _textures.Count;
                }
        }
    }
}
