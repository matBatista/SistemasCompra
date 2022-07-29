using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ItemViewModel
    {
        public int Categoria { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public int Situacao { get; set; }
    }
}
