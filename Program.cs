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
            xpScales x = new xpScales();
            x.init(0.35);
            Mechanics.XPScales.Add(x);

            XMLLoader = new Loader();
            Loop mainLoop= new Loop();
            Console.Read();
        }
    }
}
