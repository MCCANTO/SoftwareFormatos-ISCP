using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllArranqueMaquinaQuery: IRequest<StatusResponse<List<GetAllArranqueMaquinaResponse>>>
    {
        public int Linea { get; set; }
        public string OrdenId { get; set; }

    }

    public class GetAllArranqueMaquinaQueryHandler : IRequestHandler<GetAllArranqueMaquinaQuery, StatusResponse<List<GetAllArranqueMaquinaResponse>>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllArranqueMaquinaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<GetAllArranqueMaquinaResponse>>> Handle(GetAllArranqueMaquinaQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var parametros = new { p_Linea = request.Linea, p_OrdenId = request.OrdenId };

                var items = await cnn.QueryAsync<GetAllArranqueMaquinaResponse>("FR.LISTAR_ARRANQUE_MAQUINA", parametros, commandType: CommandType.StoredProcedure);

                return new StatusResponse<List<GetAllArranqueMaquinaResponse>>()
                {
                    Ok = true,
                    Data = items.ToList()
                };
            }
        }
    }
}
