using System.Data;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllVerificacionEquipoQuery : IRequest<StatusResponse<object>>
    {
        public int arranqueMaquinaId { get; set; }
    }

    public class GetAllVerificacionEquipoQueryHandler : IRequestHandler<GetAllVerificacionEquipoQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllVerificacionEquipoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<object>> Handle(GetAllVerificacionEquipoQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var parametros = new { p_ArranqueMaquinaId = request.arranqueMaquinaId };

                var items = await cnn.QueryAsync<dynamic>("FR.LISTAR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO", parametros, commandType: CommandType.StoredProcedure);

                var data = items.ToList();

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = data
                };
            }
        }
    }
}

