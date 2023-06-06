using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    internal class Party
    {
        public List<Creature> partyList;
        public Party()
        {
            partyList = new List<Creature>();
        }
        public void AddCreature(Creature creature)
        {
            partyList.Add(creature);
        }
        public bool AllFainted()
        {
            bool toReturn = true;
            foreach(Creature creature in partyList)
            {
                Console.WriteLine("Checking a critter: "+creature.currentHP);
                if (!creature.isFainted())//Check if the current creature is fainted
                {
                    toReturn = false;
                    break;//Break the first time we find a living critter, we only need one or more, not a count
                }
            }
            return toReturn;
        }
    }
}
