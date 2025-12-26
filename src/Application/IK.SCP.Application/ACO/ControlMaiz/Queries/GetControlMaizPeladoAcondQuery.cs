using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetControlMaizPeladoAcondQuery : IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
    }

    public class GetControlMaizPeladoAcondQueryHandler : IRequestHandler<GetControlMaizPeladoAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetControlMaizPeladoAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetControlMaizPeladoAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarControlMaizPeladoAcond(request.OrdenId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
