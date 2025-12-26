using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos.ENV;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class SaveArranqueMaquinaCondicionPreviaCommand : ArranqueMaquinaCondicionPreviaCreateDto, IRequest<StatusResponse>
    {
    }
    public class SaveArranqueMaquinaCondicionPreviaCommandHandler : IRequestHandler<SaveArranqueMaquinaCondicionPreviaCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public SaveArranqueMaquinaCondicionPreviaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(SaveArranqueMaquinaCondicionPreviaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.GuardarEnvasadoArranqueMaquinaCondicionPrevia(request);

                return StatusResponse.TrueFalse(_result > 0, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR, data: _result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message);
            }
        }
    }
}
