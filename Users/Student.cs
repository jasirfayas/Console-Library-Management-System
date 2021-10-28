using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Student : User
    {
        public override Roles Role => Roles.Student;

        public override void Dashboard(List<User> ulist, List<Book> bookList)
        {
            var op = new Operations(null, bookList, this); ;
            var choice = "";
            while (choice != "q")
            {
                Console.Clear();
                Console.WriteLine("\n\t[STUDENT DASHBOARD]");
                Console.WriteLine($"\n\tWelcome {this.Name},");
                Console.WriteLine("\n\t\t[MENU]");
                Console.WriteLine("\t\t(1) View all books");
                Console.WriteLine("\t\t(2) Search a book");
                Console.WriteLine("\t\t(3) View issued books");
                Console.WriteLine("\t\t(4) Update your own details");
                Console.WriteLine("\t\t(q) Log out");
                Console.Write("\n\t\tEnter your choice: ");
                choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        op.ViewAllBooks();
                        break;
                    case "2":
                        op.SearchBook();
                        break;
                    case "3":
                        op.ViewBorrowed();
                        break;
                    case "4":
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
