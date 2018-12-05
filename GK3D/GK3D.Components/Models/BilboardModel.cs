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
    public class BilboardModel : CustomModel<VertexPositionColorNormal>
    {
        public Texture Texture { get; set; }


        private Vector2 _textureTranslation;
        public Vector2 TextureTranslation { get=>_textureTranslation; set
            {
                if (_textureTranslation == value)
                    return;

                _textureTranslation = value;
                OnTexturePropertyChange();
            }
        }

        private Vector2 _textureScales;
        public Vector2 TextureScales { get=>_textureScales; set
            {
                if (_textureScales == value)
                    return;

                _textureScales = value;
                OnTexturePropertyChange();
            }
        }

        protected Matrix TextureMatrix = Matrix.Identity;

        protected virtual void OnTexturePropertyChange()
        {
            TextureMatrix = Matrix.CreateScale(new Vector3(TextureScales, 1))*Matrix.CreateTranslation(new Vector3(TextureTranslation,0));
        }

        public BilboardModel(SimpleEffect effect) : base(effect)
        {
            Vertexes = new VertexPositionColorNormal[6];
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f);

            var color = Color.Black;
            Vector3 topLeftFront = new Vector3(0, 1.0f, 0);
            Vector3 topRightFront = new Vector3(1.0f, 1.0f, 0);

            Vector3 btmLeftFront = new Vector3(0, 0, 0);
            Vector3 btmRightFront = new Vector3(1.0f, 0, 0);

            Vertexes[2] = new VertexPositionColorNormal(topLeftFront, color, normalFront);
            Vertexes[1] = new VertexPositionColorNormal(btmLeftFront, color, normalFront);
            Vertexes[0] = new VertexPositionColorNormal(topRightFront, color, normalFront);
            Vertexes[5] = new VertexPositionColorNormal(btmLeftFront, color, normalFront);
            Vertexes[4] = new VertexPositionColorNormal(btmRightFront, color, normalFront);
            Vertexes[3] = new VertexPositionColorNormal(topRightFront, color, normalFront);
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            int triangleCount = Vertexes.Length / 3;

            Effect.View = view;
            Effect.World = World;
            Effect.Projection = projection;

            Effect.TextureMatrix = TextureMatrix;
            Effect.Texture = Texture;
            Effect.TextureLoaded = Texture == null ? 0 : 1;

            Effect.CurrentTechnique = Effect.Techniques["TechTextureMatrix"];
            var pass = Effect.CurrentTechnique.Passes["TextureMatrix"];
            pass.Apply();
            Effect.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vertexes, 0, triangleCount);
        }
    }
}
