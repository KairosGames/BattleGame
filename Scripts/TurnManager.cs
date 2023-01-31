using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuDeCombat
{
    static public class TurnManager
    {
        public class Player
        {
            internal string Name = "";
            internal int Power = 0;
            internal int Life = 0;
        }

        public class Ordinateur
        {
            internal string Name = "";
            internal int Power = 0;
            internal int Life = 0;
        }

        internal static Tuple<int, int> Resolution(int pPlayer, int pIA, int pPlayerAction, int pIaAction, ref string[] txtResult)
        {
            for(int i =0; i < txtResult.Count(); i++)
            {
                txtResult[i] = "";
            }

            //Construction classe
            var classeHumain = new Player();
            var classeOrdi = new Ordinateur();

            //Choix classe humain
            int choixH = pPlayer;

            //Attribution classe
            if (choixH == 0)
            {
                classeHumain.Name = "Damager";
                classeHumain.Power = 2;
                classeHumain.Life = 3;
            }
            else if (choixH == 1)
            {
                classeHumain.Name = "Healer";
                classeHumain.Power = 1;
                classeHumain.Life = 4;
            }
            else if (choixH == 2)
            {
                classeHumain.Name = "Tank";
                classeHumain.Power = 1;
                classeHumain.Life = 5;
            }
            else if (choixH == 3)
            {
                classeHumain.Name = "Archer";
                classeHumain.Power = 2;
                classeHumain.Life = 3;
            }

            //Choix random de classe
            int choixO = pIA;

            //Choix classe ordi
            if (choixO == 0)
            {
                classeOrdi.Name = "Damager";
                classeOrdi.Power = 2;
                classeOrdi.Life = 3;
            }
            else if (choixO == 1)
            {
                classeOrdi.Name = "Healer";
                classeOrdi.Power = 1;
                classeOrdi.Life = 4;
            }
            else if (choixO == 2)
            {
                classeOrdi.Name = "Tank";
                classeOrdi.Power = 1;
                classeOrdi.Life = 5;
            }
            else if (choixO == 3)
            {
                classeOrdi.Name = "Archer";
                classeOrdi.Power = 2;
                classeOrdi.Life = 3;
            }

            //Choix action joueur
            int choixHumain = pPlayerAction;
            int choixOrdi = pIaAction;


            int degatHumain = 0;
            int degatOrdi = 0;

            //Savoir si le joueur ou l'ordi se défends
            bool isHDefend = false;
            bool isODefend = false;

            //Pour l'archer
            bool isHide = false;
            bool oneShot;
            int vieHPrecedant = classeHumain.Life;
            int vieOPrecedant = classeOrdi.Life;

            //Evenement selon les attaque
            if (choixHumain == 0)
            {
                if (classeHumain.Name == "Archer") //On vérifie si le joueur à la classe archer, car celle-ci possède une attaque différente
                {
                    isHide = false;
                    degatOrdi += AttaqueArcher(ref isHide, false, ref txtResult) ;

                    if (isHide == true) { degatHumain -= 1; isHDefend = true; } //Si l'archer arrive à se cacher, il bloque un dégâts
                }
                else
                {
                    degatOrdi += classeHumain.Power; //Si le joueur attaque, on ajoute au dégat ordi le power du joueur
                }
            }
            else if (choixHumain == 1) { degatHumain -= classeOrdi.Power; isHDefend = true; } //Si le joueur se défend, on enlève au dégat du joueur le power de l'ordi


            if (choixOrdi == 0)
            {
                if (classeOrdi.Name == "Archer") //On vérifie si le joueur à la classe archer, car celle-ci possède une attaque différente
                {
                    isHide = false;
                    degatHumain += AttaqueArcher(ref isHide, true, ref txtResult);

                    if (isHide == true) { degatOrdi -= 1; isODefend = true; } //Si l'archer arrive à se cacher, il bloque un dégâts
                }
                else
                {
                    degatHumain += classeOrdi.Power; //Si l'ordi attaque, on ajoute au dégat humain le power de l'ordi
                }
            }
            else if (choixOrdi == 1) { degatOrdi -= classeHumain.Power; isODefend = true; } //Si l'ordi se défends, on enlève au dégat de l'ordi le power du joueur

            //Si attaque spécial
            if (choixHumain == 2)
            {
                if (classeHumain.Name == "Damager")
                {
                    if (degatHumain > 0)
                    {
                        degatOrdi += degatHumain; //Renvoie les dégats pris par le joueur à l'adversaire, uniquement si les dégats sont positif
                    }
                }
                else if (classeHumain.Name == "Healer") { degatHumain = SpecialHeal(degatHumain); }
                else if (classeHumain.Name == "Tank")
                {
                    degatHumain += 1;
                    degatOrdi = SpecialTank(degatOrdi, false, ref txtResult);
                }
                else if (classeHumain.Name == "Archer")
                {
                    oneShot = false;
                    oneShot = SpecialArcher(false, ref txtResult);

                    if (oneShot == true)
                    {
                        degatOrdi += classeOrdi.Life + classeHumain.Power; //One shot l'adversaire
                    }
                    else
                    {
                        txtResult[0] = "You  missed  you're  special  skill  !";
                    }
                }
            }

            //Si attaque spécial
            if (choixOrdi == 2)
            {
                if (classeOrdi.Name == "Damager")
                {
                    if (degatOrdi > 0)
                    {
                        degatHumain += degatOrdi;
                    }
                }
                else if (classeOrdi.Name == "Healer") { degatOrdi = SpecialHeal(degatOrdi); }
                else if (classeOrdi.Name == "Tank")
                {
                    degatOrdi += 1;
                    degatHumain = SpecialTank(degatHumain, true, ref txtResult);
                }
                else if (classeOrdi.Name == "Archer")
                {
                    oneShot = false;
                    oneShot = SpecialArcher(true, ref txtResult);

                    if (oneShot == true)
                    {
                        degatHumain += classeHumain.Life + classeOrdi.Power; //One shot l'adversaire
                    }
                    else
                    {
                        txtResult[1] = "Opponent  missed  his  special  skill  !";
                    }
                }
            }

            //Si l'ordinateur ou le joueur c'est défendue, mais que celui-ci n'a pas attaqué (deux défense ou défense spécial healer),
            //alors on remet tout à zéro pour éviter les soins
            if (isHDefend == true && degatHumain < 0) { degatHumain = 0; }
            if (isODefend == true && degatOrdi < 0) { degatOrdi = 0; }

            //Affiche un message différent selon si le joueur ou l'ordi c'est soigné ou s'il à infligé des dégat
            if (degatHumain < 0)
            {
                classeHumain.Life += Math.Abs(degatHumain);
                txtResult[2] = $"You're  healed  by  {Math.Abs(degatHumain)}  HP.";
                degatHumain = 0;
            }
            else
            {
                if (degatOrdi < 0) //Fix à un problème : Si l'ordinateur se soignait, affichait que nous avions mis - 2 dégat
                {
                    txtResult[2] = "You  didn't  make  any  damage.";
                }
                else
                {
                    txtResult[2] = $"You  dealt  {degatOrdi}  damage(s)  !";
                }
            }

            if (degatOrdi < 0)
            {
                classeOrdi.Life += Math.Abs(degatOrdi);
                txtResult[3] = $"Opponent  healed  himself  by  { Math.Abs(degatOrdi)}  HP.";
                degatOrdi = 0;
            }
            else
            {
                txtResult[3] = $"Opponent  dealt  {degatHumain}  damage(s)  !";
            }

            classeHumain.Life -= degatHumain;
            classeOrdi.Life -= degatOrdi;

            
            if (classeHumain.Life <= 0) { classeHumain.Life = 0; }
            if (classeOrdi.Life <= 0) { classeOrdi.Life = 0; }


            degatHumain = classeHumain.Life - vieHPrecedant;
            degatOrdi = classeOrdi.Life - vieOPrecedant;

            Tuple<int, int> degat = new Tuple<int, int>(degatHumain, degatOrdi);
            return degat;
        }

        static int SpecialHeal(int degat)
        {
            return degat - 2;
        }

        static int SpecialTank(int degat, bool isBot, ref string[] txtResult)
        {
            degat += 2;

            if (isBot)
            {
                txtResult[1] = "Opponent  sacrified  1  HP.";
            }
            else
            {
                txtResult[0] = "You  sacrified  1  HP.";
            }

            return degat;
        }

        static int AttaqueArcher(ref bool isHide, bool isBot, ref string[] txtResult)
        {
            
            Random r = new Random();
            int randomDegat = r.Next(1, 3);
            int randomMiss = r.Next(0, 2);

            if (randomMiss == 0)
            {
                isHide = true;
                if (isBot)
                {
                    txtResult[1] = "Opponent  dodged  a  damage  !";
                }
                else
                {
                    txtResult[0] = "You  dodged  a  damage  !";
                }
            }

            return randomDegat;
        }

        static bool SpecialArcher(bool isBot, ref string[] txtResult)
        {
            Random r = new Random();
            int randomHeadShot = r.Next(0, 5);

            if (randomHeadShot == 0)
            {
                if (isBot)
                {
                    txtResult[1] = "Opponent  shot  you're  head  !";
                }
                else
                {
                    txtResult[0] = "You  did  a  headshot  !";
                }
                return true;
            }

            return false;
        }

        internal static int AdvanceIA(int classe)
        {
            Random r = new Random();
            int random = 0;
            random = r.Next(1, 11);
            int choixIA = 0;

            //Pour le damager, va privilégier l'attaque et la défence
            if (classe == 0)
            {
                if (random <= 4) { choixIA = 1; }
                else if (random <= 8) { choixIA = 2; }
                else { choixIA = 3; }

                return choixIA - 1;
            }
            //Pour le healer, va privilégier l'attaque spécial et l'attaque
            else if (classe == 1)
            {
                if (random <= 4) { choixIA = 3; }
                else if (random <= 8) { choixIA = 1; }
                else { choixIA = 2; }

                return choixIA - 1;
            }
            //Pour le tank, va privilégier la défence et la capacité spécial
            else if (classe == 2)
            {
                if (random <= 4) { choixIA = 2; }
                else if (random <= 8) { choixIA = 3; }
                else { choixIA = 1; }

                return choixIA - 1;
            }
            //Pour l'archer, va privilégier la défense et l'attaque spécial
            else if (classe == 3)
            {
                if (random <= 4) { choixIA = 3; }
                else if (random <= 7) { choixIA = 2; }
                else { choixIA = 1; }

                return choixIA - 1;
            }

            return choixIA - 1;
        }
    }
}
