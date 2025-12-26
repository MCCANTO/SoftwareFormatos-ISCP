using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllRegistroCaracterizacionFrituraQuery : IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
    }
    public class GetAllRegistroCaracterizacionFrituraQueryHandler : IRequestHandler<GetAllRegistroCaracterizacionFrituraQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetAllRegistroCaracterizacionFrituraQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(GetAllRegistroCaracterizacionFrituraQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarRegistroCaracterizacion(request.OrdenId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

}
