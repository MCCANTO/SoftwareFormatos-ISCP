using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertGranelControlObservacionCommand : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertGranelControlObservacionCommandHandler : IRequestHandler<InsertGranelControlObservacionCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertGranelControlObservacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertGranelControlObservacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _uow.GuardarObservacionControlGranel(request.EnvasadoraId, request.Orden, request.Observacion);

                return StatusResponse.TrueFalse(res, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                var _response = StatusResponse.False(CommandConst.MSJ_INSERT_ERROR);
                _response.AddMessage(ex.Message);
                return _response;
            }
        }
    }
}
