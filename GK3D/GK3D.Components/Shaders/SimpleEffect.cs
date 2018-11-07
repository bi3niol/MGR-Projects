using GK3D.Components.SceneObjects;
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
        private static class ParamNames
        {
            public const string LightPower = "LightPower";
            public const string LightPosition = "LightPosition";
            public const string LightDirection = "LightDirection";
            public const string LightColor = "LightColor";
            public const string LightKDiffuse = "LightKDiffuse";
            public const string LightKSpecular = "LightKSpecular";
            public const string LightType = "LightType";
            public const string LightEnabled = "LightEnabled";
            public const string LightCount = "LightCount";
            public const string SpecularM = "SpecularM";
            public const string Texture = "Texture";
            public const string TextureLoaded = "TextureLoaded";
        }

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

        public int TextureLoaded
        {
            get
            {
                return Parameters[ParamNames.TextureLoaded].GetValueInt32();
            }
            set
            {
                Parameters[ParamNames.TextureLoaded].SetValue(value);
            }
        }

        public Texture2D Texture
        {
            get
            {
                return Parameters[ParamNames.Texture].GetValueTexture2D();
            }
            set
            {
                Parameters[ParamNames.Texture].SetValue(value);
            }
        }

        private List<Light> lights = new List<Light>();
        private float[] LightPowers
        {
            get
            {
                return Parameters[ParamNames.LightPower].GetValueSingleArray();
            }
            set
            {
                Parameters[ParamNames.LightPower].SetValue(value);
            }
        }
        private float[] LightKDiffuses
        {
            get
            {
                return Parameters[ParamNames.LightKDiffuse].GetValueSingleArray();
            }
            set
            {
                Parameters[ParamNames.LightKDiffuse].SetValue(value);
            }
        }
        private float[] LightKSpeculars
        {
            get
            {
                return Parameters[ParamNames.LightKSpecular].GetValueSingleArray();
            }
            set
            {
                Parameters[ParamNames.LightKSpecular].SetValue(value);
            }
        }
        private Vector3[] LightColors
        {
            get
            {
                return Parameters[ParamNames.LightColor].GetValueVector3Array();
            }
            set
            {
                Parameters[ParamNames.LightColor].SetValue(value);
            }
        }

        private Vector3[] LightPositions
        {
            get
            {
                return Parameters[ParamNames.LightPosition].GetValueVector3Array();
            }
            set
            {
                Parameters[ParamNames.LightPosition].SetValue(value);
            }
        }
        private Vector3[] LightDirections
        {
            get
            {
                return Parameters[ParamNames.LightDirection].GetValueVector3Array();
            }
            set
            {
                Parameters[ParamNames.LightDirection].SetValue(value);
            }
        }
        private float[] LightTypes
        {
            get
            {
                return Parameters[ParamNames.LightType].GetValueSingleArray();
            }
            set
            {
                Parameters[ParamNames.LightType].SetValue(value);
            }
        }
        private int LightCount
        {
            get
            {
                return Parameters[ParamNames.LightCount].GetValueInt32();
            }
            set
            {
                Parameters[ParamNames.LightCount].SetValue(value);
            }
        }

        public SimpleEffect(GraphicsDeviceManager graphics, Effect effect) : this(effect)
        {
            Graphics = graphics;
        }
        public SimpleEffect(Effect cloneSource) : base(cloneSource)
        {
        }

        public bool UpdateLight(Light light)
        {
            return true;
        }

        public bool UpdateLightColor(Light light)
        {
            int id = IndexOfLight(light.ID);

            if (id < 0) return false;

            var colors = LightColors;
            colors[id] = light.Color.ToVector3();
            LightColors = colors;

            return true;
        }

        public bool AddLight(Light light)
        {
            if (lights.Any(l => l.ID == light.ID))
                return false;
            lights.Add(light);
            switch (light.Type)
            {
                case LightType.Directional:
                    return AddDirectional(light);
                case LightType.Point:
                    break;
                case LightType.Spot:
                    break;
                default:
                    break;
            }
            return AddDirectional(light); ;
        }

        public bool RemoveLight(int lightId)
        {
            int id = IndexOfLight(lightId);
            if (id < 0)
                return false;
            lights.RemoveAt(id);
            return true;
        }

        private int IndexOfLight(int lightId)
        {
            return lights.FindIndex(l => l.ID == lightId);
        }

        private bool AddDirectional(Light light)
        {
            //if (light.Type != LightType.Directional)
            //    throw new ArgumentException("Light type must by a Directional");
            int lightCount = LightCount;

            AddBaseLightProperties(light, lightCount);

            LightCount = lightCount + 1;

            return true;
        }

        private void AddBaseLightProperties(Light light, int lightindex)
        {
            var directions = LightDirections;
            directions[lightindex] = light.Direction;
            LightDirections = directions;

            var positions = LightPositions;
            positions[lightindex] = light.Position;
            LightPositions = positions;

            var powers = LightPowers;
            powers[lightindex] = light.Power;
            LightPowers = powers;

            var types = LightTypes;
            types[lightindex] = (int)light.Type;
            LightTypes = types;

            var kd = LightKDiffuses;
            kd[lightindex] = light.KDiffuse;
            LightKDiffuses = kd;

            var ks = LightKSpeculars;
            ks[lightindex] = light.KSpecular;
            LightKSpeculars = ks;

            var dcolors = LightColors;
            dcolors[lightindex] = light.Color.ToVector3();
            LightColors = dcolors;

        }
    }
}
