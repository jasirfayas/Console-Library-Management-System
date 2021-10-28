using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    class Program
    {
        public static List<User> allUsers = new List<User>();
        public static List<Book> allBooks = new List<Book>();
        static void Main(string[] args)
        {
            allUsers.Add(new Admin { Username = "admin", Password = "admin", Name = "John" });
            allUsers.Add(new Librarian { Username = "librarian", Password = "lpass", Name = "Lucy" });
            allUsers.Add(new Student { Username = "student", Password = "spass", Name = "Peter" });

            allBooks.Add(new Book { Title = "book1", Author = "author1" });

            while (true) //user login and password entering
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n\t\tWelcome to the Library Management System!");
                Console.ResetColor();
                Console.WriteLine($"\n\t\t{"".PadRight(10, '~')} LOGIN {"".PadRight(10, '~')}");

                bool userExists = false;
                Console.Write("\n\t\tEnter Username: ");
                var username = Console.ReadLine();
                User loggedUser = default;

                foreach (var user in allUsers) //username validation
                {
                    if (username == user.Username)
                    {
                        userExists = true;
                        loggedUser = user;
                        break;
                    }
                }

                if (!userExists)
                {
                    Beautify.Error("Invalid username!");
                    Beautify.ClearScreen("go back..");
                    continue;
                }

                Console.Write("\n\t\tEnter Password: ");
                var password = Beautify.ReadPassword();

                if (password != loggedUser.Password) //password validation
                {
                    Beautify.Error("Incorrect password!");
                    Beautify.ClearScreen("go back..");
                    continue;
                }
                Beautify.Success("Successfully logged in !");
                Beautify.ClearScreen("go to your dashboard...");

                loggedUser.Dashboard(allUsers, allBooks);
            }
        }

    }
}
