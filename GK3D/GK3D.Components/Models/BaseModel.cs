using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public abstract class BaseModel<TVertex> : IModel where TVertex : struct, IVertexType
    {
        protected TVertex[] Vertexes;
        protected BasicEffect Effect;
        protected GraphicsDeviceManager Graphics;

        public object Tag { get; set; }

        public BaseModel(GraphicsDeviceManager graphics, BasicEffect effect)
        {
            Graphics = graphics;
            Effect = effect;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            if (Effect == null) return;

            int triangleCount = Vertexes.Length / 3;

            Effect.View = view;
            Effect.World = world;
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
