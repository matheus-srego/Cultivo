using System.ComponentModel.DataAnnotations;

namespace Cultivo.Web.Models
{
    public class BaseViewModel
    {
        [Display(Name = "Identificador:")]
        public virtual int Id { get; set; }
    }
}
