using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertBlendingObservacionCommand : IRequest<StatusResponse>
    {
        public int BlendingArranqueId { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertBlendingObservacionCommandHandler : IRequestHandler<InsertBlendingObservacionCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertBlendingObservacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertBlendingObservacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueObservacionBlending(request.BlendingArranqueId, request.Observacion);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
