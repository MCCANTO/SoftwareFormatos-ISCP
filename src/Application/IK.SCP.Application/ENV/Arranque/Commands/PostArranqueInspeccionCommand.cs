using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueInspeccionCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
        public int CantidadCaja { get; set; }
        public string Etiquetador { get; set; } = "";
        public string Posicion { get; set; } = "";
        public string Inspector { get; set; } = "";
        public string Ruta { get; set; } = "";
    }

    public class PostArranqueInspeccionCommandHandler : IRequestHandler<PostArranqueInspeccionCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public PostArranqueInspeccionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(PostArranqueInspeccionCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_ArranqueId = request.ArranqueId,
                        p_CantidadCaja = request.CantidadCaja,
                        p_Etiquetador = request.Etiquetador,
                        p_Posicion = request.Posicion,
                        p_Inspector = request.Inspector,
                        p_Imagen = request.Ruta,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_INSPECCION", parametros, commandType: System.Data.CommandType.StoredProcedure);

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
