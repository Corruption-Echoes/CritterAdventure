using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    internal class Zone
    {
        public List<Action> Actions;
        public string Name;
        public string Description;
        public int ZoneID;
        public Zone(int zoneID) 
        { 
            ZoneID = zoneID;
        }
        public List<string> printActions()
        {
            List<string> options=new List<string>();
            foreach(Action action in Actions)
            {
                options.Add(action.printMenu());
            }
            return options;
        }
        public virtual bool PerformAction(int selection)
        {
            return false;
        }
    }
}
