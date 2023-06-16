using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler.Interface
{
    static public class InterfaceOptions
    {
        public static point OptionsOffset = new point(0, 10);
        public static point CombatDrawOffset = new point(0, 1);
        public static point EnemyHPBarPosition = new point(20, 1);
        public static point AllyHPBarPosition = new point(20, 8);
        public static point AllyXPBarPosition = new point(20, 9);
        public static int BarLengths = 10;
        public static ConsoleColor HighlightColor = ConsoleColor.Green;
        public static ConsoleColor DefaultColor = ConsoleColor.Black;
        public static ConsoleColor xpBlue= ConsoleColor.Blue;
        public static ConsoleColor darkBlue = ConsoleColor.DarkBlue;
        public static ConsoleColor TextColor = ConsoleColor.Yellow;
        public static ConsoleColor HighlightText = ConsoleColor.White;
        public static ConsoleColor WarningColor = ConsoleColor.Red;

    }
}
