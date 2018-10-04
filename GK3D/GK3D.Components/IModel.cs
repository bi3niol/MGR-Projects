using Microsoft.Xna.Framework;

namespace GK3D.Components
{
    public interface IModel
    {
       
        //
        // Summary:
        //     Custom attached object. Skinning data is example of attached object for model.
        object Tag { get; set; }

        //
        // Summary:
        //     Draws the model meshes.
        //
        // Parameters:
        //   world:
        //     The world transform.
        //
        //   view:
        //     The view transform.
        //
        //   projection:
        //     The projection transform.
        void Draw(Matrix world, Matrix view, Matrix projection);
    }
}
