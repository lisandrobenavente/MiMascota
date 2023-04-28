using Dapper.Contrib.Extensions;
using System;

namespace Data.Dapper.Models
{
    [Table("Users")]
    public class UserProfile
    {
        [ExplicitKey]
        public Guid Id { get; set; }        
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
