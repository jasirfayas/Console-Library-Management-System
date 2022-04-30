using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Operations //all the users call their methods from this class
    {
        private User currentUser;
        public Operations(User activeUser)
        {
            currentUser = activeUser;
        }

        //private methods, used only inside this class
        bool CheckIfEmpty(string userInput, string item)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Beautify.Error(item + " cannot be empty!");
                return true;
            }
            return false;
        }
        User GetUserObject(string uName)
        {
            foreach (var user in Home.allUsers)
            {
                if (uName == user.Username) //username exists
                {
                    return user;
                }
            }
            return null; //username doesn't exist
        }
        void AddUserToList(User userObj)
        {
            bool success = false;
            while (!success)
            {
            Username:
                Console.Write("\n\n\t\tEnter username: ");
                var uName = Console.ReadLine();
                if (CheckIfEmpty(uName, "Username"))
                    goto Username;
                if (GetUserObject(uName) != null)
                {
                    Beautify.Warning("Username already exists!");
                    return;
                }
                userObj.Username = uName;

            Password:
                Console.Write("\n\t\tEnter password: ");
                userObj.Password = Console.ReadLine();
                if (CheckIfEmpty(userObj.Password, "Password"))
                    goto Password;

                FullName:
                Console.Write("\n\t\tEnter Full Name: ");
                userObj.Name = Console.ReadLine();
                if (CheckIfEmpty(userObj.Name, "Full Name"))
                    goto FullName;

                Home.allUsers.Add(userObj);
                Beautify.Success("user is added to the database.");
                success = true;
            }

        }
        void ReturnBook(Book book)
        {
            if (book.Status == BookStatus.In)
            {
                Beautify.Error("This book is already IN.");
                return;
            }
            if (book.BorrowedUser?.Name != currentUser.Name)
            {
                Beautify.Warning("Can't Return. This book is not borrowed by you.");
                return;
            }
            book.Status = BookStatus.Return;
            Beautify.Info("The book is scheduled to return.");
            return;
        }
        void ReserveBook(Book book)
        {
            if (book.Status != BookStatus.In)
            {
                Beautify.Error("Book is not available!");
                return;
            }
            book.Status = BookStatus.Reserved;
            book.ReservedUser = currentUser;
            Beautify.Info("The book is successsfully reserved.");
        }
        void BookOperations(Book book)
        {
            Console.WriteLine("\n\t\t(1) Reserve\t(2) Return\t(3) Go Back");
            while (true)
            {
                Console.Write("\n\t\tChoose an option: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ReserveBook(book);
                        break;
                    case "2":
                        ReturnBook(book);
                        break;
                    case "3":
                        return;
                    default:
                        Beautify.Error("Invalid Choice!");
                        break;
                }
            }
        }

        //Manage Users
        internal void AddUser()
        {
            var choice = "";
            while (choice != "q")
            {
                Console.WriteLine("\n\n\t\t[ADD NEW USER]");
                Console.WriteLine("\n\t\t(1) Admin");
                Console.WriteLine("\t\t(2) Librarian");
                Console.WriteLine("\t\t(3) Student");
                Console.WriteLine("\n\t\t(q) Go Back");
                Console.Write("\n\t\tEnter your choice: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("\n\t\tADD NEW ADMIN: ");
                        var admin = new Admin();
                        int adminCount = Home.allUsers.Count(user => user.Role == Roles.Admin);
                        if (adminCount == 5)
                        {
                            Beautify.Error("Maximum admin count reached!");
                            break;
                        }
                        AddUserToList(admin);
                        break;
                    case "2":
                        Console.Write("\n\t\tADD NEW LIBRARIAN: ");
                        var librarian = new Librarian();
                        AddUserToList(librarian);
                        break;
                    case "3":
                        Console.Write("\n\t\tADD NEW STUDENT: ");
                        var student = new Student();
                        AddUserToList(student);
                        break;
                    case "q":
                        return;
                    default:
                        Beautify.Error("Invalid Choice!");
                        break;
                }
                Beautify.ClearScreen("continue");
            }
        }
        internal void UpdateUser()
        {
            Console.WriteLine("\n\n\t\t[UPDATE USERS]");
            Console.Write("\t\tEnter the username to update: ");
            var uName = Console.ReadLine();
            if (GetUserObject(uName) == null)
            {
                Console.WriteLine("\n\tUser doesn't exist!");
                return;
            }
            User upUser = GetUserObject(uName);
            AddUserToList(upUser);
            Beautify.ClearScreen("continue");
        }
        internal void ViewAllUsers()
        {
            Console.WriteLine("\n\n\t\t[USER DATABASE]");
            Console.WriteLine($"\n\t\t{"Username",-20}{"Password",-20}{"Full Name",-20}Role");
            Console.WriteLine("\t\t".PadRight(70, '-'));
            foreach (User user in Home.allUsers)
            {
                user.DisplayDetails();
            }
            Beautify.ClearScreen("continue");
        }
        internal void DeleteUser()
        {
            Console.WriteLine("\n\n\t\t[DELETE USER]");
            Console.Write("\t\tEnter the username to delete: ");
            string uName = Console.ReadLine();
            User delUser = GetUserObject(uName);

            if (delUser == null)
            {
                Beautify.Error("User doesn't exist!");
                Beautify.ClearScreen("continue");
                return;
            }

            int adminCount = Home.allUsers.Count(user => user.Role == Roles.Admin);

            if (delUser.Role == Roles.Admin && adminCount == 1)
            {
                Beautify.Error("Can't delete the only admin!");
            }
            else
            {
                Home.allUsers.Remove(delUser);
            }
            Beautify.ClearScreen("continue");
        }
        internal void UpdateOwnDetails(User userObj)
        {
            Console.WriteLine("\n\n\t\t[UPDATE YOUR DETAILS]");
            Console.WriteLine("\n\t\t(1) Username\t(2) Password\t(3) Full Name\t(4) Go Back");
            Console.Write("\n\t\tChoose an option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("\n\t\tEnter new username: ");
                    userObj.Username = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("\n\t\tEnter new password: ");
                    userObj.Password = Console.ReadLine();
                    break;
                case "3":
                    Console.Write("\n\t\tChange Full Name: ");
                    userObj.Name = Console.ReadLine();
                    break;
                case "4":
                default:
                    Console.Clear();
                    return;
            }
            Beautify.Info("Details updated successfully.");
            Beautify.ClearScreen("continue");
        }

        //Manage Books
        internal void AddBooks()
        {
            Console.WriteLine("\n\n\t\t[ADD BOOK]");
            var bk = new Book();
            Console.Write("\n\t\tEnter title: ");
            bk.Title = Console.ReadLine();
            Console.Write("\t\tEnter Author name: ");
            bk.Author = Console.ReadLine();
            Home.allBooks.Add(bk);
            Beautify.Success("Book added successfully!");
            Beautify.ClearScreen("continue");
        }
        internal void DeleteBooks()
        {
            Console.WriteLine("\n\n\t\t[DELETE A BOOK]");
            Console.Write("\n\t\tEnter title of the book: ");
            var inputTitle = Console.ReadLine();
            foreach (var book in Home.allBooks)
            {
                if (inputTitle == book.Title)
                {
                    Home.allBooks.Remove(book);
                    Beautify.Warning("Book is deleted!");
                    Beautify.ClearScreen("continue");
                    return;
                }
            }
            Beautify.Error("Book doesn't exist!");
            Beautify.ClearScreen("continue");
        }
        internal void ViewAllBooks()
        {
            Console.WriteLine("\n\n\t\t[BOOKS DATABASE]");
            Console.WriteLine($"\n\t\t{"Title",-20}{"Author",-20}{"Status",-15}Borrowed by");
            Console.WriteLine("\t\t".PadRight(70, '-'));
            foreach (var book in Home.allBooks)
            {
                book.DisplayDetails();
            }
            Beautify.ClearScreen("continue");
        }
        internal void GetReservedList()
        {
            Console.WriteLine("\n\n\t\t[BOOKS TO ISSUE]");
            Console.WriteLine($"\n\t\t{"Title",-20}{"Author",-20}{"Status",-15}Borrowed by");
            Console.WriteLine("\t\t".PadRight(70, '-'));
            foreach (var book in Home.allBooks)
            {
                if (book.ReservedUser != null)
                {
                    book.DisplayDetails();
                }
            }
            Console.Write("\n\t\tEnter title of the book to issue: ");
            var bTitle = Console.ReadLine();
            foreach (var book in Home.allBooks)
            {
                if (bTitle == book.Title)
                {
                    book.BorrowedUser = book.ReservedUser;
                    book.ReservedUser = null;
                    book.BorrowedUser.issuedBooks.Add(book);
                    book.Status = BookStatus.Out;
                    Beautify.Success("The book is issued!");
                    Beautify.ClearScreen("continue");
                    return;
                }
            }
            Beautify.Error("Book doesn't exist");
            Beautify.ClearScreen("continue");
        }
        internal void GetReturnList()
        {
            Console.WriteLine("\n\n\t\t[BOOKS TO RETURN]");
            Console.WriteLine($"\n\t\t{"Title",-20}{"Author",-20}{"Status",-15}Borrowed by");
            Console.WriteLine("\t\t".PadRight(70, '-'));
            foreach (var book in Home.allBooks)
            {
                if (book.Status == BookStatus.Return)
                {
                    book.DisplayDetails();
                }
            }
            Console.Write("\n\t\tEnter title of the book to return: ");
            var bTitle = Console.ReadLine();
            foreach (var book in Home.allBooks)
            {
                if (bTitle == book.Title)
                {
                    book.BorrowedUser.issuedBooks.Remove(book);
                    book.BorrowedUser = null;
                    book.Status = BookStatus.In;
                    Beautify.Success("The book is returned!");
                    Beautify.ClearScreen("continue");
                    return;
                }
            }
            Beautify.Error("Book doesn't exist");
            Beautify.ClearScreen("continue");
        }
        internal void SearchBook()
        {
            Console.WriteLine("\n\n\t\t[SEARCH A BOOK]");
            while (true)
            {
                Console.Write("\n\t\tEnter title of the book: ");
                var bTitle = Console.ReadLine();
                foreach (var book in Home.allBooks)
                {
                    if (bTitle == book.Title)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\t\t[SEARCH RESULTS]");
                        Console.WriteLine($"\n\t\t{"Title",-20}{"Author",-20}{"Status",-15}Borrowed by");
                        Console.WriteLine("\t\t".PadRight(70, '-'));
                        book.DisplayDetails();
                        BookOperations(book);
                        return;
                    }
                }
                Beautify.Error("Book doesn't exist");
            }
            //Beautify.ClearScreen("continue");
        }
        internal void ViewBorrowed()
        {
            if (currentUser.issuedBooks.Count == 0)
            {
                Console.WriteLine("\n\t\tEmpty List!");
                Beautify.ClearScreen("continue");
                return;
            }
            Console.WriteLine("\n\n\t\t[BOOKS YOU BORROWED]");
            Console.WriteLine($"\n\t\t{"Title",-20}{"Author",-20}{"Status",-15}Borrowed by");
            Console.WriteLine("\t\t".PadRight(70, '-'));
            foreach (var book in currentUser.issuedBooks)
            {
                book.DisplayDetails();
            }
            Beautify.ClearScreen("continue");
        }
    }
}