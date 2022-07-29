using MediatR;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarProdutoCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitarCompraRepository;

        public RegistrarProdutoCommandHandler(SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this.solicitarCompraRepository = solicitarCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacao = new SolicitacaoCompraAgg.SolicitacaoCompra(request.usuarioSolicitante,request.nomeFornecedor);
            
            IEnumerable<SolicitacaoCompraAgg.Item> itens = null;
            
            solicitacao.RegistrarCompra(itens);

            solicitarCompraRepository.RegistrarCompra(solicitacao);

            Commit();
            PublishEvents(solicitacao.Events);

            return Task.FromResult(true);
        }
    }
}
