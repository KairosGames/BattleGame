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
    static public class Tools
    {
        internal static Random random { get; set; } = new Random();

        internal static int Width { get; set; } = 1200; //500
        internal static int Height { get; set; } = 800; //800
        internal static Color BackGroundColor { get; set; } = Color.Black;
        internal static Color DefaultColor { get; set; } = Color.White;

        internal static MouseState mouse { get; set; }
        internal static MouseState prv_mouse { get; set; }

        internal static KeyboardState keyboard { get; set; }
        internal static KeyboardState prv_keyboard { get; set; }

        internal static GamePadCapabilities gmpd1caps { get; set; }
        internal static GamePadState gamepad1 { get; set; }
        internal static GamePadState prv_gamepad1 { get; set; }

        internal static SpriteFont DefaultFont { get; set; }
        internal static Dictionary<string, SpriteFont> Fonts { get; set; } = new Dictionary<string, SpriteFont>();


        //------------------------------------------------------------


        internal static double DistMeasure(int x1, int y1, int x2, int y2)
        {
            return Math.Pow(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2), 0.5);
        }

        internal static double AngleMeasure(int x1, int y1, int x2, int y2)
        {
            return Math.Atan2(y2 - y1, x2 - x1);
        }

        internal static int Rnd(int pMin, int pMax)
        {
            return random.Next(pMin, pMax + 1);
        }

        internal static Texture2D DrawRec(int pW, int pH, Color pColor)
        {
            var lclrec = new Texture2D(Get<GraphicsDeviceManager>().GraphicsDevice, pW, pH);
            Color[] data = new Color[pW * pH];
            for (int i = 0; i < data.Length; ++i) data[i] = pColor;
            lclrec.SetData(data);
            return lclrec;
        }


        //------------------------------------------------------------


        internal static void update()
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
            gmpd1caps = GamePad.GetCapabilities(PlayerIndex.One);
            if (gmpd1caps.IsConnected)
                gamepad1 = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);
        }


        //------------------------------------------------------------


        internal static void prv_update()
        {
            prv_mouse = Mouse.GetState();
            prv_keyboard = Keyboard.GetState();
            if (gmpd1caps.IsConnected)
                prv_gamepad1 = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);
        }


        //------------------------------------------------------------


        internal static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        internal static void AddService<T>(T pRef)
        {
            Services.Add(typeof(T), pRef);
        }

        internal static T Get<T>()
        {
            return (T)Services[typeof(T)];
        }


    }
}
