using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class UpdateBlendingControlComponenteCommand : IRequest<StatusResponse>
    {
        public string Orden { get; set; }
        public List<ControlComponenteBlendingUpdateDto> Componentes { get; set; }
    }

    public class UpdateBlendingControlComponenteCommandHandler : IRequestHandler<UpdateBlendingControlComponenteCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateBlendingControlComponenteCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateBlendingControlComponenteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.ActualizarControlComponentesBlending(request.Orden, request.Componentes);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_UPDATE_OK, CommandConst.MSJ_UPDATE_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
