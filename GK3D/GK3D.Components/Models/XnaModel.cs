using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GK3D.Components.Models
{
    public class XnaModel : BaseModel, IXnaModel
    {
        public bool TextureEnabled { get; set; } = true;
        private Model model;
        public ModelBoneCollection Bones => model.Bones;

        public ModelMeshCollection Meshes => model.Meshes;

        public List<Texture> Textures { get; set; } = new List<Texture>();

        public Texture Texture
        {
            get
            {
                if (Textures.Count > 0)
                    return Textures[0];
                return null;
            }
            set
            {
                if (Textures.Count == 0)
                    Textures.Add(value);

                if (Textures.Contains(value))
                    Textures.Remove(value);

                Textures.Insert(0, value);
            }
        }
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
            Effect.CurrentTechnique = Effect.Techniques["TechTexture"];
            foreach (var texture in Textures)
            {
                EffectPass pass;
                Effect.Texture = texture;
                Effect.TextureLoaded = texture == null ? 0 : 1;
                pass = Effect.CurrentTechnique.Passes["Texture"];

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
}
