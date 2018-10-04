using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class BaseModel : IModel
    {
        protected VertexPositionTexture[] Vertexes;
        protected BasicEffect Efect;
        protected GraphicsDeviceManager Graphics;

        public object Tag { get; set; }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            if (Efect == null) return;

            int triangleCount = Vertexes.Length / 3;

            Efect.View = view;
            Efect.World = world;
            Efect.Projection = projection;

            foreach (var pass in Efect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vertexes, 0, triangleCount);
            }
        }
    }
}
