using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllChecklistQuery : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
    }

    public class GetAllChecklistQueryHandler : IRequestHandler<GetAllChecklistQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllChecklistQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllChecklistQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GetAllChecklistGranel(request.EnvasadoraId, request.Orden);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result.ToList());
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
