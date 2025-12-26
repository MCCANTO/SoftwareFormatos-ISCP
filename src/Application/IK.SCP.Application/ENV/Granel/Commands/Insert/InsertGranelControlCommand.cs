using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class InsertGranelControlCommand : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
        public object Parametros { get; set; }
    }

    public class InsertGranelControlCommandHandler : IRequestHandler<InsertGranelControlCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertGranelControlCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertGranelControlCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarControlGranel(request.EnvasadoraId, request.Orden, request.Parametros);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
