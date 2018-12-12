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
    public class EkranModel : CustomModel<VertexPositionNormalTexture>
    {
        public Texture Texture { get; set; }


        private Vector2 _textureTranslation;
        public Vector2 TextureTranslation
        {
            get => _textureTranslation; set
            {
                if (_textureTranslation == value)
                    return;

                _textureTranslation = value;
                OnTexturePropertyChange();
            }
        }

        private Vector2 _textureScales = Vector2.One;
        public Vector2 TextureScales
        {
            get => _textureScales; set
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
            TextureMatrix = Matrix.CreateScale(new Vector3(TextureScales, 1)) * Matrix.CreateTranslation(new Vector3(TextureTranslation, 0));
        }

        public EkranModel(SimpleEffect effect) : base(effect)
        {
            Vertices = new VertexPositionNormalTexture[6];
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1);

            var color = Color.Black;
            Vector3 topLeftFront = new Vector3(0, 0, 0);
            Vector3 topRightFront = new Vector3(1.0f, 0, 0);

            Vector3 btmLeftFront = new Vector3(0, 1, 0);
            Vector3 btmRightFront = new Vector3(1, 1, 0);

            Vertices[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, Vector2.Zero);
            Vertices[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, Vector2.Zero);
            Vertices[2] = new VertexPositionNormalTexture(topRightFront, normalFront, Vector2.Zero);
            Vertices[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, Vector2.Zero);
            Vertices[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, Vector2.Zero);
            Vertices[5] = new VertexPositionNormalTexture(topRightFront, normalFront, Vector2.Zero);

            OnTexturePropertyChange();
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            int triangleCount = 2;

            Effect.View = view;
            Effect.World = World;
            Effect.Projection = projection;

            Effect.TextureMatrix = TextureMatrix;
            Effect.Texture = Texture;
            Effect.TextureLoaded = Texture == null ? 0 : 1;
            Effect.LightsEnabled = false;
            Effect.CurrentTechnique = Effect.Techniques["TechTextureMatrix"];

            var pass = Effect.CurrentTechnique.Passes["TextureMatrix"];
            pass.Apply();
            Effect.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vertices, 0, triangleCount);
            Effect.LightsEnabled = true;
        }

    }
}
