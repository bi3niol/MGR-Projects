using System.Collections.Generic;
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


        public XnaModel(Model m) : base()
        {
            model = m;
        }

        public XnaModel(GraphicsDevice graphicsDevice, List<ModelBone> bones, List<ModelMesh> meshes):this(new Model(graphicsDevice, bones, meshes))
        {}

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
            model.Draw(World, view, projection);
        }
    }
}
