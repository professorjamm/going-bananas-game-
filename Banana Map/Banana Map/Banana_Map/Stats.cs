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
    class Stats
    {
        public int hp = 100, sanity = 0, damage, maxHp = 100;
        Texture2D hpBar, Pixel, SanityText, hpText, corrupted, controls;
        Texture2D[] FakeText;
        Random R = new Random();
        int index = 0;
        float opacity = 0.6f;
        Color Health;

        public Stats(Texture2D Bar, Texture2D pixel, Texture2D[] fakeText, Texture2D sanBar, Texture2D HP, Texture2D Corrupt, Texture2D Controls)
        {
            hpBar = Bar;
            Pixel = pixel;
            FakeText = fakeText;
            SanityText = sanBar;
            hpText = HP;
            corrupted = Corrupt;
            controls = Controls;
        }
        public void update()
        {
            damage = 5 + (sanity / 10);
            Health = new Color(128 + ((int)Math.Ceiling(1.5 * hp)), 112 + (((int)Math.Ceiling(1.5 * hp))), 27 + ((int)Math.Ceiling(0.4 * hp))); //127

            if (maxHp > 10)
                maxHp = 100 - sanity;
            else
                maxHp = 10;
            if (hp > maxHp)
                hp = maxHp;

        }
        public void EnemyKilled()
        {
            sanity += 1;
        }


        public void damageTaken(int enemyDmg)
        {
            hp -= enemyDmg;
        }
        public Rectangle Hallucination(Rectangle playerRec)
        {
            if (R.Next(-6900, sanity) >= 15)
            {
                int x = R.Next((1920 / 2) - 540, ((1920 / 2) - 540) + 1080);
                if (x <= 550)
                    index = 1;
                else
                    index = 0;
                return new Rectangle(x, playerRec.Y, 100, 100);
            }
            else
                return new Rectangle(0, 0, 0, 0);

        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Pixel, new Rectangle(10, 50, (int)Math.Ceiling(3.8 * hp), 45), Health);
            spriteBatch.Draw(hpBar, new Rectangle(10, 50, 380, 60), Color.White); //760, 250
            spriteBatch.Draw(hpText, new Rectangle(10, 10, 380, 50), Color.White);
            //spriteBatch.Draw(corrupted, new Rectangle(10+  (int)Math.Round(3.8 * maxHp), 50, (int)Math.Round(3.8 * sanity), 60), new Rectangle((int)Math.Round(7.6 * maxHp), 0, (int)Math.Round(7.6 * maxHp), 100), Color.White);
            for (int i = 0; i < sanity / 10; i++)
            {
                spriteBatch.Draw(corrupted, new Rectangle(390 - (i * 38) - 38, 50, 38, 60), new Rectangle(760 - (76 * i) - 76, 0, 76, 100), Color.White);
            }
            //760 x 100
            spriteBatch.Draw(Pixel, new Rectangle(10, 206, 380 - (int)Math.Ceiling(3.8 * sanity), 32), Color.LimeGreen);
            spriteBatch.Draw(SanityText, new Rectangle(10, 160, 380, 125), Color.White); //760, 250
            spriteBatch.Draw(controls, new Rectangle(1600, 10, 380, 125), Color.White);
            //
        }
        public void draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(FakeText[index], rect, new Rectangle(0, 0, 512, 512), Color.White * opacity);
        }

    }
}
