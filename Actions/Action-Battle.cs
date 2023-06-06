using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    internal class Action_Battle : Action
    {
        Combat currentCombat;
        public Action_Battle() : base("Battle","Find a wild creature to fight!")
        {

        }
        public override bool PerformAction(wildListing foeDesign)
        {
            Console.WriteLine("Performing Action!");
            Console.WriteLine("Performing Action!");
            Console.WriteLine("Performing Action!");
            Console.WriteLine("Performing Action!");
            Console.WriteLine("Performing Action!");
            Creature critter=new Creature(foeDesign.Species,Creature.ZeroIV,Creature.generateIVS(),foeDesign.pickLevel());
            Party wildParty = new Party();
            wildParty.AddCreature(critter);
            currentCombat = new Combat(wildParty);
            return false;
        }
    }
}
