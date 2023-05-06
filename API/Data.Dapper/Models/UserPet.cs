using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Models
{
    [Table("UserPets")]
    public class UserPet
    {
        [ExplicitKey]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public Guid UserTypeId { get; set; }
        public Guid PetTypeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string SizeOrBreed { get; set; }
        public bool Neutered { get; set; }
        public bool HasPatologies { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        
    }
}
