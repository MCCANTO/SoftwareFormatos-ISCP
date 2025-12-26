using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetArranqueMaquinaVariableBasicaQuery : IRequest<StatusResponse>
    {
        public int id { get; set; }
    }

    public class GetArranqueMaquinaVariableBasicaQueryHandler : IRequestHandler<GetArranqueMaquinaVariableBasicaQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetArranqueMaquinaVariableBasicaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetArranqueMaquinaVariableBasicaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ObtenerEnvasadoArranqueMaquinaVariableBasica(request.id);

                return StatusResponse.TrueFalse(result != null, QueryConst.MSJ_GET_OK, QueryConst.MSJ_GET_ERROR, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, 500);
            }
        }
    }
}
