using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class XnaModel : IXnaModel
    {
        private Model model;
        public ModelBoneCollection Bones => model.Bones;

        public ModelMeshCollection Meshes => model.Meshes;

        public ModelBone Root
        {
            get => model.Root;
            set => model.Root = value;
        }
        public object Tag
        {
            get => model.Tag;
            set => model.Tag = value;
        }

        public XnaModel(Model m)
        {
            model = m;
        }

        public XnaModel(GraphicsDevice graphicsDevice, List<ModelBone> bones, List<ModelMesh> meshes)
        {
            model = new Model(graphicsDevice, bones, meshes);
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

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            model.Draw(world, view, projection);
        }
    }
}
