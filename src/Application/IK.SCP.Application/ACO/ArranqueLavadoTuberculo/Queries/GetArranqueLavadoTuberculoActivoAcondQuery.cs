using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetArranqueLavadoTuberculoActivoAcondQuery : IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
    }

    public class GetArranqueLavadoTuberculoActivoAcondQueryHandler : IRequestHandler<GetArranqueLavadoTuberculoActivoAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetArranqueLavadoTuberculoActivoAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetArranqueLavadoTuberculoActivoAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ObtenerArranqueLavadoTuberculoAbiertoAcond(request.OrdenId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
