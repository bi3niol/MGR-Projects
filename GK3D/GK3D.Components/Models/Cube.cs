using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.Models
{
    public class Cube : BaseModel<VertexPositionColorNormal>
    {
        public Cube(GraphicsDeviceManager graphics, BasicEffect effect, float size, Color color) : base(graphics, effect)
        {
            Vertexes = new VertexPositionColorNormal[36];
            var Size = new Vector3(size);
           
            Vector3 topLeftFront = new Vector3(-1.0f, 1.0f, -1.0f) * Size;
            Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, 1.0f) * Size;
            Vector3 topRightFront = new Vector3(1.0f, 1.0f, -1.0f) * Size;
            Vector3 topRightBack = new Vector3(1.0f, 1.0f, 1.0f) * Size;

            // Calculate the position of the vertices on the bottom face.
            Vector3 btmLeftFront = new Vector3(-1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmLeftBack = new Vector3(-1.0f, -1.0f, 1.0f) * Size;
            Vector3 btmRightFront = new Vector3(1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmRightBack = new Vector3(1.0f, -1.0f, 1.0f) * Size;

            // Normal vectors for each face (needed for lighting / display)
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 normalBack = new Vector3(0.0f, 0.0f, -1.0f);
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f);
            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f);
            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f);

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2(1.0f * Size.X, 0.0f * Size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * Size.X, 0.0f * Size.Y);
            Vector2 textureBottomLeft = new Vector2(1.0f * Size.X, 1.0f * Size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * Size.X, 1.0f * Size.Y);

            // Add the vertices for the FRONT face.
            Vertexes[0] = new VertexPositionColorNormal(topLeftFront, color, normalFront);
            Vertexes[1] = new VertexPositionColorNormal(btmLeftFront, color, normalFront);
            Vertexes[2] = new VertexPositionColorNormal(topRightFront, color, normalFront);
            Vertexes[3] = new VertexPositionColorNormal(btmLeftFront, color, normalFront);
            Vertexes[4] = new VertexPositionColorNormal(btmRightFront, color, normalFront);
            Vertexes[5] = new VertexPositionColorNormal(topRightFront, color, normalFront);

            // Add the vertices for the BACK face.
            Vertexes[6] = new VertexPositionColorNormal(topLeftBack, color, normalBack);
            Vertexes[7] = new VertexPositionColorNormal(topRightBack, color, normalBack);
            Vertexes[8] = new VertexPositionColorNormal(btmLeftBack, color, normalBack);
            Vertexes[9] = new VertexPositionColorNormal(btmLeftBack, color, normalBack);
            Vertexes[10] = new VertexPositionColorNormal(topRightBack, color, normalBack);
            Vertexes[11] = new VertexPositionColorNormal(btmRightBack, color, normalBack);

            // Add the vertices for the TOP face.
            Vertexes[12] = new VertexPositionColorNormal(topLeftFront, color, normalTop);
            Vertexes[13] = new VertexPositionColorNormal(topRightBack, color, normalTop);
            Vertexes[14] = new VertexPositionColorNormal(topLeftBack, color, normalTop);
            Vertexes[15] = new VertexPositionColorNormal(topLeftFront, color, normalTop);
            Vertexes[16] = new VertexPositionColorNormal(topRightFront, color, normalTop);
            Vertexes[17] = new VertexPositionColorNormal(topRightBack, color, normalTop);

            // Add the vertices for the BOTTOM face. 
            Vertexes[18] = new VertexPositionColorNormal(btmLeftFront, color, normalTop);
            Vertexes[19] = new VertexPositionColorNormal(btmLeftBack, color, normalTop);
            Vertexes[20] = new VertexPositionColorNormal(btmRightBack, color, normalTop);
            Vertexes[21] = new VertexPositionColorNormal(btmLeftFront, color, normalTop);
            Vertexes[22] = new VertexPositionColorNormal(btmRightBack, color, normalTop);
            Vertexes[23] = new VertexPositionColorNormal(btmRightFront, color, normalBottom);

            // Add the vertices for the LEFT face.
            Vertexes[24] = new VertexPositionColorNormal(topLeftFront, color, normalLeft);
            Vertexes[25] = new VertexPositionColorNormal(btmLeftBack, color, normalLeft);
            Vertexes[26] = new VertexPositionColorNormal(btmLeftFront, color, normalLeft);
            Vertexes[27] = new VertexPositionColorNormal(topLeftBack, color, normalLeft);
            Vertexes[28] = new VertexPositionColorNormal(btmLeftBack, color, normalLeft);
            Vertexes[29] = new VertexPositionColorNormal(topLeftFront, color, normalLeft);

            // Add the vertices for the RIGHT face. 
            Vertexes[30] = new VertexPositionColorNormal(topRightFront, color, normalRight);
            Vertexes[31] = new VertexPositionColorNormal(btmRightFront, color, normalRight);
            Vertexes[32] = new VertexPositionColorNormal(btmRightBack, color, normalRight);
            Vertexes[33] = new VertexPositionColorNormal(topRightBack, color, normalRight);
            Vertexes[34] = new VertexPositionColorNormal(topRightFront, color, normalRight);
            Vertexes[35] = new VertexPositionColorNormal(btmRightBack, color, normalRight);

            //// Add the vertices for the FRONT face.
            //Vertexes[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureTopLeft);
            //Vertexes[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            //Vertexes[2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);
            //Vertexes[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            //Vertexes[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureBottomRight);
            //Vertexes[5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);

            //// Add the vertices for the BACK face.
            //Vertexes[6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
            //Vertexes[7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            //Vertexes[8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            //Vertexes[9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            //Vertexes[10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            //Vertexes[11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);

            //// Add the vertices for the TOP face.
            //Vertexes[12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            //Vertexes[13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);
            //Vertexes[14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureTopLeft);
            //Vertexes[15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            //Vertexes[16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureBottomRight);
            //Vertexes[17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);

            //// Add the vertices for the BOTTOM face. 
            //Vertexes[18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            //Vertexes[19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureBottomLeft);
            //Vertexes[20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            //Vertexes[21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            //Vertexes[22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            //Vertexes[23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureTopRight);

            //// Add the vertices for the LEFT face.
            //Vertexes[24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);
            //Vertexes[25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            //Vertexes[26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureBottomRight);
            //Vertexes[27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureTopLeft);
            //Vertexes[28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            //Vertexes[29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);

            //// Add the vertices for the RIGHT face. 
            //Vertexes[30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            //Vertexes[31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureBottomLeft);
            //Vertexes[32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);
            //Vertexes[33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureTopRight);
            //Vertexes[34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            //Vertexes[35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);
        }
    }
}
