using System.ComponentModel.DataAnnotations;

namespace Cultivo.Web.Models
{
    public class BaseViewModel
    {
        [Key]
        [Display(Name = "Identificador:")]
        public virtual int Id { get; set; }
    }
}
