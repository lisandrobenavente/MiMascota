using Dapper.Contrib.Extensions;

namespace Data.Dapper.Models
{
    [Table("Users")]
    public class UserProfile
    {
        [ExplicitKey]
        public Guid Id { get; set; } = Guid.NewGuid();        
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string EmailAddress { get; set; }
        
        public string Address { get; set; }
        [System.ComponentModel.DataAnnotations.Phone]
        public string Phone { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
}
