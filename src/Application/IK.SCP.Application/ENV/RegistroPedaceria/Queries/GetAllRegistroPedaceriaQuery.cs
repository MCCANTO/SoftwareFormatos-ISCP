using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllRegistroPedaceriaQuery : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string OrdenId { get; set; }
    }

    public class GetAllRegistroPedaceriaQueryHandler : IRequestHandler<GetAllRegistroPedaceriaQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetAllRegistroPedaceriaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllRegistroPedaceriaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ListarEnvasadoRegistroPedaceria(request.EnvasadoraId, request.OrdenId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.ToString(), statusCode: 500);
            }
        }
    }
}

