using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ArranqueMaquina.ViewModels.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetArranqueMaquinaQuery : IRequest<StatusResponse<ArranqueMaquinaViewModel>>
    {
        public int envasadoraId { get; set; }
        public string ordenId { get; set; }
    }

    public class GetArranqueMaquinaQueryHandler : IRequestHandler<GetArranqueMaquinaQuery, StatusResponse<ArranqueMaquinaViewModel>>
    {
        private readonly IUnitOfWork _uow;
        public GetArranqueMaquinaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<ArranqueMaquinaViewModel>> Handle(GetArranqueMaquinaQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ENV.PA_OBTENER_ARRANQUE_MAQUINA", new { p_EnvasadoraId = request.envasadoraId, p_OrdenId = request.ordenId }, commandType: CommandType.StoredProcedure);

                var arranque = await results.ReadFirstOrDefaultAsync<ArranqueMaquinaViewModel>();

                if (arranque == null)
                {
                    return new StatusResponse<ArranqueMaquinaViewModel> { Ok = false };
                }
                else
                {
                    var observaciones = await results.ReadAsync<ArranqueMaquinaObservacionViewModel>();
                    var condiciones = await results.ReadAsync<ArranqueMaquinaCondPrevCabViewModel>();
                    var variables = await results.ReadAsync<ArranqueMaquinaVarBasCabViewModel>();
                    
                    arranque.Observaciones = observaciones.ToList();
                    arranque.Condiciones = condiciones.ToList();
                    arranque.Variables = variables.ToList();

                }

                return new StatusResponse<ArranqueMaquinaViewModel> { Ok = true, Data = arranque };
            }
        }
    }
}
