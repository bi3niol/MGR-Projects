using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Components.SceneObjects
{
    public class Camera
    {
        private Vector3 _position;
        public Vector3 Position
        {
            get => _position; set
            {
                if (_position == value)
                    return;
                _position = value;
                UpdateViewMatrix();
            }
        }

        private Vector3 _up;
        public Vector3 Up
        {
            get => _up; set
            {
                if (_up == value)
                    return;
                _up = value;
                UpdateViewMatrix();
            }
        }

        private Vector3 _lookAt;
        public Vector3 LookAt
        {
            get => _lookAt; set
            {
                if (_lookAt == value)
                    return;
                _lookAt = value;
                UpdateViewMatrix();
            }
        }

        private Matrix viewMAtrix;
        public Matrix ViewMatrix { get => viewMAtrix; set => viewMAtrix = value; }

        private void UpdateViewMatrix()
        {
            viewMAtrix = Matrix.CreateLookAt(_position, _lookAt, _up);
        }
    }
}
