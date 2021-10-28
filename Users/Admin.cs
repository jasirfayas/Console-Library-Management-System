using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Admin : User
    {
        public override Roles Role => Roles.Admin;

        public override void Dashboard(List<User> ulist, List<Book> bookList)
        {
            var op = new Operations(ulist, bookList, this);
            var choice = "";
            while (choice != "q")
            {
                Console.Clear();
                Console.WriteLine("\n\t[ADMIN DASHBOARD]");
                Console.WriteLine($"\n\tWelcome {this.Name},");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t\t[Manage Users]");
                Console.WriteLine("\t\t (1) Add new user");
                Console.WriteLine("\t\t (2) Update a user");
                Console.WriteLine("\t\t (3) Delete a user");
                Console.WriteLine("\t\t (4) View all users");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\n\t\t[Manage Books]");
                Console.WriteLine("\t\t (5) Add a book");
                Console.WriteLine("\t\t (6) Delete a book");
                Console.WriteLine("\t\t (7) View all books");
                Console.WriteLine("\t\t (8) Search a book");
                Console.ResetColor();
                Console.WriteLine("\n\t\t(9) Update your own details");
                Console.WriteLine("\t\t(q) Log out");
                Console.Write("\n\t\tEnter your choice: ");
                choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        op.AddUser();
                        break;
                    case "2":
                        op.UpdateUser();
                        break;
                    case "3":
                        op.DeleteUser();
                        break;
                    case "4":
                        op.ViewAllUsers();
                        break;
                    case "5":
                        op.AddBooks();
                        break;
                    case "6":
                        op.DeleteBooks();
                        break;
                    case "7":
                        op.ViewAllBooks();
                        break;
                    case "8":
                        op.SearchBook();
                        break;
                    case "9":
                        op.UpdateOwnDetails(this);
                        break;
                    case "q":
                        Beautify.Warning("\n\t\tYou are logged out!");
                        break;
                    default:
                        break;
                }
            }
            Beautify.ClearScreen("go back to login screen");
        }
    }
}
