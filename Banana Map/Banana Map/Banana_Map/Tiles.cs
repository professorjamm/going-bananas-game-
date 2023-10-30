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
using System.IO;

namespace Banana_Map
{
    class Tiles
    {
        int X, Y,ObjectIndex;
        Texture2D[] ObjectText;
        Rectangle R, left, right, top, bottom;
        Rectangle[] recArr;

        public Tiles (int XLocation, int YLocation ,int ObjIndex, Texture2D[] objArray)
        {
            X = XLocation;
            Y = YLocation;
            ObjectIndex = ObjIndex;
            ObjectText = objArray;        

            R = new Rectangle(420 + (Y * 100), 30 + (X * 100), 290, 290);
            
            left = new Rectangle(R.X, R.Y + 10, 10, R.Height/2 - 20);
            top = new Rectangle(R.X, R.Y, R.Width, 10);
            right = new Rectangle(R.X+(R.Width-10), R.Y + 10, 10, R.Height/2 - 20);
            bottom = new Rectangle(R.X, R.Y+(R.Height/2-10), R.Width, 10);

            if (ObjectIndex == 0)//Horizontal Table
            {
                right.X -= 30; right.Height -= 30;
                left.X += 30; left.Height -= 30;
                top.X += 30;top.Width -= 60;
                bottom.X += 30;bottom.Width -= 60; bottom.Y -= 30;
            }
            else if (ObjectIndex == 1)//Vertical Table
            {
                left.X += 30; left.Height += 70;
                bottom.Y += 70; bottom.X += 30; bottom.Width -= 240;
                top.X += 30; top.Width -= 240;
                right.X -= 200;

            }
            else if (ObjectIndex == 2)//TV
            {
                left.X += 35;
                top.X += 35; top.Width -= 70;
                bottom.X += 35; bottom.Width -= 70;
                right.Width -= 35;
            }
            else if (ObjectIndex == 3)//Bookshelf
            {
                right.X -= 30;
                left.X += 30;
                top.X += 30; top.Width -= 60;
                bottom.X += 30; bottom.Width -= 60;
            }

            recArr = new Rectangle[4]; recArr[0] = left; recArr[1] = right; recArr[2] = top; recArr[3] = bottom;
        }

        public Rectangle[] getRecArr()
        {
            return recArr;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ObjectText[ObjectIndex], R, Color.White);
            //for(int i=0;i<4;i++)
            //    spriteBatch.Draw(ObjectText[2],recArr[i], Color.White);
        }
    }
}
