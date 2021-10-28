using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public enum Roles
    {
        Student,
        Librarian,
        Admin,
    }

    public enum BookStatus
    {
        In,
        Out,
        Reserved,
        Return
    }
}
