using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetAllOrdenAcondQuery : IRequest<StatusResponse>
    {
        public string? OrdenId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int MateriaPrimaId { get; set; }
    }

    public class GetAllOrdenAcondQueryHandler : IRequestHandler<GetAllOrdenAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllOrdenAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllOrdenAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarOrdenAcond(request.OrdenId, request.FechaInicio, request.FechaFin, request.MateriaPrimaId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
