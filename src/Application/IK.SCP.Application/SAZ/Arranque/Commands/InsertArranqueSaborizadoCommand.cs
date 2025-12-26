using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Commands
{
    public class InsertArranqueSaborizadoCommand : ArranqueSazonadoCreateDto, IRequest<StatusResponse>
    {
    }

    public class InsertArranqueSaborizadoCommandHandler : IRequestHandler<InsertArranqueSaborizadoCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public InsertArranqueSaborizadoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(InsertArranqueSaborizadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _uow.GuardarArranqueSazonado(request);
                return StatusResponse.TrueFalse(result, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(CommandConst.MSJ_INSERT_ERROR, statusCode: 500);
            }
        }
    }
}
