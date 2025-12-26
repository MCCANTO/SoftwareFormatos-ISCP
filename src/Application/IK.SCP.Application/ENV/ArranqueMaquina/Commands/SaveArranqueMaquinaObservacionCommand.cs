using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.Queries;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class SaveArranqueMaquinaObservacionCommand : IRequest<StatusResponse>
    {
        public int arranqueMaquinaId { get; set; }
        public string observacion { get; set; }
    }

    public class SaveArranqueMaquinaObservacionCommandHandler : IRequestHandler<SaveArranqueMaquinaObservacionCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public SaveArranqueMaquinaObservacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(SaveArranqueMaquinaObservacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarEnvasadoArranqueMaquinaObservacion(request.arranqueMaquinaId, request.observacion);

                return StatusResponse.TrueFalse(result > 0, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, 500);
            }
        }
    }
}
