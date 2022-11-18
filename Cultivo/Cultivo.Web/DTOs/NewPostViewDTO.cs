using Cultivo.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cultivo.Web.DTOs
{
    public class NewPostViewDTO
    {
        public int UserId { get; set; }

        [Display(Name = "Adicione uma imagem à postagem:")]
        public string image = "";

        [Display(Name = "Escreva algo aqui:")]
        public string written { get; set; }
    }
}
