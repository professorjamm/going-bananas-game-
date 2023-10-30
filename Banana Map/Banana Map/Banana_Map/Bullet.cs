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
    class Bullet
    {
        Texture2D BulletText;
        Vector2 screenPos2, origin2 = new Vector2(100, 40);
        double deltaX2, deltaY2, hypotenuse, xVel, yVel;
        public double velocity = 50;
        public int airTime = 0;

        float RR;

        public Bullet(Texture2D bulletText, float rotaionRadians, MouseState mouse, Vector2 ScreenPos)
        {
            screenPos2 = ScreenPos;
            BulletText = bulletText;
            RR = rotaionRadians;

            deltaX2 = screenPos2.X - mouse.X;
            deltaY2 = screenPos2.Y - mouse.Y;

            hypotenuse = Math.Sqrt(((Math.Pow(deltaX2, 2) + (Math.Pow(deltaY2, 2)))));

            xVel = -velocity * deltaX2 / hypotenuse;
            yVel = -velocity * deltaY2 / hypotenuse;
        }
        public void update()
        {
            airTime += 1;

            screenPos2.X += (float)xVel;
            screenPos2.Y += (float)yVel;
        }
        public Rectangle getRect()
        {
            return new Rectangle((int)screenPos2.X, (int)screenPos2.Y, 32, 32);
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BulletText, screenPos2, null, Color.White, RR, origin2, 1.0f, SpriteEffects.None, 0.0f);
        }

    }
}
