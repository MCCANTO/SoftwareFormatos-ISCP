using Dapper;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.FR.Queries
{
    public class GetAllVerificacionEquipoDetalleQuery : IRequest<StatusResponse>
    {
        //public int tipoId { get; set; }
        public int linea { get; set; }
        public int arranqueMaquinaVerificacionEquipoCabId { get; set; }
    }

    public class GetAllVerificacionEquipoDetalleQueryHandler : IRequestHandler<GetAllVerificacionEquipoDetalleQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetAllVerificacionEquipoDetalleQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetAllVerificacionEquipoDetalleQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                var parametros = new { /*p_TipoId = request.tipoId,*/ p_Linea = request.linea, p_ArranqueMaquinaVerificacionEquipoCabId = request.arranqueMaquinaVerificacionEquipoCabId };

                var results = await cnn.QueryAsync<dynamic>("FR.LISTAR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO_DETALLE", parametros, commandType: CommandType.StoredProcedure);
               
                if (results == null) return null;

                var data = results.ToList();

                var verificaciones = data
                                        .GroupBy(g => new { g.Orden_1, g.Nombre_1 })
                                        .Select(x => new
                                        {
                                            padre = x.Key.Orden_1.ToString() + ". " + x.Key.Nombre_1,
                                            detalle = x.Select(y => new
                                            {
                                                id = y.Id,
                                                verificacionEquipoId = y.VerificacionEquipoId,
                                                nombre = $"{y.Orden_2}.- {y.Nombre_2}",
                                                detalle = $"{y.Orden_3}.- {y.Nombre_3}",
                                                operativo = y.Operativo,
                                                limpio = y.Limpio,
                                                observacion = y.Observacion,
                                                orden = y.Orden_2,
                                                cerrado = y.Cerrado
                                            }).ToList()
                                            //x.GroupBy(g => new { g.Orden_2, g.Nombre_2 })
                                            // .Select(y => new
                                            // {
                                            //     padre = y.Key.Orden_2.ToString() + ". " + y.Key.Nombre_2,
                                            //     detalle = y.Select(z => new {
                                            //         id = z.Id,
                                            //         verificacionEquipoId = z.VerificacionEquipoId,
                                            //         nombre = $"{z.Orden_2}.- {z.Nombre_2}",
                                            //         detalle = z.Nombre_3,
                                            //         operativo = z.Operativo,
                                            //         limpio = z.Limpio,
                                            //         observacion = z.Observacion,
                                            //         orden = z.Orden_2,
                                            //         cerrado = z.Cerrado
                                            //     }).ToList()
                                            // }).ToList()
                                        }).ToList();

                //var response = new List<VerificacionEquipoResponse>();

                                        //foreach (var padre in data.Select(p => new { p.Orden_1, p.Nombre_1 }).Distinct().OrderBy(o => o.Orden_1).ToList())
                                        //{
                                        //    var categoria = new VerificacionEquipoResponse() { Categoria = $"{padre.Orden_1}.- {padre.Nombre_1}" };

                                        //    var variableDetalles = data.Where(f => f.Nombre_1 == padre.Nombre_1).Select(p => new VerificacionEquipoDetalle
                                        //    {
                                        //        Id = p.Id,
                                        //        VerificacionEquipoId = p.VerificacionEquipoId,
                                        //        Nombre = $"{p.Orden_2}.- {p.Nombre_2}",
                                        //        Detalle = p.Nombre_3,
                                        //        Operativo = p.Operativo,
                                        //        Limpio = p.Limpio,
                                        //        Observacion = p.Observacion,
                                        //        Orden = p.Orden_2,
                                        //        Cerrado = p.Cerrado
                                        //    }).OrderBy(o => o.Orden).ToList();
                                        //    categoria.Verificaciones = variableDetalles;

                                        //    response.Add(categoria);
                                        //}

                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: verificaciones);
            }
        }
    }
}

