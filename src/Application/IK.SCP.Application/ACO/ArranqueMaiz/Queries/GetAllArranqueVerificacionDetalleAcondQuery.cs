using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetAllArranqueVerificacionDetalleAcondQuery : IRequest<StatusResponse>
    {
        public int Id { get; set; }
    }
    public class GetAllArranqueVerificacionDetalleAcondQueryHandler : IRequestHandler<GetAllArranqueVerificacionDetalleAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllArranqueVerificacionDetalleAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllArranqueVerificacionDetalleAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarArranqueMaizVerificacionAcond(request.Id);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
