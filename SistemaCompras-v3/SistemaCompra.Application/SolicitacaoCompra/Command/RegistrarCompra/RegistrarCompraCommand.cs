using MediatR;
using System;
using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string usuarioSolicitante { get; set; }
        public string nomeFornecedor { get; set; }
        public IEnumerable<ItemViewModel> itens { get; set; }
    }
}
