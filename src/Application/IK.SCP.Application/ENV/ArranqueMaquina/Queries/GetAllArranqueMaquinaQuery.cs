using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllArranqueMaquinaQuery : GetAllArranqueMaquinaQueryRequest, IRequest<StatusResponse<List<GetAllArranqueMaquinaQueryResponse>>>
    {
    }

    public class GetAllArranqueMaquinaQueryHandler : IRequestHandler<GetAllArranqueMaquinaQuery, StatusResponse<List<GetAllArranqueMaquinaQueryResponse>>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllArranqueMaquinaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<GetAllArranqueMaquinaQueryResponse>>> Handle(GetAllArranqueMaquinaQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var results = await cnn.QueryAsync<GetAllArranqueMaquinaQueryResponse>("ENV.PA_LISTAR_ARRANQUE_MAQUINA", new { p_EnvasadoraId = request.EnvasadoraId, p_OrdenId = request.OrdenId }, commandType: CommandType.StoredProcedure);

                return new StatusResponse<List<GetAllArranqueMaquinaQueryResponse>> {Ok = true, Data = results.ToList() };

            }
        }
    }
}
