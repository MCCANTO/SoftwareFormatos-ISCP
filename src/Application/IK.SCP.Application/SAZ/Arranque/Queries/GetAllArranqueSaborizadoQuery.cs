using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.SAZ.Queries
{
    public class GetAllArranqueSaborizadoQuery : IRequest<StatusResponse>
    {
        public int SazonadorId { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Linea { get; set; }
        public string? Producto { get; set; }
        public string? Sabor { get; set; }
    }
    public class GetAllArranqueSaborizadoQueryHandler : IRequestHandler<GetAllArranqueSaborizadoQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetAllArranqueSaborizadoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllArranqueSaborizadoQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var _result = await _uow.ListarArranqueSazonado(request.SazonadorId, request.Fecha, request.Linea, request.Producto ?? "", request.Sabor ?? "");

                var data = _result
                                .GroupBy(g => new { g.ArranqueId, g.Orden, g.SaborId, g.SaborDescripcion, g.Cerrado, g.FechaCreacion })
                                .Select(p => new
                                {
                                    p.Key.ArranqueId,
                                    p.Key.Orden,
                                    p.Key.SaborId,
                                    p.Key.SaborDescripcion,
                                    p.Key.Cerrado,
                                    p.Key.FechaCreacion,
                                    Productos = p.Select(p => new { 
                                        p.Linea,
                                        p.OrdenFR,
                                        p.Producto
                                    }).ToList()
                                }).ToList();

                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: data);
            }
            catch (Exception ex)
            {
                return StatusResponse.False(ex.Message, statusCode: 500);
            }

            //var items = await _context.SAZ_ArranqueProductos
            //                        .Where(f => f.Arranque.SaborizadorId == request.SazonadorId)
            //                        .Include(i => i.Arranque)
            //                        .Select(p => new
            //                        {
            //                            p.ArranqueId,
            //                            Saborizador = p.Arranque.Saborizador.Nombre,
            //                            Sabor = p.Arranque.SaborId,
            //                            SaborDescripcion = p.Arranque.SaborDescripcion,
            //                            Fecha = p.Arranque.FechaCreacion,
            //                            _context.FR_VOrdenFrituras.Where(f => f.Orden == p.Orden).FirstOrDefault().Linea,
            //                            _context.FR_VOrdenFrituras.Where(f => f.Orden == p.Orden).FirstOrDefault().Producto,
            //                        })
            //                        .ToListAsync();

            //return new StatusResponse() { Ok = true, Data = 1 };
        }
    }
}
