using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Console.WriteLine("Checking if combat is resolved!");
            OptionHandler OH = new OptionHandler(PlayerParty.partyList[Player.currentPokemon].getMoveNames());

            while (!PlayerParty.AllFainted()||!Enemy.AllFainted())
            {
                Console.WriteLine("Combat is occuring!");
                int playerSelection = OH.getSelection();
                Console.WriteLine(playerSelection);
            }
            Console.WriteLine("Combat has been resolved");
        }
    }
}
