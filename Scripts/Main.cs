using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace JeuDeCombat
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        internal SpriteBatch _spriteBatch;

        GameState gameState;
        EffectsManager effsmanager;



        //  -██████╗-██████╗-███╗---██╗███████╗████████╗██████╗-██╗---██╗-██████╗████████╗-██████╗-██████╗-
        //  ██╔════╝██╔═══██╗████╗--██║██╔════╝╚══██╔══╝██╔══██╗██║---██║██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗
        //  ██║-----██║---██║██╔██╗-██║███████╗---██║---██████╔╝██║---██║██║--------██║---██║---██║██████╔╝
        //  ██║-----██║---██║██║╚██╗██║╚════██║---██║---██╔══██╗██║---██║██║--------██║---██║---██║██╔══██╗
        //  ╚██████╗╚██████╔╝██║-╚████║███████║---██║---██║--██║╚██████╔╝╚██████╗---██║---╚██████╔╝██║--██║
        //  -╚═════╝-╚═════╝-╚═╝--╚═══╝╚══════╝---╚═╝---╚═╝--╚═╝-╚═════╝--╚═════╝---╚═╝----╚═════╝-╚═╝--╚═╝
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Tools.AddService(_graphics);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Tools.AddService(this);
        }



        //  ██╗███╗---██╗██╗████████╗██╗-█████╗-██╗-----██╗███████╗███████╗
        //  ██║████╗--██║██║╚══██╔══╝██║██╔══██╗██║-----██║╚══███╔╝██╔════╝
        //  ██║██╔██╗-██║██║---██║---██║███████║██║-----██║--███╔╝-█████╗--
        //  ██║██║╚██╗██║██║---██║---██║██╔══██║██║-----██║-███╔╝--██╔══╝--
        //  ██║██║-╚████║██║---██║---██║██║--██║███████╗██║███████╗███████╗
        //  ╚═╝╚═╝--╚═══╝╚═╝---╚═╝---╚═╝╚═╝--╚═╝╚══════╝╚═╝╚══════╝╚══════╝
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Tools.Width;
            _graphics.PreferredBackBufferHeight = Tools.Height;
            _graphics.ApplyChanges();
            //--------------------


            gameState = new GameState();
            effsmanager = new EffectsManager();


            //--------------------

            base.Initialize();
        }



        //  ██╗------██████╗--█████╗-██████╗-
        //  ██║-----██╔═══██╗██╔══██╗██╔══██╗
        //  ██║-----██║---██║███████║██║--██║
        //  ██║-----██║---██║██╔══██║██║--██║
        //  ███████╗╚██████╔╝██║--██║██████╔╝
        //  ╚══════╝-╚═════╝-╚═╝--╚═╝╚═════╝-
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //--------------------

            Tools.Fonts.Add("pokemon18", Content.Load<SpriteFont>("pokemon18"));
            Tools.Fonts.Add("SketchGothicSchool120", Content.Load<SpriteFont>("SketchGothicSchool120"));

            //--------------------

            MediaPlayer.IsRepeating = true;

            gameState.current = new Start();

        }



        //  ██╗---██╗██████╗-██████╗--█████╗-████████╗███████╗
        //  ██║---██║██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔════╝
        //  ██║---██║██████╔╝██║--██║███████║---██║---█████╗--
        //  ██║---██║██╔═══╝-██║--██║██╔══██║---██║---██╔══╝--
        //  ╚██████╔╝██║-----██████╔╝██║--██║---██║---███████╗
        //  -╚═════╝-╚═╝-----╚═════╝-╚═╝--╚═╝---╚═╝---╚══════╝
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Tools.update();
            //--------------------


            gameState.current.Update(gameTime);


            //--------------------
            Tools.prv_update();

            base.Update(gameTime);
        }



        //  ██████╗-██████╗--█████╗-██╗----██╗
        //  ██╔══██╗██╔══██╗██╔══██╗██║----██║
        //  ██║--██║██████╔╝███████║██║-█╗-██║
        //  ██║--██║██╔══██╗██╔══██║██║███╗██║
        //  ██████╔╝██║--██║██║--██║╚███╔███╔╝
        //  ╚═════╝-╚═╝--╚═╝╚═╝--╚═╝-╚══╝╚══╝-  
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Tools.BackGroundColor);

            _spriteBatch.Begin(SpriteSortMode.Deferred);
            //--------------------

            gameState.current.Draw(gameTime);

            //--------------------
            //DebugDisplay(gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }




        //  ██████╗-███████╗██████╗-██╗---██╗-██████╗-
        //  ██╔══██╗██╔════╝██╔══██╗██║---██║██╔════╝-
        //  ██║--██║█████╗--██████╔╝██║---██║██║--███╗
        //  ██║--██║██╔══╝--██╔══██╗██║---██║██║---██║
        //  ██████╔╝███████╗██████╔╝╚██████╔╝╚██████╔╝
        //  ╚═════╝-╚══════╝╚═════╝--╚═════╝--╚═════╝-
        protected void DebugDisplay(GameTime gameTime)
        {
            

        }


    }
}