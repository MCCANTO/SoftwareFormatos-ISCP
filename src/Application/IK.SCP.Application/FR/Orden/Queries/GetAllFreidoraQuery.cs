using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllFreidoraQuery : IRequest<StatusResponse<object>>
    {
    }

    public class GetAllFreidoraQueryHandler : IRequestHandler<GetAllFreidoraQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllFreidoraQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetAllFreidoraQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<dynamic>("FR.LISTAR_FREIDORA", commandType: CommandType.StoredProcedure);

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }

}
