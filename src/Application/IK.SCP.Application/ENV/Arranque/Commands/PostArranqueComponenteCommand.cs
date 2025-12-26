using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueComponenteCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
        public string Componente { get; set; } = "";
        public string Lote { get; set; } = "";
        public decimal Humedad { get; set; }
        public bool EvaluacionSensorial { get; set; }
        public string Observacion { get; set; } = "";
    }

    public class PostArranqueComponenteCommandHandler : IRequestHandler<PostArranqueComponenteCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public PostArranqueComponenteCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(PostArranqueComponenteCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_ArranqueId = request.ArranqueId,
                        p_Componente = request.Componente,
                        p_Lote = request.Lote,
                        p_Humedad = request.Humedad,
                        p_EvaluacionSensorial = request.EvaluacionSensorial,
                        p_Observacion = request.Observacion,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_COMPONENTE", parametros, commandType: System.Data.CommandType.StoredProcedure);

                    return new StatusResponse<int>() { Ok = true, Data = id_arranque };
                }
                catch (Exception ex)
                {
                    return new StatusResponse<int>() { Ok = false };
                }

            }
        }
    }
}
