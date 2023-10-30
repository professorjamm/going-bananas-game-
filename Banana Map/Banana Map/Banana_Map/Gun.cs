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

    class Gun
    {
        Texture2D[] GunText;
        public float rotationDegrees, rotationRadians;
        Vector2 screenPos, origin = new Vector2(100, 40);
        double deltaX, deltaY;
        public int timer = 40;

        int index;

        public Gun(Texture2D[] GunTexture)
        {
            GunText = GunTexture;
        }

        public void update(MouseState Mouse, Vector2 ScreenPos)
        {
            if (timer > 0)
                timer--;

            screenPos = ScreenPos;
            deltaX = screenPos.X - Mouse.X;
            deltaY = screenPos.Y - Mouse.Y;

            double angle = Math.Atan2(deltaY, deltaX) + Math.PI / 2;
            rotationRadians = (float)angle;
            rotationDegrees = MathHelper.ToDegrees(rotationRadians);
            rotationRadians = MathHelper.ToRadians(rotationDegrees - 90);
            rotationDegrees = (rotationDegrees + 360) % 360;

            if (rotationDegrees >= 180)
                index = 1;
            else if (rotationDegrees >= 0)
                index = 0;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GunText[index], screenPos, null, Color.White, rotationRadians, origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }

}
