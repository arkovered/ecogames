using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWindowSize.GraphicsSupport
{
    class FontSupport
    {
        static private SpriteFont sTheFont = null;
        static private Color sDefaultDrawColor = Color.Black;
        static private Vector2 sStatusLocation = new Vector2(5, 5);

        static private void LoadFont()
        {
            /*Inicializa a variável	SpriteFont e carrega a fonte de	texto Arial*/
            if (null == sTheFont)
                sTheFont = Game1.sContent.Load<SpriteFont>("Arial");
        }

        /*muda a cor */
        static private Color ColorToUse(Nullable<Color> c)
        {
            return (null == c) ? sDefaultDrawColor : (Color)c;
        }

        /*Escreve uma mensagem no canto superior esquerdo da janela*/
        static public void PrintStatus(String msg, Nullable<Color> drawColor)
        {
            LoadFont();
            Color useColor = ColorToUse(drawColor);
            // Compute top-left corner as the reference for output status
            Game1.sSpriteBatch.DrawString(sTheFont, msg, sStatusLocation, useColor);
        }

        /*Localização do que a funcao PrintStatus escreve*/
      static public void PrintStatusAt(Vector2 pos, String msg, Nullable<Color> drawColor)
        {
            LoadFont();
            Color useColor = ColorToUse(drawColor);
            int pixelX, pixelY;
            Camera.ComputePixelPosition(pos, out pixelX, out pixelY);
            Game1.sSpriteBatch.DrawString(sTheFont, msg,
           new Vector2(pixelX, pixelY), useColor);
        }

        internal static void PrintStatus(object p1, object p2)
        {
            throw new NotImplementedException();
        }
    }
}
