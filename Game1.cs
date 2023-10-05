using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.Mime;
using System;
using System.Drawing;

namespace shootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D target_Sprite;
        Texture2D crosshairs_Sprite;
        Texture2D background_Sprite;
        SpriteFont gameFont;
        const int TARGET_RADIUS = 45;
        int score = 0;
        float timer = 10f;
        bool mReleased = true;

        float mouseTargetDist;

        MouseState mState;

        Vector2 targetPosition = new Vector2(300, 300);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            target_Sprite = Content.Load<Texture2D>("target");
            crosshairs_Sprite = Content.Load<Texture2D>("crosshairs");
            background_Sprite = Content.Load<Texture2D>("sky");

            gameFont = Content.Load<SpriteFont>("GalleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (timer > 0)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            
            mState = Mouse.GetState();


            mouseTargetDist = Vector2.Distance(targetPosition, new Vector2(mState.X, mState.Y));
            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {

                if (mouseTargetDist < TARGET_RADIUS && timer > 0)
                { 
                    score++;
                    Random rand = new Random();

                    targetPosition.X = rand.Next(TARGET_RADIUS, _graphics.PreferredBackBufferWidth - TARGET_RADIUS + 1);
                    targetPosition.Y = rand.Next(TARGET_RADIUS, _graphics.PreferredBackBufferHeight - TARGET_RADIUS + 1);

                }
                mReleased = false;
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background_Sprite, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);
            if(timer> 0)
            {
                _spriteBatch.Draw(target_Sprite, new Vector2(targetPosition.X - TARGET_RADIUS, targetPosition.Y - TARGET_RADIUS), Microsoft.Xna.Framework.Color.White);
            }
            _spriteBatch.DrawString(gameFont, "Score: " + score.ToString(), new Vector2(3, 3), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(gameFont, "Time: " + Math.Ceiling(timer).ToString(), new Vector2(3, 40), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.Draw(crosshairs_Sprite, new Vector2(mState.X - 25, mState.Y - 25), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}