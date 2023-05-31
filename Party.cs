using System;
using System.Collections.Generic;
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
    }
}
