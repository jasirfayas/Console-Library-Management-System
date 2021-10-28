using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public BookStatus Status { get; set; }
        public User BorrowedUser;
        public User ReservedUser;
        public void DisplayDetails()
        {
            Console.WriteLine($"\t\t{this.Title,-20}{this.Author,-20}{this.Status,-15}{BorrowedUser?.Name}");
        }
    }
}
