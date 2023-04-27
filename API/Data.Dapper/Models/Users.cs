using System;
using Dapper.Contrib.Extensions;

namespace Data.Dapper.Models
{
    [Table("Users")]
    public class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
    }

}
