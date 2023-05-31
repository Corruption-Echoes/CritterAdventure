using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiny_Battler.Interface;

namespace Tiny_Battler
{
    internal class Loop
    {
        public List<Zone> Zones;
        public int CurrentZone;
        public Loop() 
        {
            //Initialize the zones
            Zones = new List<Zone>
            {
                new Zone_Field(0)
            };
            while (true)//Action selection loop
            {
                int selection = new OptionHandler(Zones[CurrentZone].printActions()).getSelection();
                Console.WriteLine(selection);
                while (true)//Action performance loop
                {
                    if (!Zones[CurrentZone].PerformAction(selection))                    
                    {//Break out of this loop once the action says it's been completed!
                        break;
                    }
                }//Break out of this loop when the game is closing!
            }
            Console.ReadLine();
        }
    }
}
