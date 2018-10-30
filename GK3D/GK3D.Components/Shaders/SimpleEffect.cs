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
            public const string LightPosition_Direction = "LightPosition_Direction";
            public const string LightDiffuseColor = "LightDiffuseColor";
            public const string LightKDiffuse = "LightKDiffuse";
            public const string LightSpecularColor = "LightSpecularColor";
            public const string LightKSpecular = "LightKSpecular";
            public const string LightType = "LightType";
            public const string LightEnabled = "LightEnabled";
            public const string LightCount = "LightCount";
            public const string SpecularM = "SpecularM";
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
        private Vector4[] LightDiffuseColors
        {
            get
            {
                return Parameters[ParamNames.LightDiffuseColor].GetValueVector4Array();
            }
            set
            {
                Parameters[ParamNames.LightDiffuseColor].SetValue(value);
            }
        }
        private Vector4[] LightSpecularColors
        {
            get
            {
                return Parameters[ParamNames.LightSpecularColor].GetValueVector4Array();
            }
            set
            {
                Parameters[ParamNames.LightSpecularColor].SetValue(value);
            }
        }
        private Vector4[] LightPosition_Directions
        {
            get
            {
                return Parameters[ParamNames.LightPosition_Direction].GetValueVector4Array();
            }
            set
            {
                Parameters[ParamNames.LightPosition_Direction].SetValue(value);
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

        public bool AddLight(Light light)
        {
            if (lights.Any(l => l.ID == light.ID))
                return false;

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
            return true;
        }

        public bool RemoveLight(int lightId)
        {
            int id = lights.FindIndex(l => l.ID == lightId);
            if (id < 0)
                return false;
            lights.RemoveAt(id);
            return true;
        }

        private bool AddDirectional(Light light)
        {
            if (light.Type != LightType.Directional)
                throw new ArgumentException("Light type must by a Directional");
            int lightCount = LightCount;

            var directions = LightPosition_Directions;
            directions[lightCount]=light.Direction.Value;
            LightPosition_Directions = directions;

            AddBaseLightProperties(light);

            LightCount = lightCount + 1;

            return true;
        }

        private void AddBaseLightProperties(Light light)
        {
            int lightCount = LightCount;

            var powers = LightPowers;
            powers[lightCount]=light.Power;
            LightPowers = powers;

            var types =LightTypes;
            types[lightCount]=(int)light.Type;
            LightTypes = types;

            var kd = LightKDiffuses;
            kd[lightCount]=light.KDiffuse;
            LightKDiffuses = kd;

            var ks = LightKSpeculars;
            ks[lightCount]=light.KSpecular;
            LightKSpeculars = ks;

            var dcolors = LightDiffuseColors;
            dcolors[lightCount]=light.DiffuseColor.ToVector4();
            LightDiffuseColors = dcolors;

            var scolors =LightSpecularColors;
            scolors[lightCount]=light.SpecularColor.ToVector4();
            LightSpecularColors = scolors;
        }
    }
}
