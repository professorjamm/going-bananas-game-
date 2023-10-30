using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Banana_Map
{
    class Enemy
    {
        int enemySpeedX, spriteIndex, enemySpeedY, enemyHP;
        public int x, y;
        Texture2D enemy;
        public Rectangle enemyDest;
        Rectangle[] moveSheet;

        public Enemy(Texture2D text, Rectangle EnemyDest, int healthP)
        {
            enemy = text;
            //enemyDest = new Rectangle(500, 540, 100, 100);
            enemyDest = EnemyDest;
            enemySpeedY = 2;
            enemySpeedX = 2;
            moveSheet = new Rectangle[4];
            spriteIndex = 0;
            enemyHP = healthP;
        }

        public void Update(int timer, Player play, List<Bullet> bullet)
        {
            if (timer % 6 == 0)
            {
                spriteIndex++;
                if (spriteIndex == 4)
                    spriteIndex = 0;
            }

            if (enemyDest.X > play.getRec().X)
                enemyDest.X -= enemySpeedX;
            else if (enemyDest.X < play.getRec().X)
                enemyDest.X += enemySpeedX;
            if (enemyDest.Y > play.getRec().Y)
                enemyDest.Y -= enemySpeedY;
            else if (enemyDest.Y < play.getRec().Y)
                enemyDest.Y += enemySpeedY;



            if (bullet.Count > 0)
                for (int i = 0; i < bullet.Count; i++)
                {
                    if (enemyDest.Intersects(bullet[i].getRect()))
                    {

                        bullet[i].velocity = 0;
                    }

                }

            for (int i = 0; i < moveSheet.Length; i++)
            {
                moveSheet[i] = new Rectangle((i % 3) * 512, 512 * (int)((double)i / (double)3), 511, 512);
            }

        }
        public bool Intersect(Player play)
        {
            if (enemyDest.Intersects(play.getRec()))
            {
                if (enemyDest.X >= play.getRec().X)
                    enemyDest = new Rectangle(enemyDest.X + 80, enemyDest.Y, enemyDest.Width, enemyDest.Height);
                if (enemyDest.X <= play.getRec().X)
                    enemyDest = new Rectangle(enemyDest.X - 80, enemyDest.Y, enemyDest.Width, enemyDest.Height);
                if (enemyDest.Y >= play.getRec().Y)
                    enemyDest = new Rectangle(enemyDest.X, enemyDest.Y + 80, enemyDest.Width, enemyDest.Height);
                if (enemyDest.Y <= play.getRec().Y)
                    enemyDest = new Rectangle(enemyDest.X, enemyDest.Y - 80, enemyDest.Width, enemyDest.Height);
                return true;
            }
            return false;
        }
        public bool Kill(List<Bullet> bullet, Stats stat)
        {
            if (bullet.Count > 0)
                for (int i = 0; i < bullet.Count; i++)
                {
                    if (enemyDest.Intersects(bullet[i].getRect()))
                    {
                        bullet[i].velocity = 0;
                        enemyHP -= stat.damage - ((bullet[i].airTime / 60) * 2);
                        if (enemyHP <= 0)

                            return true;
                        else
                        {
                            if (enemyDest.X >= bullet[i].getRect().X)
                                enemyDest = new Rectangle(enemyDest.X + 60, enemyDest.Y, enemyDest.Width, enemyDest.Height);
                            if (enemyDest.X <= bullet[i].getRect().X)
                                enemyDest = new Rectangle(enemyDest.X - 60, enemyDest.Y, enemyDest.Width, enemyDest.Height);
                            if (enemyDest.Y >= bullet[i].getRect().Y)
                                enemyDest = new Rectangle(enemyDest.X, enemyDest.Y + 60, enemyDest.Width, enemyDest.Height);
                            if (enemyDest.Y <= bullet[i].getRect().Y)
                                enemyDest = new Rectangle(enemyDest.X, enemyDest.Y - 60, enemyDest.Width, enemyDest.Height);
                            return false;
                        }
                    }

                }
            return false;


        }
        public void changeRec(Rectangle recc)
        {
            enemyDest = recc;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemy, enemyDest, moveSheet[spriteIndex], Color.White);
        }
    }
}
