
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components
{
    public interface IXnaModel : IModel
    {
        //
        // Summary:
        //     A collection of Microsoft.Xna.Framework.Graphics.ModelBone objects which describe
        //     how each mesh in the mesh collection for this model relates to its parent mesh.
        ModelBoneCollection Bones { get; }
        //
        // Summary:
        //     A collection of Microsoft.Xna.Framework.Graphics.ModelMesh objects which compose
        //     the model. Each Microsoft.Xna.Framework.Graphics.ModelMesh in a model may be
        //     moved independently and may be composed of multiple materials identified as Microsoft.Xna.Framework.Graphics.ModelMeshPart
        //     objects.
        ModelMeshCollection Meshes { get; }
        //
        // Summary:
        //     Root bone for this model.
        ModelBone Root { get; set; }
        //
        // Summary:
        //     Copies bone transforms relative to all parent bones of the each bone from this
        //     model to a given array.
        //
        // Parameters:
        //   destinationBoneTransforms:
        //     The array receiving the transformed bones.
        void CopyAbsoluteBoneTransformsTo(Matrix[] destinationBoneTransforms);
        //
        // Summary:
        //     Copies bone transforms relative to Microsoft.Xna.Framework.Graphics.Model.Root
        //     bone from a given array to this model.
        //
        // Parameters:
        //   sourceBoneTransforms:
        //     The array of prepared bone transform data.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     sourceBoneTransforms is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     sourceBoneTransforms is invalid.
        void CopyBoneTransformsFrom(Matrix[] sourceBoneTransforms);
        //
        // Summary:
        //     Copies bone transforms relative to Microsoft.Xna.Framework.Graphics.Model.Root
        //     bone from this model to a given array.
        //
        // Parameters:
        //   destinationBoneTransforms:
        //     The array receiving the transformed bones.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     destinationBoneTransforms is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     destinationBoneTransforms is invalid.
        void CopyBoneTransformsTo(Matrix[] destinationBoneTransforms);
    }
}
