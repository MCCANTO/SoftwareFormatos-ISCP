using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetArranqueByOrdenQuery : IRequest<StatusResponse<GetArranqueByOrdenResponse>>
    {
        public int envasadoraId { get; set; }
        public string orden { get; set; }
    }

    public class GetArranqueByOrdenQueryHandler : IRequestHandler<GetArranqueByOrdenQuery, StatusResponse<GetArranqueByOrdenResponse>>
    {
        private readonly IUnitOfWork _uow;

        public GetArranqueByOrdenQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<GetArranqueByOrdenResponse>> Handle(GetArranqueByOrdenQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var item = await cnn.QueryFirstOrDefaultAsync<GetArranqueByOrdenResponse>("ENV.OBTENER_ARRANQUE_POR_ORDEN", new { p_EnvasadoraId = request.envasadoraId, p_OrdenId = request.orden }, commandType: CommandType.StoredProcedure);

                return new StatusResponse<GetArranqueByOrdenResponse>()
                {
                    Ok = true,
                    Data = item
                };
            }
        }
    }
}
