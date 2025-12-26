using Dapper;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetArranqueVariableBasicaQuery : IRequest<StatusResponse>
    {
        public int ArranqueVariableBasicaCabId { get; set; }
    }

    public class GetArranqueVariableBasicaQueryHandler : IRequestHandler<GetArranqueVariableBasicaQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetArranqueVariableBasicaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(GetArranqueVariableBasicaQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var results = await cnn.QueryAsync<dynamic>("ENV.OBTENER_ARRANQUE_VARIABLE_BASICA", new { p_ArranqueVariableBasicaCabId = request.ArranqueVariableBasicaCabId }, commandType: CommandType.StoredProcedure);


                if (results == null) return null;

                var data = results.ToList();

                var variables = data.GroupBy(g => g.Padre)
                                    .Select(x => new
                                    {
                                        padre = x.Key,
                                        items = x.Select(y => new
                                        {
                                            id = y.Id,
                                            variableBasicaId = y.VariableBasicaId,
                                            nombre = y.Nombre,
                                            comentario = y.Comentario,
                                            valor = y.Valor,
                                            observacion = y.Observacion,
                                            cerrado = y.Cerrado ?? false,
                                            primerOrden = y.PrimerOrden,
                                            segundoOrden = y.SegundoOrden
                                        }).ToList()
                                    });

                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: variables);

            }
        }
    }
}
