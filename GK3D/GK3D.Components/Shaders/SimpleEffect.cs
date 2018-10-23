using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Components.Shaders
{
    public class SimpleEffect : Effect
    {
        public GraphicsDeviceManager Graphics { get; set; }
        public Matrix View
        {
            get
            {
                return Parameters[nameof(View)].GetValueMatrix();
            }
            set
            {
                Parameters[nameof(View)].SetValue(value);
            }
        }
        public Matrix World
        {
            get
            {
                return Parameters[nameof(World)].GetValueMatrix();
            }
            set
            {
                Parameters[nameof(World)].SetValue(value);
            }
        }
        public Matrix Projection
        {
            get
            {
                return Parameters[nameof(Projection)].GetValueMatrix();
            }
            set
            {
                Parameters[nameof(Projection)].SetValue(value);
            }
        }

        public Vector3 CameraPosition
        {
            get
            {
                return Parameters[nameof(CameraPosition)].GetValueVector3();
            }
            set
            {
                Parameters[nameof(CameraPosition)].SetValue(value);
            }
        }

        public SimpleEffect(GraphicsDeviceManager graphics, Effect effect) : this(effect)
        {
            Graphics = graphics;
        }
        public SimpleEffect(Effect cloneSource) : base(cloneSource)
        {
        }
    }
}
