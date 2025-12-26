using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class UpdateGranelChecklistDatosCommand : ChecklistGranelUpdateDto, IRequest<StatusResponse>
    {
    }

    public class UpdateGranelChecklistDatosCommandHandler : IRequestHandler<UpdateGranelChecklistDatosCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateGranelChecklistDatosCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateGranelChecklistDatosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.UpdateChecklistGranel(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_UPDATE_OK, CommandConst.MSJ_UPDATE_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
