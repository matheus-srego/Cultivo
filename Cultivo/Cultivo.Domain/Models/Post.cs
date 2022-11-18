using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Domain.Models
{
    public class Post : BaseEntity
    {
        public int UserId { get; set; }
        public string image { get; set; }
        public string written { get; set; }
    }
}
