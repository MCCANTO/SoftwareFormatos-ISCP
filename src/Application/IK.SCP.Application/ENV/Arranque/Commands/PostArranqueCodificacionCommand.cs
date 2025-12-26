using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranqueCodificacionCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
        public string TipoCodificacion { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public decimal Tamanio { get; set; }
        public string TipoArchivo { get; set; }
    }

    public class PostArranqueCodificacionCommandHandler : IRequestHandler<PostArranqueCodificacionCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public PostArranqueCodificacionCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse<int>> Handle(PostArranqueCodificacionCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {

                    var parametros = new
                    {
                        p_ArranqueId = request.ArranqueId,
                        p_TipoCodificacion = request.TipoCodificacion,
                        p_Nombre = request.Nombre,
                        p_Ruta = request.Ruta,
                        p_Tamanio = request.Tamanio,
                        p_TipoArchivo = request.TipoArchivo,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_CODIFICACION", parametros, commandType: CommandType.StoredProcedure);

                    return new StatusResponse<int> { Ok = true, Data = id_arranque };
                }
                catch
                {
                    return new StatusResponse<int> { Ok = false, Message = "Error al registrar arranque de codificación." };
                }
            }
        }
    }
}
