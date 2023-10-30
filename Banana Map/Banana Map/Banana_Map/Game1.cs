using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework.Media;


namespace Banana_Map
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>  
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState oldkb;

        public Color playerColor = Color.White; //WORK ON DIS

        String currentLevel = "1";

        Texture2D Map, controls;
        Color newBlack = new Color(25, 25, 25);
        int screenWidth, screenHeight;

        Level level;

        Player player;
        Texture2D playerMoveTex, playerLeftTex, playerRightTex, playerIdleTex;
        public static int time;

        Texture2D enemy, ballTex;

        GameState gameState = new GameState();

        MouseState oldMouse;

        Rectangle secretRec, startRec;
        Texture2D secretTex, endTex, bossRoomTex;

        Gun gun;
        List<Bullet> bullet;
        Texture2D bulletText;
        Texture2D[] GunText;

        Random rnd;
        int qq;
        Texture2D[] startTex;

        Texture2D hpBar, Pixel, sanBar, hpTXT, corrupted;
        Stats stat;
        Texture2D[] Fakes, Insane;

        Rectangle Hallucination;

        Texture2D Dead;
        Texture2D[] Idle, Walking;
        public static int deadEnemy;

        bool win;
        Texture2D winTex;

        Boss boss;

        bool goDown = false;
        Song myClip, menu, Bossy,victory,lose;
        SoundEffect shoot,bruh,hit;
        

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            IsMouseVisible = true;
            oldMouse = Mouse.GetState();
            gameState = GameState.StartScreen;

            secretRec = new Rectangle(0, 0, screenWidth, screenHeight);
            startRec = new Rectangle(0, 0, screenWidth, screenHeight);

            GunText = new Texture2D[2];
            bullet = new List<Bullet>();

            startTex = new Texture2D[3];

            time = 0;
            deadEnemy = 0;
            rnd = new Random();
            qq = rnd.Next(0, 2);

            Fakes = new Texture2D[2];

            Idle = new Texture2D[2];
            Walking = new Texture2D[2];
            
            
            Insane = new Texture2D[3]; 

            win = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            


            secretTex = this.Content.Load<Texture2D>("bruh");
            endTex = this.Content.Load<Texture2D>("gameOver");

            startTex[0] = this.Content.Load<Texture2D>("startPng/start 1"); startTex[1] = this.Content.Load<Texture2D>("startPng/start 2"); startTex[2] = this.Content.Load<Texture2D>("startPng/start 3");
            winTex = this.Content.Load<Texture2D>("startPng/win");
            // TODO: use this.Content to load your game content here
            Map = this.Content.Load<Texture2D>("textures/Base Room");
            bossRoomTex = this.Content.Load<Texture2D>("textures/Boss Room");

            level = new Level("1", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));

            playerMoveTex = this.Content.Load<Texture2D>("textures/moveBan");
            playerLeftTex = this.Content.Load<Texture2D>("textures/leftBan");
            playerRightTex = this.Content.Load<Texture2D>("textures/rightBan");
            playerIdleTex = this.Content.Load<Texture2D>("textures/idleBan");
            Insane[1] = this.Content.Load<Texture2D>("textures/InsaneLeft");
            Insane[0] = this.Content.Load<Texture2D>("textures/idleInsane");
            Insane[2] = this.Content.Load<Texture2D>("textures/InsaneRight");
            player = new Player(playerMoveTex, playerLeftTex, playerRightTex, playerIdleTex, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 300, Insane);

            enemy = Content.Load<Texture2D>("textures/moveEn.png");
            //enemy1 = new Enemy(enemy);

            GunText[0] = this.Content.Load<Texture2D>("textures/Weapon");
            GunText[1] = this.Content.Load<Texture2D>("textures/Weapon (2)");
            gun = new Gun(GunText);
            bulletText = this.Content.Load<Texture2D>("textures/bullet");

            corrupted = this.Content.Load<Texture2D>("textures/Corrupted");
            hpBar = this.Content.Load<Texture2D>("textures/Bar");
            hpTXT = this.Content.Load<Texture2D>("textures/HpTxt");
            sanBar = this.Content.Load<Texture2D>("Sanity bar");
            Pixel = this.Content.Load<Texture2D>("textures/Pixel");
            Fakes[0] = this.Content.Load<Texture2D>("textures/HallucinationL");
            Fakes[1] = this.Content.Load<Texture2D>("textures/HallucinationR");
            controls = this.Content.Load<Texture2D>("textures/ControlsCurrent");
            stat = new Stats(hpBar, Pixel, Fakes, sanBar, hpTXT, corrupted, controls);

            Idle[0] = this.Content.Load<Texture2D>("demonIdleL");
            Idle[1] = this.Content.Load<Texture2D>("demonIdleR");

            Walking[0] = this.Content.Load<Texture2D>("demonWalkL");
            Walking[1] = this.Content.Load<Texture2D>("demonWalkR");
            Dead = this.Content.Load<Texture2D>("Dying");

            ballTex = Content.Load<Texture2D>("fireball");

            boss = new Boss(Idle, Walking, ballTex, Content.Load<Texture2D>("textures/moveEn.png"),Dead);


            shoot = this.Content.Load<SoundEffect>("Sounds/Gun");
            bruh = this.Content.Load<SoundEffect>("Sounds/Bruh");
            hit = this.Content.Load<SoundEffect>("Sounds/bruhHit");


            myClip = Content.Load<Song>("music/EerieNoise");
            menu = this.Content.Load<Song>("music/Menu");
            Bossy = this.Content.Load<Song>("music/Boss");
            victory = this.Content.Load<Song>("music/Victory");
            lose = this.Content.Load<Song>("music/Lose");
            MediaPlayer.Play(menu);


        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private String ReadFileAsStrings(string path)
        {
            String bruhString = "";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    
                    while (!reader.EndOfStream)
                    {
                        bruhString += reader.ReadLine() + "\n";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("This file could not be read: ");
                Console.WriteLine(e.Message);
            }
            return bruhString;
        }





        public void changeLevel(int n, String curr)
        {
            if (n == 1)//right
            {
                player.changeRec(new Rectangle(100, GraphicsDevice.Viewport.Height / 2 - 64, 128, 128));
                //enemy1 = new Enemy(enemy);
                if (curr == "1")
                {
                    level = new Level("1 Right", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1 Right";
                }
                else if (curr == "1 Left")
                {
                    level = new Level("1", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1";
                }
            }
            else if (n == 2)//top
            {
                player.changeRec(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 64, GraphicsDevice.Viewport.Height + 100, 128, 128));
                //enemy1 = new Enemy(enemy);
                if (curr == "1")
                {
                    level = new Level("1 Top", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1 Top";
                }
                else if (curr == "1 Bottom")
                {
                    level = new Level("1", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1";
                }
            }
            else if (n == 3)//left
            {
                player.changeRec(new Rectangle(GraphicsDevice.Viewport.Width - 100, GraphicsDevice.Viewport.Height / 2 - 64, 128, 128));
                //enemy1 = new Enemy(enemy);
                if (curr == "1")
                {
                    level = new Level("1 Left", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1 Left";
                }
                else if (curr == "1 Right")
                {
                    level = new Level("1", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1";
                }
            }
            else//bottom
            {
                player.changeRec(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 64, -100, 128, 128));
                //enemy1 = new Enemy(enemy);
                if (curr == "1")
                {
                    level = new Level("1 Bottom", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1 Bottom";
                }
                else if (curr == "1 Top")
                {
                    level = new Level("1", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                    currentLevel = "1";
                }
            }
            time = 0;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            MouseState mouse = Mouse.GetState();
            KeyboardState kb = Keyboard.GetState();
            // TODO: Add your update logic here

            if (gameState == GameState.StartScreen)
            {
                
                        if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                        {
                            player = new Player(playerMoveTex, playerLeftTex, playerRightTex, playerIdleTex, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 500, Insane);
                            
                            stat = new Stats(hpBar, Pixel, Fakes, sanBar, hpTXT, corrupted, controls);
                            level = new Level("1", Map, this.Content.Load<Texture2D>("textures/Table_H"), this.Content.Load<Texture2D>("textures/Table_V"), this.Content.Load<Texture2D>("textures/TV"), this.Content.Load<Texture2D>("textures/Bookshelf"), Content.Load<Texture2D>("textures/moveEn.png"));
                            
                            time = 0;  
                            boss = new Boss(Idle, Walking, ballTex, Content.Load<Texture2D>("textures/moveEn.png"),Dead);
                            gameState = GameState.PlayScreen;
                            win = false;
                            playerColor = Color.White;
                            goDown = false;
                            MediaPlayer.Stop();
                            MediaPlayer.Play(myClip);
                        }
                       
            }

            if (gameState == GameState.PlayScreen)
            {
                time++;
                boss.update(time,player,bullet, stat);
                player.Update(time);
                player.changeRec(level.intersec(player.locRec, new Rectangle(-100, 0, 0, 0)));

                Rectangle playerTemp = level.hitWall(player.locRec);
                player.changeRec(playerTemp);
                if (playerTemp.Width < 10)
                {
                    changeLevel(playerTemp.Width, currentLevel);
                }
                stat.update();
                if (gameTime.TotalGameTime.Seconds % 2 == 0)
                    Hallucination = stat.Hallucination(player.locRec);
                else
                    Hallucination = new Rectangle(0, 0, 0, 0);

                if (playerColor == Color.Red && time >= 160)
                    playerColor = Color.White;

                    if (stat.hp <= 0)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(lose);
                    gameState = GameState.EndScreen;
                }
                for (int i = 0; i < level.enemy.Count(); i++)
                {
                    if (level.enemy[i].Intersect(player))
                    {
                        hit.Play();
                        stat.damageTaken(5);
                        playerColor = Color.Red;
                        time = 150;

                        
                                
                                
                        
                        
                            
                    }

                }
                level.hitWall(bullet);
                level.Update(time, player, bullet,stat);

                MouseState mouseTwo = Mouse.GetState();

                Rectangle temp = player.getRec();
                Vector2 ScreenPos = new Vector2(temp.X + (temp.Width / 2), temp.Y + (temp.Height / 2) + 20);
                gun.update(mouseTwo, ScreenPos);

                if (mouseTwo.LeftButton == ButtonState.Pressed && gun.timer == 0)
                {
                    bullet.Add(new Bullet(bulletText, gun.rotationRadians, mouseTwo, ScreenPos));
                    shoot.Play();
                    gun.timer = 30;
                }

                for (int i = 0; i < bullet.Count; i++)
                {
                    if (bullet[i].velocity == 0)
                    {
                        bullet.Remove(bullet[i]);
                    }
                    else
                        bullet[i].update();
                }

                
                    
                        if (player.getRec().Y <= -350)
                            gameState = GameState.SecretScreen;
                        if (deadEnemy == 5)
                        {
                            time = 0;
                            gameState = GameState.BossScreen;
                            player = new Player(playerMoveTex, playerLeftTex, playerRightTex, playerIdleTex, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 300, Insane);
                            
                            MediaPlayer.Stop();
                            MediaPlayer.Play(Bossy);
                        }
                       
                
            }
           
            if (gameState == GameState.BossScreen)
            {
                time++;
                
                if (stat.hp <= 0)
                {
                    
                    MediaPlayer.Stop();
                    MediaPlayer.Play(lose);
                    gameState = GameState.EndScreen;
                }
                if (playerColor == Color.Red && time >= 230)
                    playerColor = Color.White;

                player.Update(time);
                if (player.getRec().X >= GraphicsDevice.Viewport.Width - 20)
                    player.changeRec(new Rectangle(player.getRec().X - 4, player.getRec().Y, player.getRec().Width, player.getRec().Height));
                else if (player.getRec().X <= 20)
                    player.changeRec(new Rectangle(player.getRec().X + 4, player.getRec().Y, player.getRec().Width, player.getRec().Height));

                if (player.getRec().Y <= 20)
                    player.changeRec(new Rectangle(player.getRec().X, player.getRec().Y + 4, player.getRec().Width, player.getRec().Height));
                else if (player.getRec().Y >= GraphicsDevice.Viewport.Height - 140)
                    player.changeRec(new Rectangle(player.getRec().X, player.getRec().Y - 4, player.getRec().Width, player.getRec().Height));

                if (bullet.Count > 0)
                    for (int i = 0; i < bullet.Count; i++)
                    {
                        Rectangle rec = bullet[i].getRect();
                        if (rec.X <= 20 || rec.X >= GraphicsDevice.Viewport.Width - 20 || rec.Y <= 20 || rec.Y >= GraphicsDevice.Viewport.Height - 20)
                        {
                            bullet[i].velocity = 0;
                        }
                    }
                stat.update();

                MouseState mouseTwo = Mouse.GetState();

                Rectangle temp = player.getRec();
                Vector2 ScreenPos = new Vector2(temp.X + (temp.Width / 2), temp.Y + (temp.Height / 2) + 20);
                gun.update(mouseTwo, ScreenPos);

                if (mouseTwo.LeftButton == ButtonState.Pressed && gun.timer == 0)
                {
                    bullet.Add(new Bullet(bulletText, gun.rotationRadians, mouseTwo, ScreenPos));
                    shoot.Play();
                    gun.timer = 20;
                    if (stat.sanity >= 80 || stat.hp <= 10)
                        gun.timer = 5;
                }

                for (int i = 0; i < bullet.Count; i++)
                {
                    Bullet current = bullet[i];
                    if (current.velocity == 0)
                    {
                        bullet.Remove(current);
                    }
                    else
                        bullet[i].update();
                    

                    if (i < bullet.Count && bullet[i].getRect().Intersects(boss.locRec) && bullet[i].getRect().X > boss.locRec.X + 300)
                    {
                        if (bullet[i].getRect().X < boss.locRec.X + 600 && bullet[i].getRect().Y > boss.locRec.Y + boss.locRec.Height / 2)
                        {
                            bullet.Remove(bullet[i]);
                            boss.health -= 0.9;
                        }
                    }

                    

                }
                
                if (boss.health > 100)
                {
                    for (int i = 0; i < boss.ballList.Length; i++)
                    {
                        if (boss.ballList[i].Intersects(player.getRec()))
                        {
                            hit.Play();
                            stat.damageTaken(3);
                            playerColor = Color.Red;
                            time = 220;
                            
                                
                                    
                                    
                        }
                    }
                }
                else if (boss.health <= 100)
                {
                    for (int z = 0; z < boss.enemy.Count; z++)
                    {
                        boss.enemy[z].Update(time, player, bullet);
                    }
                }

                Rectangle bossRec = new Rectangle(boss.locRec.X + 350, boss.locRec.Y-150, 150, boss.locRec.Height+150);

                if(goDown)
                {
                    Rectangle recZ = player.getRec();
                    player.changeRec(new Rectangle(recZ.X, recZ.Y + 20, recZ.Width, recZ.Height));
                    if (player.getRec().Y > bossRec.Y + bossRec.Height + 10)
                    {
                        goDown = false;
                    }
                }
                

                for (int i = 0; i < boss.enemy.Count(); i++)
                {
                    if (boss.enemy[i].Intersect(player))
                    {
                        hit.Play();
                        stat.damageTaken(5);
                        playerColor = Color.Red;
                        time = 150;
                       
                               
                                
                        
                    }
                }


                if (bossRec.Intersects(player.getRec()))
                {
                    goDown = true;
                }



                boss.update(time, player, bullet, stat);
                boss.attack(time,player.getRec());

                
                        
                        if(boss.health <= 0 && boss.index ==-1)
                        {
                            gameState = GameState.EndScreen;
                            win = true;
                            MediaPlayer.Stop();
                            MediaPlayer.Play(victory);

                        }

                        

            }

            if (gameState == GameState.SecretScreen|| gameState == GameState.EndScreen)
            {
                
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    gameState = GameState.StartScreen;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(menu);
                    ResetLevels();
                }
                if(kb.IsKeyDown(Keys.Space) && oldkb.IsKeyUp(Keys.Space))
                {
                    bruh.Play();
                }
                       
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    gameState = GameState.StartScreen;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(menu);
                    ResetLevels();
                }
                       
            }

            oldkb = kb;
            oldMouse = mouse;
            base.Update(gameTime);
        }

        public void ResetLevels()
        {
            File.WriteAllText(@"Content/levelText/Level 1.txt",ReadFileAsStrings(@"Content/levelText/Level 0.txt"));
            File.WriteAllText(@"Content/levelText/Level 1 Top.txt", ReadFileAsStrings(@"Content/levelText/Level 0 Top.txt"));
            File.WriteAllText(@"Content/levelText/Level 1 Right.txt", ReadFileAsStrings(@"Content/levelText/Level 0 Right.txt"));
            File.WriteAllText(@"Content/levelText/Level 1 Left.txt", ReadFileAsStrings(@"Content/levelText/Level 0 Left.txt"));
            File.WriteAllText(@"Content/levelText/Level 1 Bottom.txt", ReadFileAsStrings(@"Content/levelText/Level 0 Bottom.txt"));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(newBlack);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (gameState == GameState.StartScreen)
            {
                Texture2D bruhaps = startTex[qq];
                spriteBatch.Draw(bruhaps, startRec, Color.White);
                
            }
            else if (gameState == GameState.PlayScreen)
            {
                level.Draw(spriteBatch);
                player.Draw(spriteBatch,playerColor,stat);
                //enemy1.Draw(spriteBatch);
                gun.Draw(spriteBatch);
                if (bullet.Count > 0)
                    for (int i = 0; i < bullet.Count; i++)
                    {
                        bullet[i].draw(spriteBatch);
                    }
                stat.draw(spriteBatch);
                if (Hallucination.Width > 0)
                    stat.draw(spriteBatch, Hallucination);
                
            }
            else if (gameState == GameState.SecretScreen)
            {
                spriteBatch.Draw(secretTex, secretRec, Color.White);
            }
            else if (gameState == GameState.BossScreen)
            {
                spriteBatch.Draw(bossRoomTex, secretRec, Color.Purple);
                player.Draw(spriteBatch, playerColor,stat);
                boss.draw(spriteBatch);
                gun.Draw(spriteBatch);
                if (bullet.Count > 0)
                    for (int i = 0; i < bullet.Count; i++)
                    {
                        bullet[i].draw(spriteBatch);
                    }
                stat.draw(spriteBatch);
            }
            else if (gameState == GameState.EndScreen || stat.hp <=0)
            { 
                if(win)
                    spriteBatch.Draw(winTex, secretRec, Color.White);
                else
                    spriteBatch.Draw(endTex, secretRec, Color.White);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
