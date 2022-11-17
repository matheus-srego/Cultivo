using Cultivo.Domain.Constants;
using Cultivo.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cultivo.Web.Models
{
    public class PostViewModel : BaseEntity
    {
        public int UserId { get; set; }
        [Display(Name = "Escreva algo aqui:")]
        public string written { get; set; }
    }
}
