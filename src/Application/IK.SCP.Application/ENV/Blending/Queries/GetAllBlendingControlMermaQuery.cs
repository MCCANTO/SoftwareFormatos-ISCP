using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllBlendingControlMermaQuery : IRequest<StatusResponse>
    {
        public string Orden { get; set; }
    }

    public class GetAllBlendingControlMermaQueryHandler : IRequestHandler<GetAllBlendingControlMermaQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllBlendingControlMermaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllBlendingControlMermaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarControlMermaBlending(request.Orden);
                return StatusResponse.TrueFalse(result != null, QueryConst.MSJ_GET_OK, QueryConst.MSJ_GET_ERROR, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
