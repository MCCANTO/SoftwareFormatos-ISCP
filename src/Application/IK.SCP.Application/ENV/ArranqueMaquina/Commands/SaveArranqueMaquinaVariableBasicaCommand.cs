using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos.ENV;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class SaveArranqueMaquinaVariableBasicaCommand : ArranqueMaquinaVariableBasicaCreateDto, IRequest<StatusResponse>
    {
    }

    public class SaveArranqueMaquinaVariableBasicaCommandHandler : IRequestHandler<SaveArranqueMaquinaVariableBasicaCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public SaveArranqueMaquinaVariableBasicaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(SaveArranqueMaquinaVariableBasicaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarEnvasadoArranqueMaquinaVariableBasica(request);

                return StatusResponse.TrueFalse(result > 0, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, 500);
            }
        }
    }
}
