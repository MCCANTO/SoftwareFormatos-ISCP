using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetGranelCondicionProcesoDetalleQuery : IRequest<StatusResponse>
    {
        public int Id { get; set; }
    }
    public class GetGranelCondicionProcesoDetalleQueryHandler : IRequestHandler<GetGranelCondicionProcesoDetalleQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetGranelCondicionProcesoDetalleQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetGranelCondicionProcesoDetalleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _uow.ListarCondicionProcesoGranelDetalle(request.Id);

                var _condiciones = _result.GroupBy(g => g.Padre)
                                            .Select(x => new
                                            {
                                                padre = x.Key,
                                                items = x.Select(y => new
                                                {
                                                    id = y.Id,
                                                    condicionProcesoId = y.CondicionProcesoId,
                                                    nombre = y.Nombre,
                                                    comentario = y.Comentario,
                                                    valor = y.Valor,
                                                    observacion = y.Observacion,
                                                    cerrado = y.Cerrado,
                                                    primerOrden = y.PrimerOrden,
                                                    segundoOrden = y.SegundoOrden
                                                }).ToList()
                                            }).ToList();

                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: _condiciones);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }
        }
    }

}
