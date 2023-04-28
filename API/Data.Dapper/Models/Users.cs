using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace Data.Dapper.Models
{
    [Table("Users")]
    public class Users
    {
        [ExplicitKey]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(20)]
        [PasswordPropertyText]         
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }

}
