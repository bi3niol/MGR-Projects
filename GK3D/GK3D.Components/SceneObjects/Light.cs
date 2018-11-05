using GK3D.Components.Game;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Components.SceneObjects
{
    public enum LightType
    {
        Directional = 0,
        Point = 1,
        Spot = 2
    }
    public class Light : GameObject
    {
        private static int _id = 0;

        public readonly int ID = _id++;
        public LightType Type { get; set; }

        public Vector3? Position { get; set; }
        public Vector3? Direction { get; set; }

        public Color Color { get; set; }
        public float KDiffuse { get; set; }

        public float KSpecular { get; set; }

        public float Power { get; set; }
    }
}
