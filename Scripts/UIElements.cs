using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;

namespace JeuDeCombat
{
    internal class Panels
    {
        string longestString;
        int longestStringSize;
        int elements;
        int recX;
        int recY;
        int recWidth;
        int recHeight;
        int interLines;
        int stringHeight;
        internal Vector2 canvasPos;

        List<string> lstChoices;
        List<Vector2> lstChoicesPos;

        internal Sprites cursor1;
        internal Sprites cursor2;
        internal int cursorLane;
        Scenes scene;
        Vector2 cursorOffSet;

        internal Vector2 moove;


        //------------------------------------------------------------


        internal Panels(Vector2 pPos, List<string> pLstChoices, Scenes pScene, int pInterLineSize)
        {
            cursor1 = new Sprites();
            cursor2 = new Sprites();
            cursor1.img = Tools.Get<Main>().Content.Load<Texture2D>("images/cursor1");
            cursor2.img = Tools.Get<Main>().Content.Load<Texture2D>("images/cursor2");

            interLines = pInterLineSize;
            stringHeight = (int)Tools.Fonts["pokemon18"].MeasureString("123Exemple").Y;
            lstChoices = pLstChoices;
            elements = lstChoices.Count;
            recX = (int)pPos.X;
            recY = (int)pPos.Y;
            canvasPos = pPos;
            longestStringSize = 0;
            lstChoicesPos = new List<Vector2>();

            for(int i=0; i < lstChoices.Count; i++)
            {
                if (longestStringSize <= Tools.Fonts["pokemon18"].MeasureString(lstChoices[i]).X)
                {
                    longestStringSize = (int)Tools.Fonts["pokemon18"].MeasureString(lstChoices[i]).X;
                    longestString = lstChoices[i];
                }

                lstChoicesPos.Add(canvasPos + new Vector2(cursor1.img.Width, (interLines * (i + 1)) + (stringHeight * i)));
            }

            recHeight = ((elements + 1) * interLines) + (elements * stringHeight);
            recWidth = (interLines * 2) + longestStringSize + cursor1.img.Width;

            cursorLane = 0;
            scene = pScene;
            cursorOffSet = new Vector2(-cursor1.img.Width - 5, -cursor1.img.Height / 2 + 14);

            moove = Vector2.Zero;
        }


        //------------------------------------------------------------


        internal void Update(GameTime pGameTime)
        {
            EffectsManager mediaPlayer = Tools.Get<EffectsManager>();

            if (scene.isChoiceOn && !scene.isChoiceDone)
            {
                if (Tools.prv_keyboard.IsKeyUp(Keys.Up) && Tools.keyboard.IsKeyDown(Keys.Up))
                {
                    if(cursorLane > 0)
                    {
                        cursorLane--;
                        mediaPlayer.sndSelect.Play();
                    }
                }
                else if (Tools.prv_keyboard.IsKeyUp(Keys.Down) && Tools.keyboard.IsKeyDown(Keys.Down))
                {
                    if(cursorLane < lstChoices.Count-1)
                    {
                        cursorLane++;
                        mediaPlayer.sndSelect.Play();
                    }
                }
            }
        }


        //------------------------------------------------------------


        internal void Draw(GameTime pGameTime)
        {
            var draw = Tools.Get<Main>()._spriteBatch;
            draw.Draw(Tools.DrawRec(recWidth, recHeight, Color.White), canvasPos + moove, Color.White);
            draw.Draw(Tools.DrawRec(recWidth - 2, recHeight - 2, Color.Black), canvasPos + new Vector2(1, 1) + moove, Color.White);
            draw.Draw(Tools.DrawRec(recWidth - 8, recHeight - 8, Color.White), canvasPos + new Vector2(4, 4) + moove, Color.White);
            draw.Draw(Tools.DrawRec(recWidth - 14, recHeight - 14, Color.Black), canvasPos + new Vector2(7, 7) + moove, Color.White);

            for(int i = 0; i < lstChoices.Count; i++)
            {
                draw.DrawString(Tools.Fonts["pokemon18"], lstChoices[i], lstChoicesPos[i] + moove, Color.White);
            }

            if (!scene.isChoiceDone)
            {
                draw.Draw(cursor1.img, lstChoicesPos[cursorLane] + cursorOffSet + moove, Color.White);
            }
            else
            {
                draw.Draw(cursor2.img, lstChoicesPos[cursorLane] + cursorOffSet + moove, Color.White);
            }
        }
    }

    //  ====================================================
    //  ====================================================
    //  ====================================================
    //  ====================================================
    //  ====================================================

    class DialogueBox
    {
        int recX;
        int recY;
        int recWidth;
        int recHeight;
        internal Vector2 canvasPos;


        internal string stringToLoaded1;
        internal string stringToShow1;
        internal Vector2 string1Pos;
        int counter1;

        internal string stringToLoaded2;
        internal string stringToShow2;
        internal Vector2 string2Pos;
        int counter2;

        internal float timer;
        float deltaTime;
        bool isText1Showed;
        bool isText2Showed;
        internal bool isAllTextShowed;


        //------------------------------------------------------------


        internal DialogueBox(Vector2 pPos, int pWidth, int pHeight)
        {
            recX = (int)pPos.X;
            recY = (int)pPos.Y;
            recWidth = pWidth;
            recHeight = pHeight;
            canvasPos = pPos;   


            stringToLoaded1 = "";
            stringToShow1 = "";
            string1Pos = new Vector2(540, 650);
            counter1 = -1;

            stringToLoaded2 = "";
            stringToShow2 = "";
            string2Pos = new Vector2(540, 710);
            counter2 = -1;

            isText1Showed = true;
            isText2Showed = true;
            isAllTextShowed = true;
            timer = 0.2f;
            deltaTime = 1.0f / 60.0f;
        }


        //------------------------------------------------------------


        internal void Update(GameTime pGameTime)
        {

            if (!isText1Showed)
            {
                timer -= deltaTime;
                if (timer <= 0)
                {
                    counter1++;
                    timer = Tools.random.Next(1, 11) / 100;
                    if (counter1 < stringToLoaded1.Length)
                    {
                        stringToShow1 += stringToLoaded1[counter1];
                    }
                    if (counter1 >= stringToLoaded1.Length)
                    {
                        timer = 0.2f;
                        isText1Showed = true;
                    }
                }
            }

            if(isText1Showed && !isText2Showed)
            {
                timer -= deltaTime;
                if (timer <= 0)
                {
                    counter2++;
                    timer = Tools.random.Next(1, 11) / 100;
                    if (counter2 < stringToLoaded2.Length)
                    {
                        stringToShow2 += stringToLoaded2[counter2];
                    }
                    if (counter2 >= stringToLoaded2.Length)
                    {
                        timer = 0;
                        isText2Showed = true;
                    }
                }
            }

            if(isText1Showed && isText2Showed)
            {
                timer += deltaTime;

                if(stringToShow1 == "Choose  your  action . . .")
                {
                    if (timer >= 0.1f) { isAllTextShowed = true; }
                }
                else
                {
                    if (timer >= 1.2f) { isAllTextShowed = true; }
                }
            }

        }


        //------------------------------------------------------------


        internal void Draw(GameTime pGameTime)
        {

            //Canvas
            var draw = Tools.Get<Main>()._spriteBatch;
            draw.Draw(Tools.DrawRec(recWidth, recHeight, Color.White), canvasPos, Color.White);
            draw.Draw(Tools.DrawRec(recWidth - 2, recHeight - 2, Color.Black), canvasPos + new Vector2(1, 1), Color.White);
            draw.Draw(Tools.DrawRec(recWidth - 8, recHeight - 8, Color.White), canvasPos + new Vector2(4, 4), Color.White);
            draw.Draw(Tools.DrawRec(recWidth - 14, recHeight - 14, Color.Black), canvasPos + new Vector2(7, 7), Color.White);

            //Texts
            draw.DrawString(Tools.Fonts["pokemon18"], stringToShow1, string1Pos, Color.White);
            draw.DrawString(Tools.Fonts["pokemon18"], stringToShow2, string2Pos, Color.White);

        }


        //------------------------------------------------------------


        internal void LoadText(string pText1 = "  ", string pText2 = "  ")
        {
            stringToLoaded1 = pText1;
            stringToShow1 = "";
            counter1 = -1;

            stringToLoaded2 = pText2;
            stringToShow2 = "";
            counter2 = -1;

            isText1Showed = false;
            isText2Showed = false;
            isAllTextShowed = false;

            timer = 0.2f;
        }
    }
}
