using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuDeCombat
{
    //  ███████╗██████╗-██████╗-██╗████████╗███████╗███████╗
    //  ██╔════╝██╔══██╗██╔══██╗██║╚══██╔══╝██╔════╝██╔════╝
    //  ███████╗██████╔╝██████╔╝██║---██║---█████╗--███████╗
    //  ╚════██║██╔═══╝-██╔══██╗██║---██║---██╔══╝--╚════██║
    //  ███████║██║-----██║--██║██║---██║---███████╗███████║
    //  ╚══════╝╚═╝-----╚═╝--╚═╝╚═╝---╚═╝---╚══════╝╚══════╝
    class Sprites
    {
        internal Texture2D img { get; set; }
        internal Color color;
        internal float alpha { get; set; } = 1.0f;
        internal bool ts { get; set; }

        internal Rectangle frmrec;
        internal int w { get; set; }
        internal int h { get; set; }
        internal Dictionary<string, Animations> anims { get; set; }
        internal Animations current { get; set; } = null;
        internal bool loop { get; set; }
        internal byte step { get; set; }
        internal byte frm { get; set; }
        internal double frmtimer { get; set; }


        internal Vector2 pos = Vector2.Zero;
        internal Vector2 orgn { get; set; } = Vector2.Zero;
        internal float rot { get; set; } = 0;
        internal Vector2 scale { get; set; } = new Vector2(1, 1);
        internal SpriteEffects sprteff { get; set; } = SpriteEffects.None;
        internal float layer { get; set; }

        internal Vector2 speed;
        internal Vector2 power;
        internal Vector2 weight;
        internal Vector2 friction;

        internal Rectangle hitboxe;
        internal Rectangle nxtXhitboxe;
        internal Rectangle nxtYhitboxe;
        //internal Rectangle hitboxe2;

        internal sbyte life { get; set; }
        internal byte kind { get; set; }

        internal bool shadow { get; set; }
        internal Vector2 shdwlocation;
        internal float shdwalpha;
        internal Color shdwcolor;


        //------------------------------------------------------------


        internal Sprites(bool pTS = false, int pW = 0, int pH = 0)
        {
            color = Tools.DefaultColor;
            layer = 0.5f;
            ts = pTS;
            if (pTS)
            {
                w = pW;
                h = pH;
                frmrec = new Rectangle(0, 0, pW, pH);
                anims = new Dictionary<string, Animations>();
            }

            shadow = false;
            shdwlocation = new Vector2(15.0f, 15.0f);
            shdwalpha = 0.4f;
            shdwcolor = Color.Black;
        }


        //------------------------------------------------------------


        internal void AddAnim(string pName, List<byte> pFrms, float pDt, bool pLoop = false, bool pPersist = false)
        {
            var anim = new Animations(pFrms, pDt, pLoop, pPersist);
            anims.Add(pName, anim);
        }

        internal void PlayAnim(string pName)
        {
            if (current == anims[pName])
            {
                return;
            }
            current = anims[pName];
            step = 0;
            frm = current.frms[step];
        }


        //------------------------------------------------------------


        internal virtual void Update(GameTime pGameTime)
        {

            if (current != null && !current.fnshed)
            {

                var lclx = frm - ((byte)((frm * w) / img.Width)) * (img.Width / w);
                var lcly = (byte)((frm * w) / img.Width);
                frmrec.X = lclx * w;
                frmrec.Y = lcly * h;

                frmtimer += (1D / 60D);
                if (frmtimer >= current.dt)
                {
                    step++;
                    frmtimer = 0;
                    if (step >= current.frms.Count)
                    {
                        if (current.loop)
                        {
                            step = 0;
                            frm = current.frms[step];
                        }
                        else
                        {
                            step = 0;
                            if (current.persist)
                            {
                                frm = current.frms[current.frms.Count - 1];
                                current.fnshed = true;
                            }
                            else
                            {
                                current = null;
                            }
                        }
                    }
                    else
                    {
                        frm = current.frms[step];
                        frmtimer = 0;
                    }
                }
            }

        }


        //------------------------------------------------------------


        internal virtual void Draw(GameTime pGameTime)
        {
            if (!ts)
            {
                if (shadow)
                    Tools.Get<Main>()._spriteBatch.Draw(img, pos + shdwlocation, null, shdwcolor * shdwalpha, rot, orgn, scale, sprteff, layer - 0.01f);

                Tools.Get<Main>()._spriteBatch.Draw(img, pos, null, color * alpha, rot, orgn, scale, sprteff, layer);
            }
            else
            {
                if (shadow)
                    Tools.Get<Main>()._spriteBatch.Draw(img, pos + shdwlocation, frmrec, shdwcolor * shdwalpha, rot, orgn, scale, sprteff, layer - 0.01f);

                Tools.Get<Main>()._spriteBatch.Draw(img, pos, frmrec, color * alpha, rot, orgn, scale, sprteff, layer);
            }
        }
    }




    //  -█████╗-███╗---██╗██╗███╗---███╗-█████╗-████████╗██╗-██████╗-███╗---██╗███████╗
    //  ██╔══██╗████╗--██║██║████╗-████║██╔══██╗╚══██╔══╝██║██╔═══██╗████╗--██║██╔════╝
    //  ███████║██╔██╗-██║██║██╔████╔██║███████║---██║---██║██║---██║██╔██╗-██║███████╗
    //  ██╔══██║██║╚██╗██║██║██║╚██╔╝██║██╔══██║---██║---██║██║---██║██║╚██╗██║╚════██║
    //  ██║--██║██║-╚████║██║██║-╚═╝-██║██║--██║---██║---██║╚██████╔╝██║-╚████║███████║
    //  ╚═╝--╚═╝╚═╝--╚═══╝╚═╝╚═╝-----╚═╝╚═╝--╚═╝---╚═╝---╚═╝-╚═════╝-╚═╝--╚═══╝╚══════╝
    class Animations
    {
        internal List<byte> frms { get; set; }
        internal float dt { get; set; }
        internal bool loop { get; set; }
        internal bool persist { get; set; }
        internal bool fnshed { get; set; }

        internal Animations(List<byte> pFrms, float pDt, bool pLoop, bool pPersist)
        {
            frms = pFrms;
            dt = pDt;
            loop = pLoop;
            if (pLoop)
            {
                persist = false;
            }
            else
            {
                persist = pPersist;
            }
            fnshed = false;
        }
    }
}
