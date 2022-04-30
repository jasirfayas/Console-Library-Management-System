using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    /// <summary>
    /// To add colors to text in the console. Also avoid repetetive code in the program.
    /// </summary>
    public static class Beautify
    {
        public static void ClearScreen(string str)
        {
            Console.Write("\n\n\t\tPress any key to " + str + "...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Error(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t" + str);
            Console.ResetColor();
        }

        public static void Success(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\t" + str);
            Console.ResetColor();
        }

        public static void Warning(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\t" + str);
            Console.ResetColor();
        }

        public static void Info(string str)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t\t" + str);
            Console.ResetColor();
        }

        public static string ReadPassword() //Password Masking
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
    }
}