using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllEnvasadoraQuery : IRequest<StatusResponse<object>>
    {
    }

    public class GetAllEnvasadoraQueryHandler : IRequestHandler<GetAllEnvasadoraQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<GetAllEnvasadoraQueryHandler> _logger;
        public GetAllEnvasadoraQueryHandler(IUnitOfWork uow, ILogger<GetAllEnvasadoraQueryHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<StatusResponse<object>> Handle(GetAllEnvasadoraQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<dynamic>("ENV.LISTAR_ENVASADORA", commandType: CommandType.StoredProcedure);
                
                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }

}
