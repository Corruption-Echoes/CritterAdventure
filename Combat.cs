using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        public void combatLoop()
        {
            while(!PlayerParty.AllFainted()||!Enemy.AllFainted())
            {
                Console.WriteLine("Combat is occuring!");
            }
        }
    }
}
