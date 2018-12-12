using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class CustomModel<TVertex> : BaseModel where TVertex : struct, IVertexType
    {
        protected TVertex[] Vertices;
        protected SimpleEffect Effect;

        public CustomModel(SimpleEffect effect)
        {
            Effect = effect;
            Initialize();
        }

        protected virtual EffectPass SetEffectVariableGetPass(SimpleEffect effect)
        {
            effect.CurrentTechnique = Effect.Techniques["TechColor"];
            return effect.CurrentTechnique.Passes["Color"];
        }

        protected virtual void SetEffectPrevValues(SimpleEffect effect) { }
        public override void Draw(Matrix view, Matrix projection)
        {
            if (Effect == null) return;

            int triangleCount = Vertices.Length / 3;

            Effect.View = view;
            Effect.World = World;
            Effect.Projection = projection;

            var pass = SetEffectVariableGetPass(Effect);
            pass.Apply();
            Effect.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vertices, 0, triangleCount);
            SetEffectPrevValues(Effect);
        }
    }
}
