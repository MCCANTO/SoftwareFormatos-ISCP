using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class UpdateArranqueLavadoTuberculoCierreAcondCommand : IRequest<StatusResponse>
    {
        public int ArranqueLavadoTuberculoId{ get; set; }
    }
    public class UpdateArranqueLavadoTuberculoCierreAcondCommandHandler : IRequestHandler<UpdateArranqueLavadoTuberculoCierreAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public UpdateArranqueLavadoTuberculoCierreAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(UpdateArranqueLavadoTuberculoCierreAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.CerrarArranqueLavadoTuberculoAcond(request.ArranqueLavadoTuberculoId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
