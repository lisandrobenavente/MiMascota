using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Models
{
    [Table("UserType")]
    public class UserType
    {
        [ExplicitKey]
        public Guid Id { get; set; } = Guid.NewGuid();        
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
