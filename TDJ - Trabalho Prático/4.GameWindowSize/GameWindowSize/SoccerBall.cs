using BookExample;
using GameWindowSize.GraphicsSupport;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWindowSize
{
    public class SoccerBall : TexturedPrimitive
    {
        private Vector2 mDeltaPosition; // Change current position by this amount

        // Passa a informação do tamanho e posição da bola para a class TexturedPrimitive
        public SoccerBall(Vector2 position, float diameter) : base("Soccer", position, new Vector2(diameter, diameter))
        {
            mDeltaPosition.X = (float)(Game1.sRan.NextDouble()) * 2f - 1f;
            mDeltaPosition.Y = (float)(Game1.sRan.NextDouble()) * 2f - 1f;
        }

        // Aumenta e diminui o raio da bola
        public float Radius
        {
            get { return mSize.X * 0.5f; }
            set { mSize.X = 2f * value; mSize.Y = mSize.X; }
        }

        // Define a localização da bola após colidir
        public void Update()
        {
            Camera.CameraWindowCollisionStatus status = Camera.CollidedWithCameraWindow(this);
            switch (status)
            {
                case Camera.CameraWindowCollisionStatus.CollideBottom:
                case Camera.CameraWindowCollisionStatus.CollideTop:
                    mDeltaPosition.Y *= -1;
                    break;
                case Camera.CameraWindowCollisionStatus.CollideLeft:
                case Camera.CameraWindowCollisionStatus.CollideRight:
                    mDeltaPosition.X *= -1;
                    break;
            }
            Position += mDeltaPosition;
        }

    }
}
