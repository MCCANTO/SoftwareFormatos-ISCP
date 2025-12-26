using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetControlTratamientoPefAcondQuery: IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
    }

    public class GetControlTratamientoPefAcondQueryHandler : IRequestHandler<GetControlTratamientoPefAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetControlTratamientoPefAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetControlTratamientoPefAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ObtenerControlPefAcond(request.OrdenId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.ToString(), statusCode: 500);
            }
        }
    }
}
