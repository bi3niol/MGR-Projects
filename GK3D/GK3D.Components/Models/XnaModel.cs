using System.Collections.Generic;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class XnaModel : BaseModel, IXnaModel
    {
        private Model model;
        public ModelBoneCollection Bones => model.Bones;

        public ModelMeshCollection Meshes => model.Meshes;

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

            //foreach (var mesh in model.Meshes)
            //{
            //    foreach (var part in mesh.MeshParts)
            //    {
            //        part.Effect = Effect;
            //    }
            //}
            model.Draw(World, view, projection);
        }
    }
}
