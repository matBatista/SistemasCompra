using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento Condicao { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            Condicao = new CondicaoPagamento(0);
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void CondicaoPagamento(Decimal valor)
        {
            if(valor > 50000) Condicao = new CondicaoPagamento(30);
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            bool existe = false;

            decimal total = 0;

            foreach (Item item in itens)
            {
                total += Convert.ToDecimal(item.Subtotal);
                existe = true;
            }

            if (!existe) throw new BusinessRuleException("Não existe itens na compra.");

            CondicaoPagamento(total);

            AddEvent(new CompraRegistradaEvent(Id,itens, total));
        }
    }
}
