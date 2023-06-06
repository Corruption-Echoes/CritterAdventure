using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    class Player
    {
        public static Party playersParty;
        public static int currentPokemon = 0;

        static Player()
        {
            playersParty = new Party();
            playersParty.AddCreature(new Creature(0,Creature.ZeroIV,Creature.ZeroIV, 5));
        }
        public static void swapPokemon(int swapTo)
        {
            currentPokemon = swapTo;
        }
    }
}
