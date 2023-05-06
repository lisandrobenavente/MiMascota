using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Models
{
    [Table("PetType")]
    public class PetType
    {
        [ExplicitKey]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
