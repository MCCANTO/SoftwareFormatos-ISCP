using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllEvaluacionAtributoQuery : IRequest<StatusResponse<List<GetAllEvaluacionAtributoResponse>>>
    {
        public int Linea { get; set; }
        public string OrdenId{ get; set; }
    }

    public class GetAllEvaluacionAtributoQueryHandler : IRequestHandler<GetAllEvaluacionAtributoQuery, StatusResponse<List<GetAllEvaluacionAtributoResponse>>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllEvaluacionAtributoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<List<GetAllEvaluacionAtributoResponse>>> Handle(GetAllEvaluacionAtributoQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {

                var parametros = new { p_Linea = request.Linea, p_OrdenId = request.OrdenId };

                var results = await cnn.QueryAsync<GetAllEvaluacionAtributoResponse>("FR.LISTAR_EVALUACION_ATRIBUTO", parametros, commandType: CommandType.StoredProcedure);

                return new StatusResponse<List<GetAllEvaluacionAtributoResponse>> { Ok = true, Data = results.ToList() };

            }
        }
    }
}
