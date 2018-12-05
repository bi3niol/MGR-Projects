using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.SceneObjects;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class SkyBox : BaseModel
    {
        private Model _skyBox;
        private Texture _texture;
        private SimpleEffect _effect;
        public Camera Camera { get; set; }
        public SkyBox(Model skybox, Texture texture, SimpleEffect effect)
        {
            _skyBox = skybox;
            _texture = texture;
            _effect = effect;
            //Scale = new Vector3(30);
        }
        public override void Draw(Matrix view, Matrix projection)
        {
            Position = Camera.Position;
            var orginalState = _effect.GraphicsDevice.RasterizerState;

            RasterizerState skybox = new RasterizerState();
            skybox.CullMode = CullMode.None;
            _effect.GraphicsDevice.RasterizerState = skybox;

            _effect.View = view;
            _effect.Projection = projection;
            _effect.World = World;
            var lightEnabled = _effect.LightsEnabled;
            _effect.LightsEnabled = false;
            EffectPass pass;
            _effect.Texture = _texture;
            _effect.TextureLoaded = 1;
            _effect.CurrentTechnique = _effect.Techniques["Skybox"];
            pass = _effect.CurrentTechnique.Passes["Pass1"];

            pass.Apply();
            foreach (var mesh in _skyBox.Meshes)
            {
                foreach (var part in mesh.MeshParts)
                {
                    part.Effect = _effect;
                }
                mesh.Draw();
            }
            _effect.LightsEnabled = lightEnabled;
            _effect.GraphicsDevice.RasterizerState = orginalState;
        }
    }
}
