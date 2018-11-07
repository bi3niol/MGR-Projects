using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class CustomModel<TVertex> : BaseModel where TVertex : struct, IVertexType
    {
        protected TVertex[] Vertexes;
        protected SimpleEffect Effect;

        public CustomModel(SimpleEffect effect)
        {
            Effect = effect;
            Initialize();
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            if (Effect == null) return;

            int triangleCount = Vertexes.Length / 3;

            Effect.View = view;
            Effect.World = World;
            Effect.Projection = projection;
            //Effect.VertexColorEnabled = true;

            //Effect.EnableDefaultLighting();
            Effect.CurrentTechnique = Effect.Techniques["TechColor"];
            var pass = Effect.CurrentTechnique.Passes["Color"];
            pass.Apply();
            Effect.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vertexes, 0, triangleCount);
        }
    }
}
