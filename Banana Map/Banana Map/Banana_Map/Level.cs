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
    class Level
    {
        Tiles[,] Tile;
        Texture2D Map, EnemyText;
        Texture2D[] Table;
        public Rectangle MapRect, TableRec;
        const int Bookshelf = 3, TV = 2, Vable = 1, Hable = 0;
        public List<Enemy> enemy = new List<Enemy>();
        public String[] levelLines = new String[9];
        int j = -1;
        String DaFile;
        public Level(String levelNum, Texture2D Map_, Texture2D TabH, Texture2D TabV, Texture2D TV_, Texture2D Book, Texture2D enemyText)
        {
            Map = Map_;
            EnemyText = enemyText;
            Table = new Texture2D[4];
            Table[0] = TabH;
            Table[1] = TabV;
            Table[2] = TV_;
            Table[3] = Book;
            LoadContent(levelNum);

        }

        public void LoadContent(String levelNum)
        {
            int screenWidth = 1920;
            int screenHeight = 1080;
            MapRect = new Rectangle((screenWidth / 2) - 540, 0, screenHeight, screenHeight); //1400
            TableRec = new Rectangle((screenWidth / 2) - 550, -75, 128, 128); //512
            //tile 100 x 100 
            Tile = new Tiles[14, 14];
            DaFile = @"Content/levelText/Level " + levelNum + ".txt";
            ReadFileAsStrings(DaFile);

        }

        private void ReadFileAsStrings(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        for (int e = 0; e < 9; e++)
                        {
                            string line = reader.ReadLine();
                            levelLines[e] = line;
                            for (int i = 0; i < 9; i++)
                            {
                                Char A = line.ElementAt(i);
                                switch (A)
                                {
                                    case 'x':
                                        Tile[i, e] = null;
                                        break;
                                    case 'b':
                                        Tile[i, e] = new Tiles(e, i, Bookshelf, Table);
                                        break;
                                    case 'v':
                                        Tile[i, e] = new Tiles(e, i, Vable, Table);
                                        break;
                                    case 'h':
                                        Tile[i, e] = new Tiles(e, i, Hable, Table);
                                        break;
                                    case 't':
                                        Tile[i, e] = new Tiles(e, i, TV, Table);
                                        break;
                                    case 'e':
                                        Tile[i, e] = null;
                                        enemy.Add(new Enemy(EnemyText, new Rectangle(420 + (i * 100), 30 + (e * 100), 100, 100),50));
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("This file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }

        public Rectangle intersec(Rectangle rec, Rectangle rec2)
        {
            bool isEnemy = true;
            if (rec2.X == -100)
            {
                isEnemy = false;
            }

            //FIX THIS 

            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    Tiles current = Tile[i, j];
                    if (current != null)
                    {
                        Rectangle[] reclist = current.getRecArr();
                        for (int x = 0; x < reclist.Length; x++)
                        {
                            if (rec.Intersects(reclist[x]) && x == 0) //left
                            {
                                if (isEnemy)
                                {
                                    if (rec2.Y >= reclist[x].Y + reclist[x].Height)
                                        return new Rectangle(rec.X - 2, rec.Y + 4, rec.Width, rec.Height);
                                    else
                                        return new Rectangle(rec.X - 2, rec.Y - 4, rec.Width, rec.Height);
                                }
                                return new Rectangle(rec.X - 4, rec.Y, rec.Width, rec.Height);
                            }
                            else if (rec.Intersects(reclist[x]) && x == 1)//right 
                            {
                                if (isEnemy)
                                {
                                    if (rec2.Y >= reclist[x].Y + reclist[x].Height)
                                        return new Rectangle(rec.X + 2, rec.Y + 4, rec.Width, rec.Height);
                                    else
                                        return new Rectangle(rec.X + 2, rec.Y - 4, rec.Width, rec.Height);
                                }
                                return new Rectangle(rec.X + 4, rec.Y, rec.Width, rec.Height);
                            }
                            else if (rec.Intersects(reclist[x]) && x == 2)//up 
                            {
                                if (isEnemy)
                                {
                                    if (rec2.X >= reclist[x].X + reclist[x].Width)
                                        return new Rectangle(rec.X + 4, rec.Y - 2, rec.Width, rec.Height);
                                    else
                                        return new Rectangle(rec.X - 4, rec.Y - 2, rec.Width, rec.Height);
                                }
                                return new Rectangle(rec.X, rec.Y - 4, rec.Width, rec.Height);
                            }
                            else if (rec.Intersects(reclist[x]) && x == 3)//down 
                            {
                                if (isEnemy)
                                {
                                    if (rec2.X >= reclist[x].X + reclist[x].Width)
                                        return new Rectangle(rec.X + 4, rec.Y + 2, rec.Width, rec.Height);
                                    else
                                        return new Rectangle(rec.X - 4, rec.Y + 2, rec.Width, rec.Height);
                                }
                                return new Rectangle(rec.X, rec.Y + 4, rec.Width, rec.Height);
                            }
                        }
                    }
                }
            }
            return rec;
        }

        public Rectangle hitWall(Rectangle rec)
        {
            if (Game1.time > 120)
            {
                if ((rec.X >= 775 && rec.X <= 975) && rec.Width == 128 && (rec.Y <= 20))
                    return new Rectangle(rec.X, rec.Y, 2, rec.Height);
                if ((rec.X >= 775 && rec.X <= 975) && rec.Width == 128 && (rec.Y >= 930))
                    return new Rectangle(rec.X, rec.Y, 4, rec.Height);
                if ((rec.Y >= 350 && rec.Y <= 530) && rec.Width == 128 && (rec.X <= 420))
                    return new Rectangle(rec.X, rec.Y, 3, rec.Height);
                if ((rec.Y >= 350 && rec.Y <= 530) && rec.Width == 128 && (rec.X >= 1380))
                    return new Rectangle(rec.X, rec.Y, 1, rec.Height);
            }

            if (rec.X <= 420)
                return new Rectangle(rec.X + 4, rec.Y, rec.Width, rec.Height);
            if (rec.X >= 1380)
                return new Rectangle(rec.X - 4, rec.Y, rec.Width, rec.Height);
            if (rec.Y <= 20)
                return new Rectangle(rec.X, rec.Y + 4, rec.Width, rec.Height);
            if (rec.Y >= 930)
                return new Rectangle(rec.X, rec.Y - 4, rec.Width, rec.Height);

            return rec;
        }

        public void hitWall(List<Bullet> bullet)
        {
            if (bullet.Count > 0)
                for (int i = 0; i < bullet.Count; i++)
                {
                    Rectangle rec = bullet[i].getRect();
                    if (rec.X <= 400 || rec.X >= 1500 || rec.Y <= 0 || rec.Y >= 1500)
                    {

                        bullet[i].velocity = 0;
                    }
                }
        }
        public void Update(int timer, Player player, List<Bullet> bullet, Stats stat)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].changeRec(hitWall(enemy[i].enemyDest));
                enemy[i].changeRec(intersec(enemy[i].enemyDest, player.locRec));
                enemy[i].Update(timer, player, bullet);
                int z = 0;
                if (enemy[i].Kill(bullet, stat))
                {
                    enemy.Remove(enemy[i]);
                    for (int x = 0; x < 9; x++)
                    {
                        for (int y = 0; y < 9; y++)
                        {

                            if (levelLines[x][y] == 'e')
                            {
                                if (i == z)
                                    levelLines[x] = levelLines[x].Substring(0, y) + "x" + levelLines[x].Substring(y + 1);
                                z++;
                            }
                        }
                    }
                    stat.hp += 2;
                    stat.sanity += 3;
                    Game1.deadEnemy += 1;
                }
            }
            String newFile = "";
            for (int x = 0; x < 9; x++)
            {
                newFile += levelLines[x] + "\n";
            }
            File.WriteAllText(DaFile, newFile);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Map, MapRect, Color.White);

            for (int i = 0; i < 14; i++)
            {
                for (int e = 0; e < 14; e++)
                {
                    if (Tile[i, e] != null)
                        Tile[i, e].Draw(spriteBatch);
                }
            }
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
