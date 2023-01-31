using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuDeCombat
{
    //  ███████╗███████╗███████╗███╗---███╗-█████╗-███╗---██╗-█████╗--██████╗-███████╗██████╗-
    //  ██╔════╝██╔════╝██╔════╝████╗-████║██╔══██╗████╗--██║██╔══██╗██╔════╝-██╔════╝██╔══██╗
    //  █████╗--█████╗--█████╗--██╔████╔██║███████║██╔██╗-██║███████║██║--███╗█████╗--██████╔╝
    //  ██╔══╝--██╔══╝--██╔══╝--██║╚██╔╝██║██╔══██║██║╚██╗██║██╔══██║██║---██║██╔══╝--██╔══██╗
    //  ███████╗██║-----██║-----██║-╚═╝-██║██║--██║██║-╚████║██║--██║╚██████╔╝███████╗██║--██║
    //  ╚══════╝╚═╝-----╚═╝-----╚═╝-----╚═╝╚═╝--╚═╝╚═╝--╚═══╝╚═╝--╚═╝-╚═════╝-╚══════╝╚═╝--╚═╝
    class EffectsManager
    {
        internal Song sngMenu;
        internal Song sngFight;

        internal SoundEffect sndVictory;
        internal SoundEffect sndGameOver;
        internal SoundEffect sndEquality;

        internal SoundEffect sndSelect;
        internal SoundEffect sndSelected;
        internal SoundEffect sndFight;
        internal SoundEffect sndDeath;
        internal SoundEffect sndSimulation;


        //------------------------------------------------------------


        internal EffectsManager()
        {

            Tools.AddService(this);

            sngMenu = Tools.Get<Main>().Content.Load<Song>("sounds/MainThemeMenu");
            sngFight = Tools.Get<Main>().Content.Load<Song>("sounds/MainThemeFight");

            sndVictory = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/Victory");
            sndGameOver = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/GameOver");
            sndEquality = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/Equality");

            sndSelect = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/SelectSound");
            sndSelected = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/Selected");
            sndFight = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/FightSound");
            sndDeath = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/Death");
            sndSimulation = Tools.Get<Main>().Content.Load<SoundEffect>("sounds/Simulation");

        }

        //------------------------------------------------------------

        internal void Update(GameTime pGameTime)
        {

            

        }


        //------------------------------------------------------------


        internal void Draw(GameTime pGameTime)
        {



        }


    }

}
