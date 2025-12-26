using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetByIdBlendingArranqueQuery : IRequest<StatusResponse>
    {
        public int Id { get; set; }
    }
    public class GetByIdBlendingArranqueQueryHandler : IRequestHandler<GetByIdBlendingArranqueQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetByIdBlendingArranqueQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetByIdBlendingArranqueQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ObtenerArranquePorIdBlending(request.Id);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
