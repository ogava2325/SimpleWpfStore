using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalStore.ClassesForTables
{
    public class Administrator : User
    {
        public Administrator(){}
        public Administrator(string name, string lastName, string email, string phoneNumber, string password)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            Phonenumber = phoneNumber;
        }
    }
}
