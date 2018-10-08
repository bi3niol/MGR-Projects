using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class CustomModel<TVertex> :BaseModel where TVertex : struct, IVertexType
    {
        protected TVertex[] Vertexes;
        protected BasicEffect Effect;
        protected GraphicsDeviceManager Graphics;

        public CustomModel(GraphicsDeviceManager graphics, BasicEffect effect)
        {
            Graphics = graphics;
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
            Effect.VertexColorEnabled = true;

            Effect.EnableDefaultLighting();

            foreach (var pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vertexes, 0, triangleCount);
            }
        }
    }
}
