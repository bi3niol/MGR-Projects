using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class XnaModel : BaseModel, IXnaModel
    {
        public bool TextureEnabled { get; set; } = true;
        private Model model;
        public ModelBoneCollection Bones => model.Bones;

        public ModelMeshCollection Meshes => model.Meshes;

        public Texture2D Texture { get; set; }

        public ModelBone Root
        {
            get => model.Root;
            set => model.Root = value;
        }

        public SimpleEffect Effect { get; set; }

        public XnaModel(SimpleEffect effect, Model m) : base()
        {
            Effect = effect;
            model = m;
        }

        public void CopyAbsoluteBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            model.CopyAbsoluteBoneTransformsTo(destinationBoneTransforms);
        }

        public void CopyBoneTransformsFrom(Matrix[] sourceBoneTransforms)
        {
            model.CopyBoneTransformsFrom(sourceBoneTransforms);
        }

        public void CopyBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            model.CopyBoneTransformsTo(destinationBoneTransforms);
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            Effect.View = view;
            Effect.Projection = projection;
            Effect.World = World;

            EffectPass pass;
            if (TextureEnabled)
            {
                Effect.Texture = Texture;
                Effect.TextureLoaded = Texture == null ? 0 : 1;
                Effect.CurrentTechnique = Effect.Techniques["TechTexture"];
                pass = Effect.CurrentTechnique.Passes["Texture"];
            }
            else
            {
                Effect.CurrentTechnique = Effect.Techniques["TechColor"];
                pass = Effect.CurrentTechnique.Passes["Color"];
            }
            pass.Apply();
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
