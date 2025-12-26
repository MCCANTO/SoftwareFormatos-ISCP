using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertGranelChecklistObservacionCommand : IRequest<StatusResponse>
    {
        public int arranqueGranelId { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertGranelChecklistObservacionCommandHandler : IRequestHandler<InsertGranelChecklistObservacionCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertGranelChecklistObservacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertGranelChecklistObservacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarChecklistObservacionGranel(request.arranqueGranelId, request.Observacion);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
