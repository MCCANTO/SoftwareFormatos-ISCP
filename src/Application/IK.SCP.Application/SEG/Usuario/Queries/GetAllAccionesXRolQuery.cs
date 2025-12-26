using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SEG.Queries
{
    public class GetAllAccionesXRolQuery : IRequest<StatusResponse>
    {
        public int RolId { get; set; }
    }

    public class GetAllAccionesXRolQueryHandler : IRequestHandler<GetAllAccionesXRolQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllAccionesXRolQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllAccionesXRolQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarAccionXRol(request.RolId);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
            
        }
    }
}
