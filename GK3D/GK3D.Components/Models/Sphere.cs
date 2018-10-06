using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class Sphere : BaseModel<VertexPositionColorNormal>
    {
        public Sphere(GraphicsDeviceManager graphics, BasicEffect effect, Color c, float radius, int nbLat, int nbLong) : base(graphics, effect)
        {
            Vertexes = new VertexPositionColorNormal[(nbLong + 1) * nbLat + 2];
            Vector3[] vertices = new Vector3[(nbLong + 1) * nbLat + 2];
            float _pi = (float)Math.PI;
            float _2pi = _pi * 2f;

            vertices[0] = Vector3.Up * radius;
            for (int lat = 0; lat < nbLat; lat++)
            {
                float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
                float sin1 = (float)Math.Sin(a1);
                float cos1 = (float)Math.Cos(a1);

                for (int lon = 0; lon <= nbLong; lon++)
                {
                    float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                    float sin2 = (float)Math.Sin(a2);
                    float cos2 = (float)Math.Cos(a2);

                    vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
                }
            }
            vertices[vertices.Length - 1] = Vector3.Up * -radius;

            Vector3[] normales = new Vector3[vertices.Length];
            for (int n = 0; n < vertices.Length; n++)
            {
                normales[n] = vertices[n];
                normales[n].Normalize();
            }



            //#region UVs
            //Vector2[] uvs = new Vector2[vertices.Length];
            //uvs[0] = Vector2.up;
            //uvs[uvs.Length - 1] = Vector2.zero;
            //for (int lat = 0; lat < nbLat; lat++)
            //    for (int lon = 0; lon <= nbLong; lon++)
            //        uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
            //#endregion


            #region Triangles
            var vertexes = new List<VertexPositionColorNormal>();

            //Top Cap
            int i = 0;
            for (int lon = 0; lon < nbLong; lon++)
            {
                vertexes.Add(new VertexPositionColorNormal(vertices[lon + 2], c, normales[lon + 2]));
                vertexes.Add(new VertexPositionColorNormal(vertices[lon + 1], c, normales[lon + 1]));
                vertexes.Add(new VertexPositionColorNormal(vertices[0], c, normales[0]));
            }

            //Middle
            for (int lat = 0; lat < nbLat - 1; lat++)
            {
                for (int lon = 0; lon < nbLong; lon++)
                {
                    int current = lon + lat * (nbLong + 1) + 1;
                    int next = current + nbLong + 1;

                    vertexes.Add(new VertexPositionColorNormal(vertices[current], c, normales[current]));
                    vertexes.Add(new VertexPositionColorNormal(vertices[current + 1], c, normales[current + 1]));
                    vertexes.Add(new VertexPositionColorNormal(vertices[next + 1], c, normales[next + 1]));

                    vertexes.Add(new VertexPositionColorNormal(vertices[current], c, normales[current]));
                    vertexes.Add(new VertexPositionColorNormal(vertices[next + 1], c, normales[next + 1]));
                    vertexes.Add(new VertexPositionColorNormal(vertices[next], c, normales[next]));
                }
            }

            //Bottom Cap
            for (int lon = 0; lon < nbLong; lon++)
            {
                vertexes.Add(new VertexPositionColorNormal(vertices[vertices.Length - 1], c, normales[vertices.Length - 1]));
                vertexes.Add(new VertexPositionColorNormal(vertices[vertices.Length - (lon + 2) - 1], c, normales[vertices.Length - (lon + 2) - 1]));
                vertexes.Add(new VertexPositionColorNormal(vertices[vertices.Length - (lon + 1) - 1], c, normales[vertices.Length - (lon + 1) - 1]));
            }
            #endregion

            Vertexes = vertexes.ToArray();
        }
    }
}
