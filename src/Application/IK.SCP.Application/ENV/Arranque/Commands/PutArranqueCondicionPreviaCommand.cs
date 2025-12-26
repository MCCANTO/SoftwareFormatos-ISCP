using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Commands
{
    public class PutArranqueCondicionPreviaCommand : IRequest<StatusResponse<int>>
    {
        public List<ArranqueCondicionBasicaRequest> condiciones { get; set; }
    }

    public class PutArranqueCondicionPreviaCommandHandler : IRequestHandler<PutArranqueCondicionPreviaCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public PutArranqueCondicionPreviaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse<int>> Handle(PutArranqueCondicionPreviaCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    foreach (var cond in request.condiciones)
                    {

                        var parametros = new
                        {
                            p_ArranqueCondicionPreviaId = cond.ArranqueCondicionPreviaId,
                            p_Valor = cond.Valor,
                            p_UsuarioModificacion = _uow.UserName
                        };

                        var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.ACTUALIZAR_ARRANQUE_CONDICION_PREVIA", parametros, commandType: CommandType.StoredProcedure);

                    }

                    return new StatusResponse<int> { Ok = true, Data = 1 };
                }
                catch
                {
                    return new StatusResponse<int> { Ok = false, Message = "Error al actualizar condición previa.", Data = 0 };
                }
            }
        }
    }
}
