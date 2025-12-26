using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ACO.Commands
{
    public class InsertArranqueLavadoTuberculoAcondCommand: IRequest<StatusResponse>
    {
        public string OrdenId { get; set; }
    }

    public class InsertArranqueLavadoTuberculoAcondCommandHandler : IRequestHandler<InsertArranqueLavadoTuberculoAcondCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertArranqueLavadoTuberculoAcondCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertArranqueLavadoTuberculoAcondCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.InsertarArranqueLavadoTuberculoAcond(request.OrdenId);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
