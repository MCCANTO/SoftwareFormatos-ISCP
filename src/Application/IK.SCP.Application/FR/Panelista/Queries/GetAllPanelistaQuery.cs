using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllPanelistaQuery : IRequest<StatusResponse<List<PanelistaResponse>>>
    {
    }

    public class GetAllPanelistaQueryHandler : IRequestHandler<GetAllPanelistaQuery, StatusResponse<List<PanelistaResponse>>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllPanelistaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<PanelistaResponse>>> Handle(GetAllPanelistaQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<PanelistaResponse>("FR.LISTAR_PANELISTAS", commandType: CommandType.StoredProcedure);

                return new StatusResponse<List<PanelistaResponse>>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }
}
