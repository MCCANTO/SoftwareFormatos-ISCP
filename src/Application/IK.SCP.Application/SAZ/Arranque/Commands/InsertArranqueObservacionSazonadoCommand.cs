using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Commands
{
    public class InsertArranqueObservacionSazonadoCommand : IRequest<StatusResponse>
    {
        public int ArranqueId { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertArranqueObservacionSazonadoCommandHandler : IRequestHandler<InsertArranqueObservacionSazonadoCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public InsertArranqueObservacionSazonadoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertArranqueObservacionSazonadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueObservacionSazonado(request.ArranqueId, request.Observacion);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_UPDATE_OK, CommandConst.MSJ_UPDATE_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
