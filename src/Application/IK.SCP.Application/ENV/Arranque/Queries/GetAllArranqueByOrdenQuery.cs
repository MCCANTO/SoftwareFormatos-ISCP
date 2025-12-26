using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllArranqueByOrdenQuery : IRequest<StatusResponse<List<GetArranqueEnvasadoResponse>>>
    {
        public int envasadoraId { get; set; }
        public string orden { get; set; }
    }

    public class GetAllArranqueByOrdenQueryHandler : IRequestHandler<GetAllArranqueByOrdenQuery, StatusResponse<List<GetArranqueEnvasadoResponse>>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllArranqueByOrdenQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<GetArranqueEnvasadoResponse>>> Handle(GetAllArranqueByOrdenQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var results = await cnn.QueryAsync<GetArranqueEnvasadoResponse>("ENV.OBTENER_ALL_ARRANQUE_POR_ORDEN", new { p_EnvasadoraId = request.envasadoraId, p_OrdenId = request.orden }, commandType: CommandType.StoredProcedure);

                return new StatusResponse<List<GetArranqueEnvasadoResponse>> { Ok = true, Data = results.ToList() };
            }
        }
    }
}
