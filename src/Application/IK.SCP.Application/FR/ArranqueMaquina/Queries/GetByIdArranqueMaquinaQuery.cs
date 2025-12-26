using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetByIdArranqueMaquinaQuery : IRequest<StatusResponse<GetByIdArranqueMaquinaResponse>>
    {
        public int Linea { get; set; }
        public string Orden { get; set; }
        public int ArranqueMaquinaId { get; set; } = 0;
    }

    public class GetByIdArranqueMaquinaQueryHandler : IRequestHandler<GetByIdArranqueMaquinaQuery, StatusResponse<GetByIdArranqueMaquinaResponse>>
    {
        private readonly IUnitOfWork _uow;

        public GetByIdArranqueMaquinaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<GetByIdArranqueMaquinaResponse>> Handle(GetByIdArranqueMaquinaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using (var cnn = _uow.Context.CreateConnection)
                {
                    var parameters = new
                    {
                        p_Linea = request.Linea,
                        p_OrdenId = request.Orden,
                        p_ArranqueMaquinaId = request.ArranqueMaquinaId
                    };

                    var results = await cnn.QueryMultipleAsync("FR.OBTENER_ARRANQUE_MAQUINA_ACTIVO", parameters, commandType: CommandType.StoredProcedure);

                    var arranque = await results.ReadFirstOrDefaultAsync<GetByIdArranqueMaquinaResponse>();

                    if (arranque != null)
                    {
                        var condiciones = await results.ReadAsync<GetByIdArranqueMaquinaCondicionResponse>();
                        var verificaciones = await results.ReadAsync<GetByIdArranqueMaquinaVerificacionEquipoResponse>();
                        var observaciones = await results.ReadAsync<GetByIdArranqueMaquinaObservacionResponse>();

                        arranque.Condiciones = condiciones.ToList();
                        arranque.Verificaciones = verificaciones.ToList();
                        arranque.Observaciones = observaciones.ToList();

                        return new StatusResponse<GetByIdArranqueMaquinaResponse> { Ok = true, Data = arranque };
                    }

                    return new StatusResponse<GetByIdArranqueMaquinaResponse> { Ok = true };
                }
            }
            catch (Exception ex)
            {
                return new StatusResponse<GetByIdArranqueMaquinaResponse> { Ok = false, Message = "Error al ejecutar la consulta." };
            }
           
        }
    }

}
