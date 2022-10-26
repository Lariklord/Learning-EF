using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect
{
    public partial class User
    {
        [Column("User_id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public Company? Company { get; set; }
        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
    [NotMapped,Table("Company LTD")]//не создавать,название в бд
    public partial class Company
    {
        public int Id { get; set; }
        [NotMapped]
        string? name;
        [Required]
        public string? Name => name;
        public Company(string name)
        {
            this.name = name;
        }
    }

}
