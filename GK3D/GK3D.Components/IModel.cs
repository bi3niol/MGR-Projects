using Microsoft.Xna.Framework;

namespace GK3D.Components
{
    public interface IModel : IGameObject
    {

        //
        // Summary:
        //     Custom attached object. Skinning data is example of attached object for model.
        object Tag { get; set; }

        Vector3 Rotation {get;set; }
        Vector3 Position { get; set; }
        Vector3 Scale { get; set; }

        Matrix World { get; }
        //
        // Summary:
        //     Draws the model meshes.
        //
        // Parameters:
        //
        //   view:
        //     The view transform.
        //
        //   projection:
        //     The projection transform.
        void Draw(Matrix view, Matrix projection);
    }
}
