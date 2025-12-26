using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class UpdateOrdenCierreAcondCommand : IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
    }
    public class UpdateOrdenCierreAcondCommandHandler : IRequestHandler<UpdateOrdenCierreAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public async Task<StatusResponse> Handle(UpdateOrdenCierreAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var result = await _uow.CerrarOrdenAcond(request.OrdenId);
                return StatusResponse.TrueFalse(true, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
