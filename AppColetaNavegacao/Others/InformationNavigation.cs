using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppColetaNavegacao.Others
{
    public class InformationNavigation
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public OPERACAO OPERACAO { get; set; }
    }
}
