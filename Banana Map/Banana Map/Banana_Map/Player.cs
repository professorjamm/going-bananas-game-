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
    class Player
    {
        Texture2D moveTex, leftTex, rightTex, idleTex;
        Texture2D[] texArr, Insane;
        Rectangle[] moveSheet;
        int spriteIndex, texIndex;
        public Rectangle locRec;

        public Player(Texture2D tm, Texture2D tl, Texture2D tr, Texture2D ti, int x, int y, Texture2D[] insane)
        {
            spriteIndex = 0; texIndex = 0;
            moveTex = tm;
            leftTex = tl;
            rightTex = tr;
            idleTex = ti;
            texArr = new Texture2D[3]; texArr[1] = leftTex; texArr[2] = rightTex; texArr[0] = idleTex;
            moveSheet = new Rectangle[8];
            Insane = insane;
            for (int i = 0; i < moveSheet.Length; i++)
            {
                moveSheet[i] = new Rectangle((i % 3) * 512, 512 * (int)((double)i / (double)3), 512, 512);
            }
            locRec = new Rectangle(x-60, y+400, 128,128);
        }

        public void Update(int timer)
        {
            KeyboardState kb = Keyboard.GetState();

            //going through spritesheet 
            if (timer % 6 == 0)
            {
                spriteIndex++;
                if (spriteIndex == 8)
                    spriteIndex = 0;
            }

            //movement
            int bruhaps = 4;
            if (kb.IsKeyDown(Keys.Left))
            {
                locRec.X -= bruhaps;
            }
            else if (kb.IsKeyDown(Keys.Right))
            {
                locRec.X += bruhaps;
            }
            if (kb.IsKeyDown(Keys.Up))
            {
                locRec.Y -= bruhaps;
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                locRec.Y += bruhaps;
            }

            //texture change
            if (kb.IsKeyDown(Keys.Left) && !kb.IsKeyDown(Keys.Right))
            {
                texIndex = 1;
            }
            else if (!kb.IsKeyDown(Keys.Left) && kb.IsKeyDown(Keys.Right))
            {
                texIndex = 2;
            }
            else if (kb.IsKeyDown(Keys.Up) || kb.IsKeyDown(Keys.Down))
            {
                texIndex = 2;
            }
            if (!kb.IsKeyDown(Keys.Left) && !kb.IsKeyDown(Keys.Right) && !kb.IsKeyDown(Keys.Up) && !kb.IsKeyDown(Keys.Down))
            {
                texIndex = 0;
            }
        }

        public Rectangle getRec()
        {
            return locRec;
        }

        public void changeRec(Rectangle recc)
        {
            locRec = recc;
        }

        public void Draw(SpriteBatch spriteBatch, Color c, Stats stat)
        {
            if (stat.sanity <= 60)
            spriteBatch.Draw(texArr[texIndex], locRec, moveSheet[spriteIndex], c);
            else if (stat.sanity >= 60 || stat.hp <=20)
                texArr = Insane;
                spriteBatch.Draw(texArr[texIndex], locRec, moveSheet[spriteIndex], c);
        }

    }
}
