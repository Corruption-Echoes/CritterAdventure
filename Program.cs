using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tiny_Battler
{
    internal class Program
    {
        public static Loader XMLLoader;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Mechanics.XPScales.Add(new xpScales());
            Mechanics.XPScales[0].init(0.35);

            XMLLoader = new Loader();
            Loop mainLoop= new Loop();
            Console.Read();
        }
    }
}
