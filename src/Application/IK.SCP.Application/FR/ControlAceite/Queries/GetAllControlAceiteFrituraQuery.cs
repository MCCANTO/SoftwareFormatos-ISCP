using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllControlAceiteFrituraQuery : IRequest<StatusResponse>
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int LineaId { get; set; }
        public string? OrdenId { get; set; }
    }

    public class GetAllControlAceiteFrituraQueryHandler : IRequestHandler<GetAllControlAceiteFrituraQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetAllControlAceiteFrituraQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(GetAllControlAceiteFrituraQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarControlAceite(request.Desde, request.Hasta, request.LineaId, request.OrdenId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.ToString(), statusCode: 500);
            }
        }
    }
}
