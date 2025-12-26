using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllBlendingArranquesQuery : IRequest<StatusResponse>
    {
        public string Orden { get; set; }
    }

    public class GetAllBlendingArranquesQueryHandler : IRequestHandler<GetAllBlendingArranquesQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllBlendingArranquesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllBlendingArranquesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarArranquesBlending(request.Orden);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
