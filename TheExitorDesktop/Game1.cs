#region Using Statements

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace TheExitorDesktop
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public BetterSpriteBatch spriteBatch;

      //  public GameScreen ScreenMenu { get; private set; }
      //  public GameScreen ScreenLevel { get; private set; }
        public GameScreenManager ScreenManager { get; private set; }
       Menu compMenu;
       GameWorld compGameWorld;

        public Wave WaveAlgorithm { get; private set; }
        

        public SpriteFont FontHalo { get; private set; }
        public SpriteFont FontStencilSmall { get; private set; }
        public SpriteFont FontStencilMiddle { get; private set; }
        public SpriteFont FontStencilBig { get; private set; }

        public DisplayMode Display { get; private set; }
        public WorldCamera Camera { get; set; }

        public Dictionary<string, Texture2D> Textures { get; private set; }
        public Dictionary<string, SoundEffect> SoundEffects { get; private set; }
        public Dictionary<string, SpriteFont> Fonts { get; private set; }
      
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           
            WaveAlgorithm = new Wave();
          
            IsMouseVisible = false;
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
            graphics.IsFullScreen = true;
            
            Display = this.GraphicsDevice.DisplayMode;
            Camera = new WorldCamera(this.GraphicsDevice.Viewport);

          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new BetterSpriteBatch(GraphicsDevice);

            Fonts = new Dictionary<string, SpriteFont>();
            Fonts.Add("Stencil10", Content.Load<SpriteFont>("Font/Stencil10"));

            FontHalo = Content.Load<SpriteFont>("Font/Halo");
            FontStencilSmall = Content.Load<SpriteFont>("Font/Stencil25");
            FontStencilMiddle = Content.Load<SpriteFont>("Font/Stencil35");
            FontStencilBig = Content.Load<SpriteFont>("Font/Stencil50");

            Textures = new Dictionary<string, Texture2D>();
            Textures.Add("Player", Content.Load<Texture2D>("player"));
            Textures.Add("Bullet", Content.Load<Texture2D>("Objects/bullet"));
            Textures.Add("StoneDarkFloor", Content.Load<Texture2D>("Objects/stoneDarkFloor"));
            Textures.Add("StoneWall", Content.Load<Texture2D>("Objects/StoneWall"));
            Textures.Add("MenuBackground", Content.Load<Texture2D>("Background/menu"));
            Textures.Add("PopUpBackground", Content.Load<Texture2D>("Background/popup"));          
            Textures.Add("MouseCursor", Content.Load<Texture2D>("cursor"));
            Textures.Add("RedDiamond", Content.Load<Texture2D>("Items/redDiamond"));
            Textures.Add("BlueDiamond", Content.Load<Texture2D>("Items/blueDiamond"));
            Textures.Add("Debug1", Content.Load<Texture2D>("tile1"));
            Textures.Add("Debug2", Content.Load<Texture2D>("tile2"));

            compMenu = new Menu(this);
            compGameWorld = new GameWorld(this);

            compMenu.Initialize();
            compGameWorld.Initialize();

            ScreenManager = new GameScreenManager(this.Components);
            ScreenManager.AddScreen(new GameScreen(this, "MainMenu", compMenu));
            ScreenManager.AddScreen(new GameScreen(this, "World", compGameWorld));

            ScreenManager.ChangeScreen("MainMenu");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            UserInput.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(Textures["MouseCursor"],UserInput.MouseVector,Color.White);
            spriteBatch.End();           
        }

        public void NewGame()
        {
            compGameWorld.Reset();
        }

       
        
    }
}
