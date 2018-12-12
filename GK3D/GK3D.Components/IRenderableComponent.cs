using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Components
{
    public interface IRenderableComponent: IComponet
    {
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
