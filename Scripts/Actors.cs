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
    abstract class Actors
    {
        internal string name;
        internal Sprites sprite;
        internal int lifeMax;
        internal int life;
        internal float lifeRatio;
        internal float lifeDisplay;

        int recX;
        int recY;
        int recWidth;
        int recHeight;
        internal Vector2 lifeBarPos;

        InGame scene;
        bool isBot;

        internal float speedAnim;

        internal Actors(InGame pScene, bool pIsBot)
        {
            recWidth = 300;
            recHeight = 30;
            if(!pIsBot)
            {
                recX = 30;
                recY = 340;
            }
            else
            {
                recX = 870;
                recY = 60;
            }
            lifeBarPos= new Vector2(recX, recY);
            life = 1;
            lifeMax = 1;
            lifeRatio = 1;
            lifeDisplay = 1;

            scene = pScene;
            isBot = pIsBot;

            speedAnim = 0.15f;
        }

        internal virtual void Update(GameTime pGameTime)
        {

            if (isBot)
            {
                sprite.alpha = scene.alphaIA;
            }
            else
            {
                sprite.alpha = scene.alphaPlayer;
            }

            if (lifeDisplay > lifeRatio)
            {
                lifeDisplay -= 1.0f / 60.0f;
                if(lifeDisplay <= lifeRatio)
                {
                    lifeDisplay = lifeRatio;
                }
            }
            if (lifeDisplay < lifeRatio)
            {
                lifeDisplay += 1.0f / 60.0f;
                if (lifeDisplay >= lifeRatio)
                {
                    lifeDisplay = lifeRatio;
                }
            }
        }
        
        internal virtual void Draw(GameTime pGameTime)
        {
            var draw = Tools.Get<Main>()._spriteBatch;
            draw.DrawString(Tools.Fonts["pokemon18"],$"HP  :  {life} / {lifeMax}", lifeBarPos + new Vector2(10, -40), Color.White * ((!isBot) ? scene.alphaPlayer : scene.alphaIA));
            draw.Draw(Tools.DrawRec(recWidth, recHeight, Color.White), lifeBarPos, Color.White * ((!isBot) ? scene.alphaPlayer : scene.alphaIA));
            draw.Draw(Tools.DrawRec(recWidth - 4, recHeight - 4, Color.Black), lifeBarPos + new Vector2(2, 2), Color.White * ((!isBot) ? scene.alphaPlayer : scene.alphaIA));
            if (recWidth * lifeDisplay > 12)
            {
                draw.Draw(Tools.DrawRec((int)(recWidth * lifeDisplay) - 12, recHeight - 12, Color.White), lifeBarPos + new Vector2(6, 6), Color.White * ((!isBot) ? scene.alphaPlayer : scene.alphaIA));
            }
        }

        //-------------------------------

        internal virtual void Action(int pAction)
        {
            switch (pAction)
            {
                case 0:
                    Attack();
                    break;
                case 1:
                    Defend();
                    break;
                case 2:
                    SpecialSkill();
                    break;
            }
        }

        internal virtual void Attack()
        {
            sprite.PlayAnim("attack");
        }

        internal virtual void Defend()
        {
            sprite.PlayAnim("defend");
        }

        internal virtual void SpecialSkill()
        {
            sprite.PlayAnim("special");
        }
    }

    //=================================================================================

    class Damager : Actors
    {
        internal Damager(InGame pScene, bool isBot) : base(pScene, isBot)
        {
            name = "Damager";
            sprite = new Sprites();
            life = 3;
            lifeMax = life;

            if (!isBot)
            {
                sprite = new Sprites(true, 640, 640);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Damager1");
                sprite.pos = new Vector2(80, 180);
            }
            else
            {
                sprite = new Sprites(true, 512, 512);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Damager2");
                sprite.pos = new Vector2(680, 70);
            }

            sprite.alpha = 0;

            sprite.AddAnim("attack", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, speedAnim);
            sprite.AddAnim("defend", new List<byte>() { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, speedAnim);
            sprite.AddAnim("special", new List<byte>() { 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47 }, speedAnim);
            sprite.AddAnim("idle", new List<byte>() { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63 }, speedAnim, true);
            sprite.PlayAnim("idle");

            lifeRatio = life / lifeMax;
        }

        internal override void Update(GameTime pGameTime)
        {
            sprite.Update(pGameTime);
            base.Update(pGameTime);
        }

        internal override void Draw(GameTime pGameTime)
        {
            sprite.Draw(pGameTime);
            base.Draw(pGameTime);
        }

        //-------------------------------

        internal override void Action(int pAction)
        {
            base.Action(pAction);
        }

        internal override void Attack()
        {
            base.Attack();
        }

        internal override void Defend()
        {
            base.Defend();
        }

        internal override void SpecialSkill()
        {
            base.SpecialSkill();
        }
    }

    //=================================================================================

    class Healer : Actors
    {
        internal Healer(InGame pScene, bool isBot) : base(pScene, isBot)
        {
            name = "Healer";
            sprite = new Sprites();
            life = 4;
            lifeMax = life;

            if (!isBot)
            {
                sprite = new Sprites(true, 640, 640);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Healer1");
                sprite.pos = new Vector2(80, 180);
            }
            else
            {
                sprite = new Sprites(true, 512, 512);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Healer2");
                sprite.pos = new Vector2(680, 70);
            }

            sprite.alpha = 0;

            sprite.AddAnim("attack", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, speedAnim);
            sprite.AddAnim("defend", new List<byte>() { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, speedAnim);
            sprite.AddAnim("special", new List<byte>() { 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47 }, speedAnim);
            sprite.AddAnim("idle", new List<byte>() { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63 }, speedAnim, true);
            sprite.PlayAnim("idle");

            lifeRatio = life / lifeMax;
        }

        internal override void Update(GameTime pGameTime)
        {
            sprite.Update(pGameTime);
            base.Update(pGameTime);
        }

        internal override void Draw(GameTime pGameTime)
        {
            sprite.Draw(pGameTime);
            base.Draw(pGameTime);
        }

        //-------------------------------

        internal override void Action(int pAction)
        {
            base.Action(pAction);
        }

        internal override void Attack()
        {
            base.Attack();
        }

        internal override void Defend()
        {
            base.Defend();
        }

        internal override void SpecialSkill()
        {
            base.SpecialSkill();
        }
    }

    //=================================================================================

    class Tank : Actors
    {
        internal Tank(InGame pScene, bool isBot) : base(pScene, isBot)
        {
            name = "Tank";
            life = 5;
            lifeMax = life;

            if(!isBot)
            {
                sprite = new Sprites(true, 640, 640);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Tank1");
                sprite.pos = new Vector2(80, 180);
            }
            else
            {
                sprite = new Sprites(true, 512, 512);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Tank2");
                sprite.pos = new Vector2(680, 70);
            }

            sprite.alpha = 0;

            sprite.AddAnim("attack", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, speedAnim);
            sprite.AddAnim("defend", new List<byte>() { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, speedAnim);
            sprite.AddAnim("special", new List<byte>() { 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47 }, speedAnim);
            sprite.AddAnim("idle", new List<byte>() { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63 }, speedAnim, true);
            sprite.PlayAnim("idle");

            lifeRatio = life / lifeMax;
        }

        internal override void Update(GameTime pGameTime)
        {
            sprite.Update(pGameTime);
            base.Update(pGameTime);
        }

        internal override void Draw(GameTime pGameTime)
        {
            sprite.Draw(pGameTime);
            base.Draw(pGameTime);
        }

        //-------------------------------

        internal override void Action(int pAction)
        {
            base.Action(pAction);
        }

        internal override void Attack()
        {
            base.Attack();
        }

        internal override void Defend()
        {
            base.Defend();
        }

        internal override void SpecialSkill()
        {
            base.SpecialSkill();
        }
    }

    //=================================================================================

    class Archer : Actors
    {
        internal Archer(InGame pScene, bool isBot) : base(pScene, isBot)
        {
            name = "Archer";
            sprite = new Sprites();
            life = 3;
            lifeMax = life;


            if (!isBot)
            {
                sprite = new Sprites(true, 640, 640);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Archer1");
                sprite.pos = new Vector2(80, 180);
            }
            else
            {
                sprite = new Sprites(true, 512, 512);
                sprite.img = Tools.Get<Main>().Content.Load<Texture2D>("images/Archer2");
                sprite.pos = new Vector2(680, 70);
            }

            sprite.alpha = 0;

            sprite.AddAnim("attack", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, speedAnim);
            sprite.AddAnim("defend", new List<byte>() { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, speedAnim);
            sprite.AddAnim("special", new List<byte>() { 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47 }, speedAnim);
            sprite.AddAnim("idle", new List<byte>() { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63 }, speedAnim, true);
            sprite.PlayAnim("idle");

            lifeRatio = life / lifeMax;
        }

        internal override void Update(GameTime pGameTime)
        {
            sprite.Update(pGameTime);
            base.Update(pGameTime);
        }

        internal override void Draw(GameTime pGameTime)
        {
            sprite.Draw(pGameTime);
            base.Draw(pGameTime);
        }

        //-------------------------------

        internal override void Action(int pAction)
        {
            base.Action(pAction);
        }

        internal override void Attack()
        {
            base.Attack();
        }

        internal override void Defend()
        {
            base.Defend();
        }

        internal override void SpecialSkill()
        {
            base.SpecialSkill();
        }
    }
}
