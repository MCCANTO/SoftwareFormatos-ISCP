using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Queries
{
    public class GetByIdChecklistElectroporadorAcondQuery : IRequest<StatusResponse>
    {
        public int Id { get; set; }
    }
    public class GetByIdChecklistElectroporadorAcondQueryHandler : IRequestHandler<GetByIdChecklistElectroporadorAcondQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetByIdChecklistElectroporadorAcondQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetByIdChecklistElectroporadorAcondQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ObtenerArranqueElectroporadorPorIdAcond(request.Id);
                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.ToString(), statusCode: 500);
            }
        }
    }
}