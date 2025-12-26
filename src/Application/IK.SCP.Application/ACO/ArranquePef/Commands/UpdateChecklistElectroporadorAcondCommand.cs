using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class UpdateChecklistElectroporadorAcondCommand : IRequest<StatusResponse>
    {
        public int ArranqueElectroporadorId { get; set; }
    }
    public class UpdateChecklistElectroporadorAcondCommandHandler : IRequestHandler<UpdateChecklistElectroporadorAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateChecklistElectroporadorAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateChecklistElectroporadorAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.CerrarArranqueElectroporadorAcond(request.ArranqueElectroporadorId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}