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
    class MyGame
    {
        // Hero stuff
        TexturedPrimitive mHero;
        Vector2 kHeroSize = new Vector2(15, 15);
        Vector2 kHeroPosition = new Vector2(22, 30);

        // Basketballs stuff
        List<BasketBall> mBBallList;
        TimeSpan mCreationTimeStamp;
        int mTotalBBallCreated = 0;
        // this is 0.5 seconds
        const int kBballMSecInterval = 500;

        // Game state
        int mScore = 0;
        int mBBallMissed = 0, mBBallHit = 0;
        const int kBballTouchScore = 1;
        const int kBballMissedScore = -2;
        const int kWinScore = 10;
        const int kLossScore = -10;
        TexturedPrimitive mFinal = null;

        // Inicializa a personagem em x posiçao com x tamanho
        public MyGame()
        {
            // Hero ...
            mHero = new TexturedPrimitive("Me", kHeroPosition, kHeroSize);
            // Basketballs
            mCreationTimeStamp = new TimeSpan(0);
            mBBallList = new List<BasketBall>();
        }


        public void UpdateGame(GameTime gameTime)
        {
            // Verifica se o jogo chegou ao final
            #region Step a.
            if (null != mFinal) // Done!!
                return;
            #endregion Step a.

            // Remove as bolas que explodiram e actualiza o score
            #region Step b.
            // Hero movement: right thumb stick
            mHero.Update(InputWrapper.ThumbSticks.Right, InputWrapper.ThumbSticks.Left);
            // Basketball ...
            for (int b = mBBallList.Count - 1; b >= 0; b--)
            {
                if (mBBallList[b].UpdateAndExplode())
                {
                    mBBallList.RemoveAt(b);
                    mBBallMissed++;
                    mScore += kBballMissedScore;
                }
            }
            #endregion Step b.

            // Remove as bolas "apanhadas" pelo herói e atualiza o score
            #region Step c.
            for (int b = mBBallList.Count - 1; b >= 0; b--)
            {
                if (mHero.PrimitivesTouches(mBBallList[b]))
                {
                    mBBallList.RemoveAt(b);
                    mBBallHit++;
                    mScore += kBballTouchScore;
                }
            }
            #endregion Step c.

            // Cria novas bolas
            #region Step d.
            // Check for new basketball condition
            TimeSpan timePassed = gameTime.TotalGameTime;
            timePassed = timePassed.Subtract(mCreationTimeStamp);
            if (timePassed.TotalMilliseconds > kBballMSecInterval)
            {
                mCreationTimeStamp = gameTime.TotalGameTime;
                BasketBall b = new BasketBall();
                mTotalBBallCreated++;
                mBBallList.Add(b);
            }
            #endregion Step d.

            // Verifica se ganhou ou perdeu
            #region Step e.
            // Check for winning condition ...
            if (mScore > kWinScore)
                mFinal = new TexturedPrimitive("Winner",
                new Vector2(75, 50), new Vector2(30, 20));
            else if (mScore < kLossScore)
                mFinal = new TexturedPrimitive("Looser",
                new Vector2(75, 50), new Vector2(30, 20));
            #endregion Step e.
        }

        // Desenha a personagem e as bolas de basket
        public void DrawGame()
        {
            mHero.Draw();
            foreach (BasketBall b in mBBallList)
                b.Draw();
            if (null != mFinal)
                mFinal.Draw();
            // Drawn last to always show up on top
            FontSupport.PrintStatus("Status: " +
            "Score=" + mScore +
            " Basketball: Generated( " + mTotalBBallCreated +
            ") Collected(" + mBBallHit + ") Missed(" + mBBallMissed + ")",
            null);
        }
    }
}
