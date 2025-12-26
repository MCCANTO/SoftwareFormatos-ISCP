using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.Common.Helpers;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllArranqueCodificacionQuery : IRequest<StatusResponse<object>>
    {
        public int ArranqueId { get; set; }
        public string TipoCodificacion { get; set; }
    }

    public class GetAllArranqueCodificacionQueryHandler : IRequestHandler<GetAllArranqueCodificacionQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllArranqueCodificacionQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse<object>> Handle(GetAllArranqueCodificacionQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<dynamic>("ENV.LISTAR_ARRANQUE_CODIFICACION", 
                            new { p_ArranqueId = request.ArranqueId, p_TipoCodificacion = request.TipoCodificacion }, 
                            commandType: CommandType.StoredProcedure);

                var data = items.Select(x => new
                {
                    x.ArranqueId,
                    x.Nombre,
                    x.Tamanio,
                    x.TipoArchivo,
                    x.UsuarioCreacion,
                    x.FechaCreacion,
                    Imagen = DataConvertHelper.ToBase64String(x.Ruta),
                    ContentType = DataConvertHelper.GetMimeTypeForFileExtension(x.Ruta),
                });

                return new StatusResponse<object>()
                {
                    Ok = true,
                    Data = data.ToList()
                };
            }
        }
    }
}
