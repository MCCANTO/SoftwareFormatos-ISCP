using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetByIdEvaluacionAtributoQuery : IRequest<StatusResponse<GetByIdEvaluacionAtributoResponse>>
    {
        public int Id { get; set; }
    }

    public class GetByIdEvaluacionAtributoQueryHandler : IRequestHandler<GetByIdEvaluacionAtributoQuery, StatusResponse<GetByIdEvaluacionAtributoResponse>>
    {
        private readonly IUnitOfWork _uow;
        public GetByIdEvaluacionAtributoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<GetByIdEvaluacionAtributoResponse>> Handle(GetByIdEvaluacionAtributoQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {

                var parametros = new { p_EvaluacionAtributoId = request.Id };

                var results = await cnn.QueryMultipleAsync("FR.OBTENER_EVALUACION_ATRIBUTO_X_ID", parametros, commandType: CommandType.StoredProcedure);

                var evaluacion = await results.ReadFirstOrDefaultAsync<GetByIdEvaluacionAtributoResponse>();

                var panelistas = await results.ReadAsync<GetByIdEvaluacionAtributoPanelistaResponse>();

                evaluacion.Panelistas = panelistas.ToList();

                return new StatusResponse<GetByIdEvaluacionAtributoResponse> { Ok = true, Data = evaluacion };

            }
        }
    }
}
