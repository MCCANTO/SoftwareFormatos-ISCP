using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Commands
{
    public class InsertArranqueVerificacionSazonadoCommand : ArranqueVerificacionSazonadoCreateDto, IRequest<StatusResponse>
    {
    }

    public class InsertArranqueVerificacionSazonadoCommandHandler : IRequestHandler<InsertArranqueVerificacionSazonadoCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertArranqueVerificacionSazonadoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertArranqueVerificacionSazonadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueVerificacionSazonado(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
