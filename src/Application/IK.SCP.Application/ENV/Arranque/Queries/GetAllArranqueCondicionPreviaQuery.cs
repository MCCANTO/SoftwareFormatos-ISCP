using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllArranqueCondicionPreviaQuery : IRequest<StatusResponse<object>>
    {
        public int ArranqueId { get; set; }
    }

    public class GetAllArranqueCondicionPreviaQueryHandler : IRequestHandler<GetAllArranqueCondicionPreviaQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllArranqueCondicionPreviaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetAllArranqueCondicionPreviaQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<dynamic>("ENV.LISTAR_ARRANQUE_CONDICION_PREVIA", new { p_ArranqueId = request.ArranqueId }, commandType: CommandType.StoredProcedure);

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }
}