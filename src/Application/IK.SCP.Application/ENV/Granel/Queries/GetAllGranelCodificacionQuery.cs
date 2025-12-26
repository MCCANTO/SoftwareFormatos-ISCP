using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Helpers;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetAllGranelCodificacionQuery : IRequest<StatusResponse>
    {
        public int EnvasadoraId { get; set; }
        public string Orden { get; set; }
    }

    public class GetAllGranelCodificacionQueryHandler : IRequestHandler<GetAllGranelCodificacionQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllGranelCodificacionQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllGranelCodificacionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _uow.ListarCodificacionGranel(request.EnvasadoraId, request.Orden);


                var result = items.Select(x => new
                {
                    x.Nombre,
                    x.Tamanio,
                    x.TipoArchivo,
                    x.UsuarioCreacion,
                    x.FechaCreacion,
                    Imagen = DataConvertHelper.ToBase64String(x.Ruta),
                    ContentType = DataConvertHelper.GetMimeTypeForFileExtension(x.Ruta),
                });

                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: result);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }
}
