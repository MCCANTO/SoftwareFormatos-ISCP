using Dapper;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;
using System.Text.Json;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueVariableBasicaCommand : IRequest<StatusResponse>
    {
        public int ArranqueVariableBasicaCabId { get; set; }
        public int ArranqueId { get; set; }
        public int TipoId { get; set; }
        public string Maquinista { get; set; }
        public List<ArranqueVariableRequest> Variables { get; set; }
    }

    public class PostArranqueVariableBasicaCommandHandler : IRequestHandler<PostArranqueVariableBasicaCommand, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public PostArranqueVariableBasicaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse> Handle(PostArranqueVariableBasicaCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {

                    var parametros = new
                    {
                        p_ArranqueVariableBasicaCabId = request.ArranqueVariableBasicaCabId,
                        p_ArranqueId = request.ArranqueId,
                        p_TipoId = request.TipoId,
                        p_Maquinista = request.Maquinista,
                        p_Variables = JsonSerializer.Serialize(request.Variables),
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_VARIABLE_BASICA", parametros, commandType: CommandType.StoredProcedure);

                    return StatusResponse.TrueFalse(id_arranque > 0, CommandConst.MSJ_INSERT_OK, CommandConst.MSJ_INSERT_ERROR);
                }
                catch (Exception ex)
                {
                    return StatusResponse.False(ex.Message, 500);
                }

            }
        }
    }
}
