using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinThreadPool
{
    public class User
    {
        public static object a;
        public static int IdForNewJson { get; set; } = 0;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email {get; set;}
        public DateTime DateOfBirth { get; set; }
        public List<User> Users { get; set; } = new List<User>();

        public override string ToString()
        {
            return $"{Name} {Surname} {Email} {DateOfBirth.ToString("dd.MM.yyyy")}";
        }
    }
}
