using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllBlendingComponentesQuery : IRequest<StatusResponse>
    {
        public string Orden { get; set; }
    }

    public class GetAllBlendingComponentesQueryHandler : IRequestHandler<GetAllBlendingComponentesQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllBlendingComponentesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllBlendingComponentesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarComponentesBlending(request.Orden);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
