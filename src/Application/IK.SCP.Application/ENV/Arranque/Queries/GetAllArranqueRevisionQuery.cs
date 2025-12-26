using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllArranqueRevisionQuery : IRequest<StatusResponse<object>>
    {
        public int ArranqueId { get; set; }
    }
    public class GetAllArranqueRevisionQueryHandler : IRequestHandler<GetAllArranqueRevisionQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllArranqueRevisionQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetAllArranqueRevisionQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<dynamic>("ENV.LISTAR_ARRANQUE_REVISION", new { p_ArranqueId = request.ArranqueId }, commandType: CommandType.StoredProcedure);

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }
}
