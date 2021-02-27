using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace HackGame
{
    public class Game1 : Game
    {
        Texture2D terminalTexture { get; set; }
        SpriteFont shellFont { get; set; }

        public static GameWindow gw;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<string> lines = new List<string>
        {
            "root@localhost:/home/ # "
        };


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1115;
            _graphics.PreferredBackBufferHeight = 624;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            gw = Window;
            gw.TextInput += OnInput;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            terminalTexture = Content.Load<Texture2D>("Terminal");
            shellFont = Content.Load<SpriteFont>("Shell");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void OnInput(object sender, TextInputEventArgs e)
        {
            if (e.Key == Keys.Enter)
            {
                lines.Add("root@localhost:/home/ # ");
                return;
            }

            if (e.Key == Keys.Back)
            {
                if(lines[lines.Count - 1].Length != "root@localhost:/home/ # ".Length)
                    lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 1);
                return;
            }

            else lines[lines.Count - 1] += e.Character;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(terminalTexture, new Vector2(0, 0), Color.White);
            
            int y = 100;

            foreach (var x in lines)
            {
                _spriteBatch.DrawString(shellFont, x, new Vector2(100, y), Color.WhiteSmoke);
                y += 20;
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
