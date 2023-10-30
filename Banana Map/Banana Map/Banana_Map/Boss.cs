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
    class Boss
    {
        Texture2D[] IdleText, WalkText;
        Texture2D deadText;
        Rectangle[] idle = new Rectangle[6], walk =new Rectangle[12], Dying = new Rectangle[17];
        public int index;
        bool walking, dead;
        public Rectangle locRec = new Rectangle(480, -100, 288 * 3, 160 * 3);
        public double health = 150;
        public List<Enemy> enemy = new List<Enemy>();

        public int curEnemy = 0;

        Rectangle[] locRecList = new Rectangle[6];

        static int max = 1;

        Texture2D ballTex,enenenene;

        public Rectangle[] ballList;
        bool[] bouncy;
        int[] xSpeed;
        int[] ySpeed;

        int iWantToCry = 0;
    

        public Boss(Texture2D[] id, Texture2D[] wa,Texture2D ball, Texture2D enemyText, Texture2D dying)
        {
            IdleText = id;
            WalkText = wa;
            deadText = dying;
            for (int i = 0; i < idle.Length; i++)
            {
                idle[i] = new Rectangle(288 * i, 0, 288, 160);
            }
            for (int i = 0; i < walk.Length; i++)
            {
                if (i < 6)
                    walk[i] = new Rectangle(288 * i, 0, 288, 160);
                else if (i >= 6)
                    walk[i] = new Rectangle(288 * (i - 6), 160, 288, 160);
            }
            for (int i = 0; i < Dying.Length; i++)
            {
                if (i < 6)
                    Dying[i] = new Rectangle(288 * i, 0, 288, 160);
                else if (i >= 6)
                    Dying[i] = new Rectangle(288 * (i - 6), 160, 288, 160);
                else if (i >= 12)
                    Dying[i] = new Rectangle(288 * (i - 12), 160, 288, 160);
            }
            ballList = new Rectangle[10];
            bouncy = new bool[10];
            xSpeed = new int[10];
            ySpeed = new int[10];
            int z = 25;
            Random rnd = new Random();
            for(int i=0;i<ballList.Length;i++)
            {
                bouncy[i] = true;
                ballList[i]= new Rectangle(1920 / 2 - 50, 50 * 14 - 450, 90, 90);
                xSpeed[i] = z;
                ySpeed[i] = rnd.Next(5,10);
                z-=5;
                if (z == 0)
                    z = -5;
            }
            
            ballTex = ball;
            locRecList[0] = new Rectangle(200, 200, 100, 100);
            locRecList[1] = new Rectangle(1600, 200, 100, 100);
            locRecList[2] = new Rectangle(200, 800, 100, 100);
            locRecList[3] = new Rectangle(1600, 800, 100, 100);
            locRecList[4] = new Rectangle(200, 500, 100, 100);
            locRecList[5] = new Rectangle(1600, 500, 100, 100);
            enenenene = enemyText;

        }
        public void update(int time, Player player, List<Bullet> bullet, Stats stat)
        {
            if (health <= 0 && !dead)
            {
                index = 0;
                dead = true;
                if (enemy.Count > 0)
                {
                    for (int i = 0; i < enemy.Count; i++)
                    {
                        enemy.Remove(enemy[i]);
                    }

                }
            }
            if (time % 13 == 0)
                {
                    if (health <= 0)
                {
                    if (index == 16 || index == -1)
                        index = -1;
                    else
                        index += 1;
                }
                    else if (!walking)
                    {
                        if (index == 5)
                            index = 0;
                        else
                            index += 1;
                    }
                    else if (walking)
                    {
                        if (index == 11)
                            index = 0;
                        else
                            index += 1;
                    }
                }
            if (health <= 100)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    enemy[i].Update(time, player, bullet);
                    if (enemy[i].Kill(bullet, stat))
                    {
                        enemy.Remove(enemy[i]);
                        stat.sanity += 4;
                        stat.hp += 2;   
                        curEnemy--;
                    }
                }
            }

        }
        public void attack(int time, Rectangle playerRec)
        {
            if (health > 100 && time > 120)
            {
                if (max != 10 && time % 45 == 0)
                    max++;
                for (int i = 0; i < max; i++)
                {
                    addX(i);
                    addY(i);

                    if (ballList[i].Y <= 20 || ballList[i].Y >= 930)
                    {
                        ySpeed[i] *= -1;
                        bouncy[i] = true;
                    }
                    if (ballList[i].X >= 1900 || ballList[i].X <= 20)
                    {
                        xSpeed[i] *= -1;
                        bouncy[i] = true;
                    }
                    if (ballList[i].Intersects(playerRec)&&bouncy[i]==true)
                    {
                        xSpeed[i] *= -1;
                        ySpeed[i] *= -1;
                        bouncy[i] = false;
                    }
                }
                
            }
            else if(health<=100&&health>50)
            {
                walking = true;
                if(curEnemy < 3&&time%180==0)
                {
                    enemy.Add(new Enemy(enenenene, locRecList[iWantToCry],25));
                    
                    curEnemy++;
                    iWantToCry++;
                    if (iWantToCry > 5)
                        iWantToCry = 0;
                }
            }
                
        }


        public void addX(int i)
        {
            ballList[i] = new Rectangle(ballList[i].X + xSpeed[i], ballList[i].Y, ballList[i].Width, ballList[i].Height);
        }

        public void addY(int i)
        {
            ballList[i] = new Rectangle(ballList[i].X, ballList[i].Y + ySpeed[i], ballList[i].Width, ballList[i].Height);
        }

        public void changeX(int x)
        {
            locRec.X += x;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (health <= 0)
            {
                spriteBatch.Draw(deadText, locRec, Dying[index], Color.White);
            }
            else if (!walking)
            {
                spriteBatch.Draw(IdleText[0], locRec, idle[index], Color.White);
                if(health>50)
                {
                    for (int i = 0; i < ballList.Length; i++)
                    {
                        spriteBatch.Draw(ballTex, ballList[i], Color.White);
                    }
                }
                
            }
            else if (walking)
                spriteBatch.Draw(WalkText[0], locRec, walk[index], Color.White);
            
            if (enemy.Count > 0)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    enemy[i].Draw(spriteBatch);
                }

            }
        }
    }
}
