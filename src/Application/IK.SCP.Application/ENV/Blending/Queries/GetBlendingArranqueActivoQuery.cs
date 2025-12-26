using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetBlendingArranqueActivoQuery : IRequest<StatusResponse>
    {
        public string Orden { get; set; }
    }

    public class GetBlendingArranqueActivoQueryHandler : IRequestHandler<GetBlendingArranqueActivoQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetBlendingArranqueActivoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetBlendingArranqueActivoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ObtenerArranqueActivoBlending(request.Orden);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

}
