using System;
using System.Collections.Generic;
using System.Text;

namespace App18.Model
{
    public class Student
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public DateTime Birthday { get; set; }
    }
}
