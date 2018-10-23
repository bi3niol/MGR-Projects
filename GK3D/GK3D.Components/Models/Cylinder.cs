using System;
using System.Collections.Generic;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class Cylinder : CustomModel<VertexPositionColorNormal>
    {
        public Cylinder(GraphicsDeviceManager graphics, SimpleEffect effect, Color c, float raius, float height, int n) : base(effect)
        {
            Vector3[] pos = new Vector3[2 * (n + 1)];
            Vector3[] normals = new Vector3[pos.Length];
            height /= 2;
            pos[0] = Vector3.Up * height;
            normals[0] = Vector3.Up;

            pos[pos.Length - 1] = Vector3.Down * height;
            normals[pos.Length - 1] = Vector3.Down;

            float _pi = (float)Math.PI;
            float _2pi = _pi * 2f;
            for (int i = 0; i < n; i++)
            {
                Vector3 norm = new Vector3();
                float a2 = _2pi * (float)(i + 1) / n;
                float z = (float)Math.Sin(a2);
                float x = (float)Math.Cos(a2);
                norm.X = x;
                norm.Z = z;
                norm.Normalize();
                pos[i + 1] = new Vector3(x * raius, height, z * raius);
                pos[i + 1 + n] = new Vector3(x * raius, -height, z * raius);
                normals[i + 1] = normals[i + 1 + n] = norm;
            }

            var vertexes = new List<VertexPositionColorNormal>();

            for (int i = 0; i < n; i++)
            {
                //top
                vertexes.Add(new VertexPositionColorNormal(pos[0], c, Vector3.Up));
                vertexes.Add(new VertexPositionColorNormal(pos[(i + 1)], c, Vector3.Up));
                vertexes.Add(new VertexPositionColorNormal(pos[(i + 1) % n + 1], c, Vector3.Up));
                //bottom
                vertexes.Add(new VertexPositionColorNormal(pos[pos.Length - 1], c, Vector3.Down));
                vertexes.Add(new VertexPositionColorNormal(pos[(i + 1) % n + 1 + n], c, Vector3.Down));
                vertexes.Add(new VertexPositionColorNormal(pos[i + 1 + n], c, Vector3.Down));

                //
                int i1 = i + 1,
                    n1 = n + 1,
                    i1n = i + 1 + n,
                    imn = (i + 1) % n;

                vertexes.Add(new VertexPositionColorNormal(pos[i1], c, normals[i1]));
                vertexes.Add(new VertexPositionColorNormal(pos[i1n], c, normals[i1n]));
                vertexes.Add(new VertexPositionColorNormal(pos[imn + n1], c, normals[imn + n1]));

                vertexes.Add(new VertexPositionColorNormal(pos[i1], c, normals[i1]));
                vertexes.Add(new VertexPositionColorNormal(pos[imn + n1], c, normals[imn + n1]));
                vertexes.Add(new VertexPositionColorNormal(pos[imn + 1], c, normals[imn + 1]));
            }
            Vertexes = vertexes.ToArray();
        }
    }
}
