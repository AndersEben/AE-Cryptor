using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_Crypt
{
    public class IO_Handler
    {

        public bool ConsoleWrite = true;

        public bool C_Pause = false;

        public void WConsole(string _text, C_Stats _stat, C_Type _type)
        {
            switch (_stat)
            {
                case C_Stats.weiß:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case C_Stats.grün:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case C_Stats.rot:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case C_Stats.blau:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case C_Stats.gelb:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }

            if (ConsoleWrite)
            {
                switch (_type)
                {
                    case C_Type.NewLine:
                        if (!C_Pause)
                        {
                            Console.WriteLine(_text);
                        }
                        else
                        {

                        }

                        break;
                    case C_Type.Line:
                        Console.Write(_text);
                        break;
                    default:
                        break;
                }

            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public enum C_Stats : int
        {
            weiß = 0,
            grün = 1,
            rot = 2,
            blau = 3,
            gelb = 4
        }

        public enum C_Type : int
        {
            NewLine = 0,
            Line = 1
        }

    }
}
