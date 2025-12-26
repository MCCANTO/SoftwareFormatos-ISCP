using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class SaveArranqueMaquinaCommand : ArranqueMaquinaDto, IRequest<StatusResponse<int>>
    {}
    public class SaveArranqueMaquinaCommandHandler : IRequestHandler<SaveArranqueMaquinaCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public SaveArranqueMaquinaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(SaveArranqueMaquinaCommand request, CancellationToken cancellationToken)
        {

            using (var cnn = _uow.Context.CreateConnection)
            {
                cnn.Open();
                try
                {
                    var id = await _uow.GuardarEnvasadoArranqueMaquina(request);
               
                    if (id == 0)
                        return new StatusResponse<int> { Ok = false, Message = "No se pudo guardar la información" };
                    else
                        return new StatusResponse<int> { Ok = true, Data = id, Message = "Información guardada correctamente" };

                }
                catch (Exception ex)
                {
                    return new StatusResponse<int> { Ok = false };
                }
            }
        }
    }
}
