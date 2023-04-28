using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;

namespace Data.Dapper.Models
{
    [Table("Users")]
    public class UserProfile
    {
        [ExplicitKey]
        public Guid Id { get; set; } = Guid.NewGuid();        
        public Guid UserId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [EmailAddress]
        [MaxLength(255)]
        public string EmailAddress { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [Phone]
        [MaxLength(50)]
        public string Phone { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
