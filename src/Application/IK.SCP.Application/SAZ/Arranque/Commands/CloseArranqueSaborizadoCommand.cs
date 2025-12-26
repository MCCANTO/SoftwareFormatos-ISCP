using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Commands
{
    public class CloseArranqueSaborizadoCommand : IRequest<StatusResponse>
    {
        public int ArranqueId { get; set; }
    }

    public class CloseArranqueSaborizadoCommandHandler : IRequestHandler<CloseArranqueSaborizadoCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public CloseArranqueSaborizadoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(CloseArranqueSaborizadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.CerrarArranqueSazonado(request.ArranqueId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_UPDATE_OK, CommandConst.MSJ_UPDATE_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
