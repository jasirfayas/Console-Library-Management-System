using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public abstract Roles Role { get; }

        public List<Book> issuedBooks = new List<Book>();

        public void DisplayDetails()
        {
            Console.WriteLine($"\t\t{Username,-20}{Password,-20}{Name,-20}{Role}");
        }
        public abstract void Dashboard(List<User> userList, List<Book> bookList);
    }
}