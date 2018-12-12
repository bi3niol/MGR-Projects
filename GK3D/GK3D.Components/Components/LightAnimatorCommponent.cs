using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.SceneObjects;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;

namespace GK3D.Components.Components
{
    public class LightAnimatorCommponent : IUpdateableComponent
    {
        private SimpleEffect effect;
        private Light light;
        private int dirR, dirG, dirB;
        public LightAnimatorCommponent(Light l, SimpleEffect e)
        {
            effect = e;
            light = l;
            dirR = dirG = dirB = 1;
        }

        private void CutAndSetDir(ref double val, ref int dir)
        {
            if (val > 255)
            {
                dir = -1;
                val = 255;
            }
            if (val < 0)
            {
                dir = 1;
                val = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            double r = light.Color.R, g = light.Color.G, b = light.Color.B;
            r = (r + dirR * Math.Max(r * gameTime.ElapsedGameTime.TotalSeconds, 1));
            g = (g + dirG * Math.Max(g * gameTime.ElapsedGameTime.TotalSeconds, 1));
            b = (b + dirB * Math.Max(b * gameTime.ElapsedGameTime.TotalSeconds, 1));

            CutAndSetDir(ref r, ref dirR);
            CutAndSetDir(ref g, ref dirG);
            CutAndSetDir(ref b, ref dirB);

            Color c = light.Color;
            c.R = (byte)r;
            c.G = (byte)g;
            c.B = (byte)b;
            light.Color = c;

            effect.UpdateLightColor(light);
        }
    }
}
