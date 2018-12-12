using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class EnvMappingModel : BaseModel
    {
        private Model model;
        public TextureCube Texture { get; set; }
        public SimpleEffect Effect { get; set; }

        public EnvMappingModel(Model model, SimpleEffect effect)
        {
            Effect = effect;
            this.model = model;
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            Effect.View = view;
            Effect.Projection = projection;
            Effect.World = World;
            Effect.CurrentTechnique = Effect.Techniques["EnvMapping"];
            EffectPass pass;
            pass = Effect.CurrentTechnique.Passes["Pass1"];
            pass.Apply();

            Effect.SkyboxTexture = Texture;

            foreach (var mesh in model.Meshes)
            {
                foreach (var part in mesh.MeshParts)
                {
                    part.Effect = Effect;
                }
                mesh.Draw();
            }
        }
    }
}
