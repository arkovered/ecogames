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
    public class BasketBall : TexturedPrimitive
    {
        private const float kIncreaseRate = 1.001f;//rate at which the basketball increases in size.
        private Vector2 kInitSize = new Vector2(5, 5);//initial size of the basketball.
        private const float kFinalSize = 15f;//size at which the basketball will explode.

        /*Gera as bolas de basket numa posição random e com o tamanho mínimo*/
        public BasketBall() : base("BasketBall", InputWrapper.ThumbSticks.Right, InputWrapper.ThumbSticks.Left)
        {
            mPosition = Camera.RandomPosition();
            mSize = kInitSize;
        }

        //Bola cresce e explode num certo tamanho.
        public bool UpdateAndExplode()
        {
            mSize *= kIncreaseRate;
            return mSize.X > kFinalSize;
        }


    }
}
