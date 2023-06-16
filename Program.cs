using System;

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
            Mechanics.XPScales.Add(x);//TODO extend to implement more than 1 XP scale

            XMLLoader = new Loader();
            Loop mainLoop = new Loop();
            Console.Read();
        }
        public static void clearScreen()
        {
            Console.Clear();
        }
    }
}
