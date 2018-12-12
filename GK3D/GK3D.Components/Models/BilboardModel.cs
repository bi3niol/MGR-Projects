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
    public class BilboardModel : CustomModel<BilboardVertex>
    {
        private bool wasLightEnabled;

        public Texture Texture { get; set; }

        protected override void OnPropertychanged(string propName)
        {
            //if (nameof(Position) == propName)
            //{
            //    Array.ForEach(Vertices, v => v.Position = Position);
            //}
            base.OnPropertychanged(propName);
        }
        public BilboardModel(SimpleEffect effect, Texture texture) : base(effect)
        {
            Vertices = new BilboardVertex[6];

            Vector3 position = new Vector3(0.5f,0,0);

            Vertices[0] = new BilboardVertex(position, new Vector2(0));
            Vertices[1] = new BilboardVertex(position, new Vector2(1, 0));
            Vertices[2] = new BilboardVertex(position, new Vector2(1));

            Vertices[3] = new BilboardVertex(position, new Vector2(0));
            Vertices[4] = new BilboardVertex(position, new Vector2(1));
            Vertices[5] = new BilboardVertex(position, new Vector2(0, 1));
            Texture = texture;
        }

        protected override EffectPass SetEffectVariableGetPass(SimpleEffect effect)
        {
            effect.Texture = Texture;
            effect.TextureLoaded = Texture == null ? 0 : 1;
            effect.CurrentTechnique = effect.Techniques["Bilboard"];
            //wasLightEnabled = effect.LightsEnabled;
            //effect.LightsEnabled = false;
            return effect.CurrentTechnique.Passes["Pass1"];
        }
        protected override void SetEffectPrevValues(SimpleEffect effect)
        {
            base.SetEffectPrevValues(effect);
            //effect.LightsEnabled = wasLightEnabled;
        }
    }
}
