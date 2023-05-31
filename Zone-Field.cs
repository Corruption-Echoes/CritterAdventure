using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tiny_Battler
{
    internal class Zone_Field:Zone
    {
        public List<wildListing> encounters;
        
        public Zone_Field(int zoneID):base(zoneID)
        {
            Name = "Field";
            Description = "A grassy field";
            populateEncounters();
            populateActions();
        }
        public void populateEncounters()
        {
            encounters=Program.XMLLoader.GetWildListings(ZoneID);
        }
        public void populateActions()
        {
            Actions = new List<Action>();
            Actions.Add(new Action_Battle());
            Actions.Add(new Action_Bag());
            Actions.Add(new Action_Heal());
        }
        public override bool PerformAction(int selection)
        {
            if (selection == 0)
            {
                List<int> weights = new List<int>();
                foreach (wildListing wild in encounters)
                {
                    weights.Add(wild.Weight);
                }
                int enemy = Mechanics.PickFromWeightedList(weights); 
                //Console.WriteLine("You encountered a " + encounters[enemy].Species);
                return (Actions[0].PerformAction(encounters[enemy]));
            }
            return false;
        }
    }
}
