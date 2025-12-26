using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class UpdateGranelEspecificacionesCommand : IRequest<StatusResponse>
    {
        public List<EspecificacionGranelUpdateDto> Especificaciones { get; set; }
    }

    public class UpdateGranelEspecificacionesCommandHandler : IRequestHandler<UpdateGranelEspecificacionesCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateGranelEspecificacionesCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateGranelEspecificacionesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarEspecificacionesGranel(request.Especificaciones);
                return StatusResponse.True(CommandConst.MSJ_UPDATE_OK);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

    //GuardarEspecificacionesGranel
}
