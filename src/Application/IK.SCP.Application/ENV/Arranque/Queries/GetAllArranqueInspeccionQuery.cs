using Dapper;
using IK.SCP.Application.Common.Helpers;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllArranqueInspeccionQuery : IRequest<StatusResponse<object>>
    {
        public int ArranqueId { get; set; }
    }

    public class GetAllArranqueInspeccionQueryHandler : IRequestHandler<GetAllArranqueInspeccionQuery, StatusResponse<object>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllArranqueInspeccionQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse<object>> Handle(GetAllArranqueInspeccionQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var items = await cnn.QueryAsync<dynamic>("ENV.LISTAR_ARRANQUE_INSPECCION", new { p_ArranqueId = request.ArranqueId }, commandType: CommandType.StoredProcedure);

                var data = items.Select(x => new
                {
                    x.ArranqueInspeccionId,
                    x.ArranqueId,
                    x.CantidadCaja,
                    x.Etiquetador,
                    x.Posicion,
                    x.Inspector,
                    Imagen = DataConvertHelper.ToBase64String(x.Imagen),
                    ContentType = DataConvertHelper.GetMimeTypeForFileExtension(x.Imagen),
                    x.UsuarioCreacion,
                    x.FechaCreacion
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
