using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiny_Battler.Interface;

namespace Tiny_Battler
{
    internal class Combat
    {
        public Party PlayerParty;
        public Party Enemy;
        public Combat(Party enemy) 
        { 
            Enemy = enemy;
            PlayerParty = Player.playersParty;
            combatLoop();
        }
        public void combatLoop()
        {
            //Console.WriteLine("Checking if combat is resolved!");
            OptionHandler OH = new OptionHandler(PlayerParty.partyList[Player.currentPokemon].getMoveNames());

            while (!PlayerParty.AllFainted()||!Enemy.AllFainted())
            {

                //Console.WriteLine("Combat is occuring!");
                
                int playerSelection = OH.getSelection();
                int AISelection = Enemy.pickMove();
                int playerMove = PlayerParty.partyList[PlayerParty.activeMember].moves[playerSelection].ID;
                int AIMove = PlayerParty.partyList[PlayerParty.activeMember].moves[playerSelection].ID;
                int xp = 0;
                //Console.WriteLine(playerSelection);
                if (PlayerParty.getSpeed() > Enemy.getSpeed())
                {//Player is faster
                    xp = useMoves(PlayerParty, Enemy, playerMove);
                    if (xp > 0)
                    {
                        PlayerParty.partyList[PlayerParty.activeMember].grantXP(xp);
                    }
                    else
                    {
                        useMoves(Enemy, PlayerParty, AIMove);
                    }
                    if (Enemy.AllFainted())
                    {
                        Console.WriteLine("All the enemies have fainted!");
                        Console.ReadLine();
                        break;
                    }
                    if (PlayerParty.AllFainted())
                    {
                        Console.WriteLine("You have blacked out!");
                        Console.ReadLine();
                        break;
                    }
                }else
                {//Enemy is faster

                    useMoves(Enemy, PlayerParty, AIMove); 
                    if (PlayerParty.AllFainted())
                    {
                        Console.WriteLine("You have blacked out!");
                        Console.ReadLine();
                        break;
                    }
                    if (!Enemy.partyList[Enemy.activeMember].isFainted())
                    {
                        xp = useMoves(PlayerParty, Enemy, playerMove);
                        if (xp > 0)
                        {
                            PlayerParty.partyList[PlayerParty.activeMember].grantXP(xp);
                        }
                    }
                    if (Enemy.AllFainted())
                    {
                        Console.WriteLine("All the enemies have fainted!");
                        Console.ReadLine();
                        break;
                    }
                }
                drawParties();
                Console.ReadLine();
            }
            //Console.WriteLine("Combat has been resolved");
        }
        public int useMoves(Party attacker,Party defender, int move)
        {
            int damage = attacker.partyList[attacker.activeMember].calculateDamage(move);
            //Console.WriteLine(damage);
            int Type = Mechanics.loader.moveTemplates[move].Type;
            string type = Mechanics.types[Type];
            //Console.WriteLine(PlayerParty.partyList[PlayerParty.activeMember].getName() + " used " + Mechanics.loader.moveTemplates[playerMove].Name+" it was " + Mechanics.TypeEffectivenessToString(Mechanics.CalculateTypeEffectiveness(type, PlayerParty.partyList[PlayerParty.activeMember].species)));
            if(defender.damageCreature(damage, type, Mechanics.loader.moveTemplates[move].Mitigator))
            {
                return Mechanics.loader.speciesTemplates[defender.partyList[defender.activeMember].species].XPgranted * defender.partyList[defender.activeMember].Level;
            }
            return 0;
        }
        public void drawParties()
        {
            Console.SetCursorPosition(InterfaceOptions.CombatDrawOffset.X, InterfaceOptions.CombatDrawOffset.Y);
            Console.Write(Enemy.partyList[Enemy.activeMember].getName());
            Console.SetCursorPosition(InterfaceOptions.EnemyHPBarPosition.X, InterfaceOptions.EnemyHPBarPosition.Y);
            BarRenderer.drawHPBar(Enemy.partyList[Enemy.activeMember].currentHP, Enemy.partyList[Enemy.activeMember].determineStat(Species.Health), false);
            Console.SetCursorPosition(InterfaceOptions.AllyHPBarPosition.X, InterfaceOptions.AllyHPBarPosition.Y);
            BarRenderer.drawHPBar(PlayerParty.partyList[PlayerParty.activeMember].currentHP, PlayerParty.partyList[PlayerParty.activeMember].determineStat(Species.Health),true);
            Console.SetCursorPosition(InterfaceOptions.AllyXPBarPosition.X, InterfaceOptions.AllyXPBarPosition.Y);
            BarRenderer.drawXPBar(PlayerParty.partyList[PlayerParty.activeMember].xp, PlayerParty.partyList[PlayerParty.activeMember].getXPToLevel(),true);
        }
        

    }
}
