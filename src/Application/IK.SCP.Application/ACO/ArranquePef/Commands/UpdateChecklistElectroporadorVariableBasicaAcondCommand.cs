using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class UpdateChecklistElectroporadorVariableBasicaAcondCommand : ArranqueElectroporadorVariableBasicaAcondUpdateDto, IRequest<StatusResponse>
    {
    }

    public class UpdateChecklistElectroporadorVariableBasicaAcondCommandHandler : IRequestHandler<UpdateChecklistElectroporadorVariableBasicaAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateChecklistElectroporadorVariableBasicaAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateChecklistElectroporadorVariableBasicaAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueElectroporadorVariableBasicaAcond(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}