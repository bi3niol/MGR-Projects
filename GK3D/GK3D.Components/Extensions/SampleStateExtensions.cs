using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Components.Extensions
{
    public static class SampleStateExtensions
    {
        public static SamplerState CopySampler(this SamplerState sampler)
        {
            return new SamplerState()
            {
                AddressU = sampler.AddressU,
                AddressV = sampler.AddressV,
                AddressW = sampler.AddressW,
                BorderColor = sampler.BorderColor,
                ComparisonFunction = sampler.ComparisonFunction,
                Filter = sampler.Filter,
                FilterMode = sampler.FilterMode,
                MaxAnisotropy = sampler.MaxAnisotropy,
                MaxMipLevel = sampler.MaxMipLevel,
                MipMapLevelOfDetailBias = sampler.MipMapLevelOfDetailBias,
                Name = sampler.Name
            };
        }
    }
}
