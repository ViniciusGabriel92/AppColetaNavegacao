using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppColetaNavegacao.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name ="Autor")]
        [MaxLength(80)]
        public string Autor { get; set; }

        [Display(Name = "Publicação")]
        public DateTime DataPublicacao { get; set; }

        [Display(Name = "Anunciante")]
        public string Anunciante { get; set; }
        [Display(Name = "Contato")]
        public string TelAnunciante { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
