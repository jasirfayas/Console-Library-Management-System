using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Librarian : User
    {
        public override Roles Role => Roles.Librarian;
        //List<Book> toIssue = new List<Book>();
        public override void Dashboard()
        {
            var op = new Operations(this);
            var choice = "";
            while (choice != "q")
            {
                Console.Clear();
                Console.WriteLine("\n\t[LIBRARIAN DASHBOARD]");
                Console.WriteLine($"\n\tWelcome {this.Name},");
                Console.WriteLine("\n\t\t[MENU]");
                Console.WriteLine("\t\t(1) Add a book");
                Console.WriteLine("\t\t(2) Delete a book");
                Console.WriteLine("\t\t(3) View all books");
                Console.WriteLine("\t\t(4) Search a book");
                Console.WriteLine("\t\t(5) Issue reserved books");
                Console.WriteLine("\t\t(6) Return books");
                Console.WriteLine("\t\t(7) Update your own details");
                Console.WriteLine("\t\t(q) Log out");
                Console.Write("\n\t\tEnter your choice: ");
                choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        op.AddBooks();
                        break;
                    case "2":
                        op.DeleteBooks();
                        break;
                    case "3":
                        op.ViewAllBooks();
                        break;
                    case "4":
                        op.SearchBook();
                        break;
                    case "5":
                        op.GetReservedList();
                        break;
                    case "6":
                        op.GetReturnList();
                        break;
                    case "7":
                        op.UpdateOwnDetails(this);
                        break;
                    case "q":
                        Beautify.Warning("\n\t\tYou are logged out!");
                        break;
                    default:
                        break;
                }
            }
            Beautify.ClearScreen("go back to login sceen");
        }

    }
}