using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetByIdArranqueLavadoTuberculoAcondQuery : IRequest<StatusResponse>
    {
        public int ArranqueLavadoTuberculoId { get; set; }
    }

    public class GetByIdArranqueLavadoTuberculoAcondQueryHandler : IRequestHandler<GetByIdArranqueLavadoTuberculoAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetByIdArranqueLavadoTuberculoAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetByIdArranqueLavadoTuberculoAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ObtenerArranqueLavadoTuberculoPorIdAcond(request.ArranqueLavadoTuberculoId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
