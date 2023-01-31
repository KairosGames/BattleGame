using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using static System.Formats.Asn1.AsnWriter;
using System.Threading;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using System.Transactions;
using System.Diagnostics.Metrics;
using static JeuDeCombat.TurnManager;

namespace JeuDeCombat
{
    class GameState
    {
        //  -██████╗--█████╗-███╗---███╗███████╗███████╗████████╗-█████╗-████████╗███████╗
        //  ██╔════╝-██╔══██╗████╗-████║██╔════╝██╔════╝╚══██╔══╝██╔══██╗╚══██╔══╝██╔════╝
        //  ██║--███╗███████║██╔████╔██║█████╗--███████╗---██║---███████║---██║---█████╗--
        //  ██║---██║██╔══██║██║╚██╔╝██║██╔══╝--╚════██║---██║---██╔══██║---██║---██╔══╝--
        //  ╚██████╔╝██║--██║██║-╚═╝-██║███████╗███████║---██║---██║--██║---██║---███████╗
        //  -╚═════╝-╚═╝--╚═╝╚═╝-----╚═╝╚══════╝╚══════╝---╚═╝---╚═╝--╚═╝---╚═╝---╚══════╝
        internal Scenes current { get; set; }


        //------------------------------------------------------------


        internal GameState()
        {
            Tools.AddService(this);
        }


        //------------------------------------------------------------


        internal void SceneSwitch(Scenes pScene)
        {
            current = null;
            current = pScene;
        }





    }




    //  ███████╗-██████╗███████╗███╗---██╗███████╗███████╗
    //  ██╔════╝██╔════╝██╔════╝████╗--██║██╔════╝██╔════╝
    //  ███████╗██║-----█████╗--██╔██╗-██║█████╗--███████╗
    //  ╚════██║██║-----██╔══╝--██║╚██╗██║██╔══╝--╚════██║
    //  ███████║╚██████╗███████╗██║-╚████║███████╗███████║
    //  ╚══════╝-╚═════╝╚══════╝╚═╝--╚═══╝╚══════╝╚══════╝
    abstract class Scenes
    {
        protected float timer;
        internal float deltaTime;
        internal bool isChoiceOn;
        internal bool isChoiceDone;


        //------------------------------------------------------------


        internal Scenes()
        {
            timer = 0;
            deltaTime = 1.0f / 60.0f;
            isChoiceOn = false;
            isChoiceDone = false;
        }


        //------------------------------------------------------------


        internal virtual void Update(GameTime pGameTime)
        {
            Tools.Get<EffectsManager>().Update(pGameTime);
        }


        //------------------------------------------------------------


        internal virtual void Draw(GameTime pGameTime)
        {
            Tools.Get<EffectsManager>().Draw(pGameTime);
        }
    }




    //  ███████╗████████╗ █████╗ ██████╗ ████████╗
    //  ██╔════╝╚══██╔══╝██╔══██╗██╔══██╗╚══██╔══╝
    //  ███████╗   ██║   ███████║██████╔╝   ██║   
    //  ╚════██║   ██║   ██╔══██║██╔══██╗   ██║   
    //  ███████║   ██║   ██║  ██║██║  ██║   ██║   
    //  ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   
    class Start : Scenes
    {
        // Title
        string title;
        float xPosTitle;
        float yPos1Title;
        float yPos2Title;
        float titleSpeed;
        Vector2 titleDims;
        Vector2 titlePos;

        // PressEnter
        string pressEnter;
        float xPos1PressEnter;
        float xPos2PressEnter;
        float yPosPressEnter;
        float pressEnterSpeed;
        Vector2 presEnterDims;
        Vector2 pressEnterPos;

        //Steps
        List<float> steps;
        float timerPress;


        //------------------------------------------------------------


        internal Start() : base()
        {
            // Title
            title = "Battle Game";
            titleDims = Tools.Fonts["SketchGothicSchool120"].MeasureString(title);
            xPosTitle = (Tools.Width - titleDims.X) / 2;
            yPos1Title = -200;
            yPos2Title = (Tools.Height / 2) - titleDims.Y;
            titlePos = new Vector2(xPosTitle, yPos1Title);

            // PressEnter
            pressEnter = "Press Enter";
            presEnterDims = Tools.Fonts["pokemon18"].MeasureString(pressEnter);
            xPos1PressEnter = 1300;
            xPos2PressEnter = (Tools.Width - presEnterDims.X) / 2;
            yPosPressEnter = (Tools.Height / 2) + titleDims.Y;
            pressEnterPos = new Vector2(xPos1PressEnter, yPosPressEnter);

            //Steps1
            steps = new List<float>() { -10, ((yPos2Title + 10) / 2) - 10, yPos2Title };
            titleSpeed = 1.5f;
            pressEnterSpeed = 30.0f;
            timerPress = 1.0f;

            MediaPlayer.Play(Tools.Get<EffectsManager>().sngMenu);
            MediaPlayer.Volume = 0.5f;

        }


        //------------------------------------------------------------


        internal override void Update(GameTime pGameTime)
        {
            // Title & PressEnter mooves
            if (!isChoiceOn)
            {
                timer += deltaTime;
                if (timer >= 1 && titlePos.Y<= steps[0])
                {
                    titlePos.Y += titleSpeed;
                }
                if(timer >= 4 && titlePos.Y <= steps[1])
                {
                    titlePos.Y += titleSpeed;
                }
                if (timer >= 6 && titlePos.Y <= steps[2])
                {
                    titlePos.Y += titleSpeed;
                }
                if(timer>=8 && pressEnterPos.X>= xPos2PressEnter)
                {
                    pressEnterPos.X -= pressEnterSpeed;
                }
                if (pressEnterPos.X <= xPos2PressEnter)
                {
                    isChoiceOn = true;
                    titlePos.Y = steps[2];
                    pressEnterPos.X = xPos2PressEnter;
                    timer = 0;
                }
            }

            if (isChoiceOn && !isChoiceDone)
            {
                if (Tools.prv_keyboard.IsKeyUp(Keys.Enter) && Tools.keyboard.IsKeyDown(Keys.Enter))
                {
                    isChoiceDone = true;
                    Tools.Get<EffectsManager>().sndSelected.Play();
                }
            }

            if (isChoiceDone)
            {
                titlePos.X -= pressEnterSpeed*2;
                pressEnterPos.X -= pressEnterSpeed*2;

                if(titlePos.X <= -titleDims.X)
                {
                    Tools.Get<GameState>().SceneSwitch(new Menu());
                }
                if (MediaPlayer.Volume > 0)
                {
                    MediaPlayer.Volume -= deltaTime * 2;
                    if (MediaPlayer.Volume <= 0) { MediaPlayer.Volume = 0; }
                }
            }

            if (isChoiceOn) { timerPress -= deltaTime; }
            if (timerPress <= 0)
            {
                timerPress = 2.0f;
            }
            

            base.Update(pGameTime);
        }


        //------------------------------------------------------------


        internal override void Draw(GameTime pGameTime)
        {
            var draw = Tools.Get<Main>()._spriteBatch;
            draw.DrawString(Tools.Fonts["SketchGothicSchool120"], title, titlePos, Color.White);
            if(timerPress >= 0.5f)
            {
                draw.DrawString(Tools.Fonts["pokemon18"], pressEnter, pressEnterPos, Color.White);
            }


            base.Draw(pGameTime);
        }
    }




    //  ███╗---███╗███████╗███╗---██╗██╗---██╗
    //  ████╗-████║██╔════╝████╗--██║██║---██║
    //  ██╔████╔██║█████╗--██╔██╗-██║██║---██║
    //  ██║╚██╔╝██║██╔══╝--██║╚██╗██║██║---██║
    //  ██║-╚═╝-██║███████╗██║-╚████║╚██████╔╝
    //  ╚═╝-----╚═╝╚══════╝╚═╝--╚═══╝-╚═════╝-
    class Menu : Scenes
    {
        List<string> lst;
        Panels startPanel;
        Vector2 panelPos1;
        Vector2 panelPos2;
        float speed;
        int nChoice;


        //------------------------------------------------------------


        internal Menu() : base()
        {
            lst = new List<string>() { "Fight", "Simulation", "Exit" };
            panelPos1 = new Vector2(1300, 100);
            panelPos2 = new Vector2(200, 100);
            startPanel = new Panels(panelPos1, lst, this, 30);
            speed = 30.0f;
            nChoice = 0;
        }


        //------------------------------------------------------------


        internal override void Update(GameTime pGameTime)
        {
            Vector2 panelPos = startPanel.canvasPos + startPanel.moove;

            if (!isChoiceOn)
            {
                timer += deltaTime;

                if (timer >= 0.5 && panelPos.X >= panelPos2.X)
                {
                    startPanel.moove.X -= speed;
                }
                if(panelPos.X <= panelPos2.X)
                {
                    isChoiceOn = true;
                    timer = 0;
                    startPanel.moove.X = -(panelPos1.X - panelPos2.X);
                }
            }

            if(isChoiceOn && !isChoiceDone && Tools.prv_keyboard.IsKeyUp(Keys.Enter) && Tools.keyboard.IsKeyDown(Keys.Enter))
            {
                nChoice = startPanel.cursorLane;
                isChoiceDone = true;
                Tools.Get<EffectsManager>().sndSelected.Play();
            }

            if (isChoiceDone)
            {
                timer += deltaTime;

                if(timer >= 0.2)
                {
                    startPanel.moove.X -= speed;
                }
                if(startPanel.moove.X <= -1900)
                {
                    switch (nChoice)
                    {
                        case 0:
                            timer = 0;
                            Tools.Get<GameState>().SceneSwitch(new DifficultyChoice());
                            break;
                        case 1:
                            timer = 0;
                            Tools.Get<GameState>().SceneSwitch(new DifficultyChoice(true));
                            break;
                        case 2:
                            timer = 0;
                            Tools.Get<Main>().Exit();
                            break;
                    }
                }
            }


            startPanel.Update(pGameTime);

            base.Update(pGameTime);
        }


        //------------------------------------------------------------


        internal override void Draw(GameTime pGameTime)
        {

            startPanel.Draw(pGameTime);

            base.Draw(pGameTime);
        }
    }




    //   ██████╗██╗  ██╗ ██████╗ ██╗ ██████╗███████╗
    //  ██╔════╝██║  ██║██╔═══██╗██║██╔════╝██╔════╝
    //  ██║     ███████║██║   ██║██║██║     █████╗  
    //  ██║     ██╔══██║██║   ██║██║██║     ██╔══╝  
    //  ╚██████╗██║  ██║╚██████╔╝██║╚██████╗███████╗
    //   ╚═════╝╚═╝  ╚═╝ ╚═════╝ ╚═╝ ╚═════╝╚══════╝
    class DifficultyChoice : Scenes
    {
        string question;
        string stringToPrint;
        Vector2 questionPos;
        int counter;

        List<string> lst;
        Panels difficultyPanel;
        Vector2 panelPos1;
        Vector2 panelPos2;
        float speed;
        int nChoice;
        bool isQuestionFinished;

        bool isSimulation;

        //------------------------------------------------------------


        internal DifficultyChoice(bool pIsSimulation = false) : base()
        {
            question = "Which  difficulty  do  you  want  ?";
            stringToPrint = "";
            questionPos = new Vector2
                (
                    (Tools.Width / 2) - (Tools.Fonts["pokemon18"].MeasureString(stringToPrint).X)/2,
                    (Tools.Height/2) - (Tools.Fonts["pokemon18"].MeasureString(stringToPrint).Y)/2 - 200
                );
            timer = 0.2f;
            counter = -1;

            lst = new List<string>() { "Easy", "Hard" };
            panelPos1 = new Vector2(1300, (Tools.Height / 2) - 100);
            panelPos2 = new Vector2((Tools.Width / 2) - 150, (Tools.Height / 2) - 100);
            difficultyPanel = new Panels(panelPos1, lst, this, 30);
            speed = 30.0f;
            nChoice = 0;
            isQuestionFinished = false;

            isSimulation = pIsSimulation;
        }


        //------------------------------------------------------------


        internal override void Update(GameTime pGameTime)
        {
            Vector2 panelPos = difficultyPanel.canvasPos + difficultyPanel.moove;

            if (!isQuestionFinished)
            {
                questionPos.X = (Tools.Width / 2) - (Tools.Fonts["pokemon18"].MeasureString(stringToPrint).X)/2;
                timer -= deltaTime;

                if(timer <= 0)
                {
                    counter++;
                    timer = Tools.random.Next(1, 11)/100;
                    if(counter < question.Length)
                    {
                        stringToPrint += question[counter];
                    }
                    if(counter >= question.Length)
                    {
                        timer = 0;
                        isQuestionFinished = true;
                    }
                }
                
            }

            if (!isChoiceOn && isQuestionFinished)
            {
                timer += deltaTime;

                if (timer >= 0.3 && panelPos.X >= panelPos2.X)
                {
                    difficultyPanel.moove.X -= speed;
                }
                if (panelPos.X <= panelPos2.X)
                {
                    isChoiceOn = true;
                    timer = 0;
                    difficultyPanel.moove.X = -(panelPos1.X - panelPos2.X);
                }
            }

            if (isChoiceOn && !isChoiceDone && Tools.prv_keyboard.IsKeyUp(Keys.Enter) && Tools.keyboard.IsKeyDown(Keys.Enter))
            {
                nChoice = difficultyPanel.cursorLane;
                isChoiceDone = true;
                Tools.Get<EffectsManager>().sndSelected.Play();
            }

            if (isChoiceDone)
            {
                timer += deltaTime;

                if (timer >= 0.2)
                {
                    difficultyPanel.moove.X -= speed;
                }
                if (difficultyPanel.moove.X <= -1900)
                {
                    timer = 0;
                    if (!isSimulation)
                    {
                        Tools.Get<GameState>().SceneSwitch(new CharacterChoice(nChoice));
                    }
                    else
                    {
                        Tools.Get<GameState>().SceneSwitch(new Simulation(nChoice));
                    }
                }
            }


            difficultyPanel.Update(pGameTime);

            base.Update(pGameTime);
        }


        //------------------------------------------------------------


        internal override void Draw(GameTime pGameTime)
        {

            var draw = Tools.Get<Main>()._spriteBatch;
            draw.DrawString(Tools.Fonts["pokemon18"], stringToPrint, questionPos, Color.White);

            difficultyPanel.Draw(pGameTime);

            base.Draw(pGameTime);
        }


    }



    //  ============================================
    //  ============================================
    //  ============================================
    //  ============================================
    //  ============================================
    //  ============================================



    class CharacterChoice : Scenes
    {
        string question;
        string stringToPrint;
        Vector2 questionPos;
        int counter;

        List<string> lst;
        Panels charactersPanel;
        Vector2 panelPos1;
        Vector2 panelPos2;
        float speed;
        int nChoice;
        bool isQuestionFinished;

        int difficultyChoice;


        //------------------------------------------------------------


        internal CharacterChoice(int pDifficulty) : base()
        {
            difficultyChoice = pDifficulty;

            question = "Choose  your  character  :  ";
            stringToPrint = "";
            questionPos = new Vector2
                (
                    (Tools.Width / 2) - (Tools.Fonts["pokemon18"].MeasureString(stringToPrint).X) / 2,
                    (Tools.Height / 2) - (Tools.Fonts["pokemon18"].MeasureString(stringToPrint).Y) / 2 - 200
                );
            timer = 0.2f;
            counter = -1;

            lst = new List<string>() { "Damager", "Healer", "Tank", "Archer" };
            panelPos1 = new Vector2(1300, (Tools.Height / 2) - 100);
            panelPos2 = new Vector2((Tools.Width / 2) - 150, (Tools.Height / 2) - 100);
            charactersPanel = new Panels(panelPos1, lst, this, 30);
            speed = 30.0f;
            nChoice = 0;
            isQuestionFinished = false;

        }


        //------------------------------------------------------------


        internal override void Update(GameTime pGameTime)
        {
            Vector2 panelPos = charactersPanel.canvasPos + charactersPanel.moove;

            if (!isQuestionFinished)
            {
                questionPos.X = (Tools.Width / 2) - (Tools.Fonts["pokemon18"].MeasureString(stringToPrint).X) / 2;
                timer -= deltaTime;

                if (timer <= 0)
                {
                    counter++;
                    timer = Tools.random.Next(1, 11) / 100;
                    if (counter < question.Length)
                    {
                        stringToPrint += question[counter];
                    }
                    if (counter >= question.Length)
                    {
                        timer = 0;
                        isQuestionFinished = true;
                    }
                }

            }

            if (!isChoiceOn && isQuestionFinished)
            {
                timer += deltaTime;

                if (timer >= 0.3 && panelPos.X >= panelPos2.X)
                {
                    charactersPanel.moove.X -= speed;
                }
                if (panelPos.X <= panelPos2.X)
                {
                    isChoiceOn = true;
                    timer = 0;
                    charactersPanel.moove.X = -(panelPos1.X - panelPos2.X);
                }
            }

            if (isChoiceOn && !isChoiceDone && Tools.prv_keyboard.IsKeyUp(Keys.Enter) && Tools.keyboard.IsKeyDown(Keys.Enter))
            {
                nChoice = charactersPanel.cursorLane;
                isChoiceDone = true;
                Tools.Get<EffectsManager>().sndSelected.Play();
            }

            if (isChoiceDone)
            {
                timer += deltaTime;

                if (timer >= 0.2)
                {
                    charactersPanel.moove.X -= speed;
                }
                if (charactersPanel.moove.X <= -1900)
                {
                    timer = 0;
                    Tools.Get<GameState>().SceneSwitch(new InGame(difficultyChoice, nChoice));
                }
            }


            charactersPanel.Update(pGameTime);

            base.Update(pGameTime);
        }


        //------------------------------------------------------------


        internal override void Draw(GameTime pGameTime)
        {

            var draw = Tools.Get<Main>()._spriteBatch;
            draw.DrawString(Tools.Fonts["pokemon18"], stringToPrint, questionPos, Color.White);

            charactersPanel.Draw(pGameTime);

            base.Draw(pGameTime);
        }


    }




    //  ██╗███╗---██╗-██████╗--█████╗-███╗---███╗███████╗
    //  ██║████╗--██║██╔════╝-██╔══██╗████╗-████║██╔════╝
    //  ██║██╔██╗-██║██║--███╗███████║██╔████╔██║█████╗--
    //  ██║██║╚██╗██║██║---██║██╔══██║██║╚██╔╝██║██╔══╝--
    //  ██║██║-╚████║╚██████╔╝██║--██║██║-╚═╝-██║███████╗
    //  ╚═╝╚═╝--╚═══╝-╚═════╝-╚═╝--╚═╝╚═╝-----╚═╝╚══════╝
    class InGame : Scenes
    {
        bool isGameReady;
        int step;

        List<string> lstChoices;
        Vector2 panelPos1;
        Vector2 panelPos2;
        Panels choicePanel;
        float speedPanels;

        Vector2 boxPos1;
        Vector2 boxPos2;
        DialogueBox box;

        Actors player;
        Actors ia;
        int playerCharacter;
        int iaCharacter;
        internal float alphaPlayer;
        internal float alphaIA;

        int difficulty;

        internal int lastPlayerChoice;
        internal int lastIAChoice;
        internal Tuple<int, int> lastResult;

        string[] textesResult;


        //------------------------------------------------------------


        internal InGame(int pDifficulty, int pPlayerCharacter) : base()
        {
            isGameReady = false;
            step = 0;

            lstChoices = new List<string>() { "Attack", "Defend", "Special", "Exit" };
            panelPos1 = new Vector2(5,-500);
            panelPos2 = new Vector2(5,5);
            speedPanels = 30.0f;
            choicePanel = new Panels(panelPos1, lstChoices, this, 30);

            boxPos1 = new Vector2(1300, 595);
            boxPos2 = new Vector2(500, 595);
            box = new DialogueBox(boxPos1, 695, 200);

            playerCharacter = pPlayerCharacter;
            iaCharacter = Tools.Rnd(0, 3);
            player = ChooseCharacter(playerCharacter, false);
            ia = ChooseCharacter(iaCharacter, true);
            alphaPlayer = 0;
            alphaIA = 0;

            difficulty = pDifficulty;

            lastPlayerChoice = 0;
            lastIAChoice = 0;
            lastResult = new Tuple<int, int>(0, 0);
            textesResult = new string[] { "", "", "", "" };

            MediaPlayer.Play(Tools.Get<EffectsManager>().sngFight);
            MediaPlayer.Volume = 0.5f;
        }


        //------------------------------------------------------------


        internal override void Update(GameTime pGameTime)
        {
            if (!isGameReady)
            {
                ScenePreparation();
            }

            if (isGameReady)
            {
                switch (step)
                {
                    case 1:
                        TimeToPlay();
                        break;
                    case 2:
                        FightAnimation();
                        break;
                    case 3:
                        CalculResult();
                        break;
                    case 4:
                        TextDisplay1();
                        break;
                    case 5:
                        TextDisplay2();
                        break;
                    case 6:
                        EndGame();
                        break;
                }
            }


            choicePanel.Update(pGameTime);
            box.Update(pGameTime);
            player.Update(pGameTime);
            ia.Update(pGameTime);

            base.Update(pGameTime);
        }


        //------------------------------------------------------------


        internal override void Draw(GameTime pGameTime)
        {
;
            player.Draw(pGameTime);
            ia.Draw(pGameTime);

            choicePanel.Draw(pGameTime);
            box.Draw(pGameTime);

            base.Draw(pGameTime);
        }



        //------------------------------------------------------------

        Actors ChooseCharacter(int pCharacterChoice, bool isBot)
        {
            switch (pCharacterChoice)
            {
                case 0:
                    return new Damager(this, isBot);
                case 1:
                    return new Healer(this, isBot);
                case 2:
                    return new Tank(this, isBot);
                case 3:
                    return new Archer(this, isBot);
                default:
                    return null;
            }
        }

        //---------------

        void ScenePreparation()
        {
            Vector2 panelPos = choicePanel.canvasPos + choicePanel.moove;
            timer += deltaTime;
            if (timer >= 0.7)
            {
                if (panelPos.Y < panelPos2.Y) { choicePanel.moove.Y += speedPanels; }
                if (panelPos.Y >= panelPos2.Y) { choicePanel.moove.Y = -(panelPos1.Y - panelPos2.Y); }
                if (box.canvasPos.X > boxPos2.X) { box.canvasPos.X -= speedPanels * 1.5f; }
                if (box.canvasPos.X <= boxPos2.X) { box.canvasPos.X = boxPos2.X; }

                if (panelPos.Y >= panelPos2.Y && box.canvasPos.X <= boxPos2.X && timer >= 1)
                {
                    if (alphaPlayer < 1) { alphaPlayer += deltaTime * 4; }
                    if (alphaIA < 1) { alphaIA += deltaTime * 4; }
                    if (alphaPlayer >= 1) { alphaPlayer = 1.0f; }
                    if (alphaIA >= 1) { alphaIA = 1.0f; }

                    if (alphaPlayer >= 1 && alphaIA >= 1)
                    {
                        box.LoadText($"Opponent  choosed  {ia.name}.", "Let's  fight !");
                        isGameReady = true;
                        step++;
                        timer = 0;
                    }
                }
            }
        }

        //---------------

        void TimeToPlay()
        {
            if (box.isAllTextShowed)
            {
                if (timer <= 0) { box.LoadText("Choose  your  action . . ."); }
                timer += deltaTime;
                if (box.isAllTextShowed)
                {
                    isChoiceOn = true;
                    isChoiceDone = false;
                }
                if(isChoiceOn && !isChoiceDone && Tools.prv_keyboard.IsKeyUp(Keys.Enter) && Tools.keyboard.IsKeyDown(Keys.Enter))
                {
                    switch (choicePanel.cursorLane)
                    {
                        default:
                            lastPlayerChoice = choicePanel.cursorLane;
                            if(difficulty == 0) { lastIAChoice = (Tools.Rnd(0, 2)); } else { lastIAChoice = TurnManager.AdvanceIA(iaCharacter); }
                            isChoiceOn = false;
                            isChoiceDone = true;
                            step++;
                            timer = 0;
                            box.LoadText("Opponent  is  choosing  . . .", ". . .    . . .    . . .");
                            break;
                        case 3:
                            Tools.Get<Main>().Exit();
                            break;
                    }
                    Tools.Get<EffectsManager>().sndSelected.Play();
                }
            }
        }

        //---------------
        
        void FightAnimation()
        {
            if(timer <= 0)
            {
                box.LoadText($"You  choosed  {lstChoices[lastPlayerChoice]}  !",$"Opponent  choosed  {lstChoices[lastIAChoice]}  !");
                player.Action(lastPlayerChoice);
                ia.Action(lastIAChoice);
                Tools.Get<EffectsManager>().sndFight.Play();
            }
            timer += deltaTime;

            if (timer >= 2.4)
            {
                step ++;
                timer = 0;
                player.sprite.PlayAnim("idle");
                ia.sprite.PlayAnim("idle");
            }
        }

        //---------------

        void CalculResult()
        {
            lastResult = TurnManager.Resolution(playerCharacter, iaCharacter, lastPlayerChoice, lastIAChoice, ref textesResult);
            player.life += lastResult.Item1;
            ia.life += lastResult.Item2;
            if (player.life >= player.lifeMax) { player.life = player.lifeMax; }
            if (ia.life >= ia.lifeMax) { ia.life = ia.lifeMax; }
            if (player.life <= 0) { player.life = 0; }
            if (ia.life <= 0) { ia.life = 0; }
            player.lifeRatio = (float)player.life / (float)player.lifeMax;
            ia.lifeRatio = (float)ia.life / (float)ia.lifeMax;
            step++;
        }

        void TextDisplay1()
        {
            if (textesResult[0] == "" && textesResult[1] == "")
            {
                step++;
            }
            else
            {
                if(timer <= 0)
                {
                    if (textesResult[0] == "") { box.LoadText(textesResult[1]); }
                    else if (textesResult[1] == "") { box.LoadText(textesResult[0]); }
                    else { box.LoadText(textesResult[0], textesResult[1]); }
                }
                timer += deltaTime;
                if (box.isAllTextShowed)
                {
                    step++;
                    timer = 0;
                }
            }
        }

        //---------------

        void TextDisplay2()
        {
            if (timer <= 0) { box.LoadText(textesResult[2], textesResult[3]); }
            timer += deltaTime;
            if (box.isAllTextShowed)
            {
                if(player.life > 0 && ia.life > 0)
                {
                    timer = 0;
                    step = 1;
                }
                else
                {
                    timer = 0;
                    step ++;
                }
            }
        }

        //---------------

        void EndGame()
        {
            if(timer <= 0)
            {
                Tools.Get<EffectsManager>().sndDeath.Play();
                if (player.life == 0 && ia.life == 0)
                {
                    box.LoadText("You're  both  dead  !");
                }
                else if (player.life == 0 && ia.life > 0)
                {
                    box.LoadText("You're  dead  !");
                }
                else if (player.life >= 0 && ia.life == 0)
                {
                    box.LoadText("Opponent  is  dead  !");
                }
            }
            timer += deltaTime;
            if (player.life == 0 && ia.life == 0)
            {
                ia.sprite.pos.X += speedPanels;
                player.sprite.pos.Y += speedPanels;
                if (ia.sprite.pos.X >= 2000) { ia.sprite.pos.X = 2000; }
                if (player.sprite.pos.Y >= 2000) { player.sprite.pos.Y = 2000; }
            }
            else if (player.life == 0 && ia.life > 0)
            {
                player.sprite.pos.Y += speedPanels;
                if (player.sprite.pos.Y >= 2000) { player.sprite.pos.Y = 2000;}
            }
            else if (player.life >= 0 && ia.life == 0)
            {
                ia.sprite.pos.X += speedPanels;
                if (ia.sprite.pos.X >= 2000) { ia.sprite.pos.X = 2000; }
            }

            if(timer>= 2.8)
            {
                alphaIA -= deltaTime*4;
                alphaPlayer -= deltaTime*4;
                if (MediaPlayer.Volume > 0)
                {
                    MediaPlayer.Volume -= deltaTime;
                    if (MediaPlayer.Volume <= 0) { MediaPlayer.Volume = 0; }
                }
            }

            if(timer >= 3)
            {
                box.canvasPos.X += speedPanels;
                box.string1Pos.X += speedPanels;
                box.string2Pos.X += speedPanels;
                choicePanel.moove.Y -= speedPanels;
            }

            if(timer>= 4)
            {
                if (player.life == 0 && ia.life == 0)
                {
                    Tools.Get<GameState>().SceneSwitch(new FinalScene("Equality"));
                }
                else if (player.life == 0 && ia.life > 0)
                {
                    Tools.Get<GameState>().SceneSwitch(new FinalScene("Game Over"));
                }
                else if (player.life >= 0 && ia.life == 0)
                {
                    Tools.Get<GameState>().SceneSwitch(new FinalScene("Victory"));
                }
            }
        }
    }




    //  -██████╗--█████╗-███╗---███╗███████╗-██████╗-██╗---██╗███████╗██████╗-
    //  ██╔════╝-██╔══██╗████╗-████║██╔════╝██╔═══██╗██║---██║██╔════╝██╔══██╗
    //  ██║--███╗███████║██╔████╔██║█████╗--██║---██║██║---██║█████╗--██████╔╝
    //  ██║---██║██╔══██║██║╚██╔╝██║██╔══╝--██║---██║╚██╗-██╔╝██╔══╝--██╔══██╗
    //  ╚██████╔╝██║--██║██║-╚═╝-██║███████╗╚██████╔╝-╚████╔╝-███████╗██║--██║
    //  -╚═════╝-╚═╝--╚═╝╚═╝-----╚═╝╚══════╝-╚═════╝---╚═══╝--╚══════╝╚═╝--╚═╝
    class FinalScene : Scenes
    {

        string finalResult;
        float xPosTitle;
        float yPos1Title;
        float yPos2Title;
        float titleSpeed;
        Vector2 titleDims;
        Vector2 titlePos;

        List<string> lstChoice;
        Panels choicePanel;
        Vector2 panelPos1;
        Vector2 panelPos2;
        float speedPanel;
        int nChoice;

        SoundEffect sndFinal;
        bool isSndPlayed;

        //------------------------------------------------------------


        internal FinalScene(string pEnd) : base()
        {
            finalResult = pEnd;
            titleDims = Tools.Fonts["SketchGothicSchool120"].MeasureString(finalResult);
            xPosTitle = (Tools.Width - titleDims.X) / 2;
            yPos1Title = -200;
            yPos2Title = (Tools.Height / 2) - titleDims.Y;
            titlePos = new Vector2(xPosTitle, yPos1Title);
            titleSpeed = 2.5f;

            lstChoice = new List<string>() { "Play  again","Exit"};
            panelPos1 = new Vector2(1300, (Tools.Height / 2) +100);
            panelPos2 = new Vector2((Tools.Width / 2) - 150, (Tools.Height / 2) + 100);
            choicePanel = new Panels(panelPos1, lstChoice, this, 30);
            speedPanel = 30.0f;
            nChoice = 0;

            isSndPlayed = false;
            switch (finalResult)
            {
                case "Victory":
                    sndFinal = Tools.Get<EffectsManager>().sndVictory;
                    break;
                case "Game Over":
                    sndFinal = Tools.Get<EffectsManager>().sndGameOver;
                    break;
                case "Equality":
                    sndFinal = Tools.Get<EffectsManager>().sndEquality;
                    break;
            }

        }


        //------------------------------------------------------------


        internal override void Update(GameTime pGameTime)
        {
            Vector2 panelPos = choicePanel.canvasPos + choicePanel.moove;

            timer += deltaTime;

            if(!isChoiceOn && !isChoiceDone && timer >= 1.0f)
            {
                if (!isSndPlayed) { sndFinal.Play(); isSndPlayed = true; }

                titlePos.Y += titleSpeed;
                if(titlePos.Y >= yPos2Title)
                {
                    titlePos.Y = yPos2Title;
                }
            }

            if (!isChoiceOn && timer>= 4.0)
            {
                timer += deltaTime;

                if (timer >= 0.3 && panelPos.X >= panelPos2.X)
                {
                    choicePanel.moove.X -= speedPanel;
                }
                if (panelPos.X <= panelPos2.X)
                {
                    isChoiceOn = true;
                    timer = 0;
                    choicePanel.moove.X = -(panelPos1.X - panelPos2.X);
                }
            }

            if (isChoiceOn && !isChoiceDone && Tools.prv_keyboard.IsKeyUp(Keys.Enter) && Tools.keyboard.IsKeyDown(Keys.Enter))
            {
                nChoice = choicePanel.cursorLane;
                isChoiceDone = true;
                timer = 0;
                Tools.Get<EffectsManager>().sndSelected.Play();
            }

            if (isChoiceDone && timer >= 0.2f)
            {
                choicePanel.moove.X -= speedPanel;
                titlePos.X -= speedPanel;

                if(timer>= 1)
                {
                    if(nChoice == 0)
                    {
                        Tools.Get<GameState>().SceneSwitch(new Menu());
                    }
                    else
                    {
                        Tools.Get<Main>().Exit();
                    }
                }
            }


            choicePanel.Update(pGameTime);

            base.Update(pGameTime);
        }


        //------------------------------------------------------------


        internal override void Draw(GameTime pGameTime)
        {
            var draw = Tools.Get<Main>()._spriteBatch;
            draw.DrawString(Tools.Fonts["SketchGothicSchool120"], finalResult, titlePos, Color.White);
            choicePanel.Draw(pGameTime);

            base.Draw(pGameTime);
        }


    }




    //  ███████╗██╗███╗   ███╗██╗   ██╗██╗      █████╗ ████████╗██╗ ██████╗ ███╗   ██╗
    //  ██╔════╝██║████╗ ████║██║   ██║██║     ██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
    //  ███████╗██║██╔████╔██║██║   ██║██║     ███████║   ██║   ██║██║   ██║██╔██╗ ██║
    //  ╚════██║██║██║╚██╔╝██║██║   ██║██║     ██╔══██║   ██║   ██║██║   ██║██║╚██╗██║
    //  ███████║██║██║ ╚═╝ ██║╚██████╔╝███████╗██║  ██║   ██║   ██║╚██████╔╝██║ ╚████║
    //  ╚══════╝╚═╝╚═╝     ╚═╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝
    class Simulation : Scenes
    {
        Sprites board;

        Panels choicePanel;
        float panelPosY1;
        float panelPosY2;
        float speedPanel;

        float[] columsX;
        float[] linesY;
        Vector2[] allResultsPos;

        int[] damagerResults;
        int[] healerResults;
        int[] tankResults;
        int[] archerResults;
        int[][] boardData;

        bool isSimulationReady;
        bool isBoardResetable;

        int difficulty;
        int nChoice;

        int counter;
        int step;
        int[] charactersLife;
        int[] charactersLifeReset;

        Tuple<int, int>[] charactersMatchs;
        string[] useless;

        //------------------------------------------------------------


        internal Simulation(int pDifficulty) : base()
        {
            useless = new string[4];

            panelPosY1 = -300;
            panelPosY2 = 350;
            speedPanel = 30.0f;
            choicePanel = new Panels(new Vector2(850, panelPosY1), new List<string> { "Launch", "Return", "Exit" }, this, 30);

            board = new Sprites();
            board.img = Tools.Get<Main>().Content.Load<Texture2D>("images/SimulationBoard");
            board.pos = new Vector2(-200,-10);
            board.alpha = 0;

            columsX = new float[] { 244, 382, 515, 651 };
            linesY = new float[] { 247, 380, 517, 644 };
            allResultsPos = new Vector2[12];
            charactersMatchs = new Tuple<int, int>[12];

            int index = 0;
            for (int l = 0; l < linesY.Length; l++)
            {
                for (int c = 0; c < columsX.Length; c++)
                {
                    if (l != c)
                    {
                        allResultsPos[index] = new Vector2(columsX[c], linesY[l]);
                        charactersMatchs[index] = new Tuple<int, int>(l, c);
                        index++;
                    }
                }
            }

            damagerResults = new int[] { 0, 0, 0, 0 };
            healerResults = new int[] { 0, 0, 0, 0 };
            tankResults = new int[] { 0, 0, 0, 0 };
            archerResults = new int[] { 0, 0, 0, 0 };
            boardData = new int[][] { damagerResults, healerResults, tankResults, archerResults };

            isSimulationReady = false;
            nChoice = 0;

            difficulty = pDifficulty;

            counter = 0;
            step = 0;
            charactersLife = new int[] { 3, 4, 5, 3 };
            charactersLifeReset = new int[] { 3, 4, 5, 3 };
            isBoardResetable = true;
        }


        //------------------------------------------------------------


        internal override void Update(GameTime pGameTime)
        {

            if (!isSimulationReady)
            {
                Vector2 panelPos = choicePanel.canvasPos + choicePanel.moove;

                timer += deltaTime;
                if(timer>= 0.8f)
                {
                    if (board.alpha < 1) { board.alpha += deltaTime * 2; }
                    if (board.alpha >= 1)
                    {
                        board.alpha = 1;
                        if (panelPos.Y < panelPosY2) { choicePanel.moove.Y += speedPanel; }
                        if (panelPos.Y >= panelPosY2)
                        {
                            choicePanel.moove.Y = 650;
                            isChoiceOn = true;
                            isSimulationReady = true;
                            timer = 0;
                        }
                    }
                }
            }

            if (isChoiceOn && !isChoiceDone && Tools.prv_keyboard.IsKeyUp(Keys.Enter) && Tools.keyboard.IsKeyDown(Keys.Enter))
            {
                nChoice = choicePanel.cursorLane;
                isChoiceDone = true;
                isChoiceOn = false;
                Tools.Get<EffectsManager>().sndSelected.Play();
            }

            if (isChoiceDone)
            {
                switch(nChoice)
                {
                    case 0:
                        LaunchSimulation();
                        break;
                    case 1:
                        ReturnToMenu();
                        break;
                    case 2:
                        Tools.Get<Main>().Exit();
                        break;
                }
            }

            choicePanel.Update(pGameTime);
            board.Update(pGameTime);
            base.Update(pGameTime);
        }


        //------------------------------------------------------------


        internal override void Draw(GameTime pGameTime)
        {
            var draw = Tools.Get<Main>()._spriteBatch;
            board.Draw(pGameTime);
            int i = 0;
            for(int l=0; l< linesY.Length; l++)
            {
                for (int c =0; c < columsX.Length; c++)
                {
                    if (l != c)
                    {
                        draw.DrawString
                        (
                            Tools.Fonts["pokemon18"],
                            boardData[l][c].ToString(),
                            allResultsPos[i]- ((new Vector2(Tools.Fonts["pokemon18"].MeasureString(boardData[l][c].ToString()).X,0))/4),
                            Color.White * board.alpha
                        );
                        i++;
                    }
                    else
                    {
                        draw.DrawString(Tools.Fonts["pokemon18"], "X", new Vector2(columsX[c], linesY[l]), Color.Black);
                    }
                }
            }

            choicePanel.Draw(pGameTime);
            base.Draw(pGameTime);

        }


        //------------------------------------------------------------

        void ReturnToMenu()
        {
            Vector2 panelPos = choicePanel.canvasPos + choicePanel.moove;
            choicePanel.moove.Y += speedPanel;
            if(panelPos.Y >= 1000)
            {
                if(board.alpha > 0) { board.alpha -= deltaTime * 2; }
                if(board.alpha <= 0)
                {
                    board.alpha = 0;
                    Tools.Get<GameState>().SceneSwitch(new Menu());
                }
            }
        }

        void LaunchSimulation()
        {
            if (isBoardResetable)
            {
                Tools.Get<EffectsManager>().sndSimulation.Play();
                for (int l = 0; l < linesY.Length; l++)
                {
                    for (int c = 0; c < columsX.Length; c++)
                    {
                        boardData[l][c] = 0;
                    }
                }
                counter = 0;
                isBoardResetable = false;
            }
            if(counter < 100)
            {
                CharactersFights();
            }
            if (counter >= 100)
            {
                isChoiceDone = false;
                isChoiceOn = true;
                isBoardResetable = true;
            }
        }

        void CharactersFights()
        {
            while (step < 12)
            {
                while(charactersLife[charactersMatchs[step].Item1] > 0 && charactersLife[charactersMatchs[step].Item2] > 0)
                {
                    PlayARound(charactersMatchs[step].Item1, charactersMatchs[step].Item2, difficulty);
                }

                if (charactersLife[charactersMatchs[step].Item1] > 0 && charactersLife[charactersMatchs[step].Item2] <= 0)
                {
                    boardData[charactersMatchs[step].Item1][charactersMatchs[step].Item2]++;
                    NextMatch();
                }
                else
                {
                    NextMatch();
                }
            }
            if(step>=12)
            {
                counter++;
                step = 0;
            }
        }

        void PlayARound(int pPlayer, int pIA, int pDifficulty)
        {
            int playerChoice = Tools.Rnd(0, 2);
            int iaChoice = (pDifficulty == 0) ? Tools.Rnd(0, 2) : TurnManager.AdvanceIA(pIA);
            var result = TurnManager.Resolution(pPlayer, pIA, playerChoice, iaChoice, ref useless);
            charactersLife[charactersMatchs[step].Item1] += result.Item1;
            charactersLife[charactersMatchs[step].Item2] += result.Item2;
        }

        void NextMatch()
        {
            for(int i = 0; i < charactersLife.Length; i++)
            {
                charactersLife[i] = charactersLifeReset[i];
            }
            step++;
        }

    }
}
