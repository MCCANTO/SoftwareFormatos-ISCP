using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetArranqueMaquinaCondicionPreviaQuery : IRequest<StatusResponse>
    {
        public int id { get; set; }
    }
    public class GetArranqueMaquinaCondicionPreviaQueryHandler : IRequestHandler<GetArranqueMaquinaCondicionPreviaQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        
        public GetArranqueMaquinaCondicionPreviaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetArranqueMaquinaCondicionPreviaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ObtenerEnvasadoArranqueMaquinaCondicionPrevia(request.id);

                return StatusResponse.TrueFalse(_result != null, QueryConst.MSJ_GET_OK, QueryConst.MSJ_GET_ERROR, data: _result);

            }
            catch (Exception ex)
            {
                return new StatusResponse { StatusCode = 500, Success = false, Messages = new List<string> { ex.Message } };
            }
        }
    }
}
