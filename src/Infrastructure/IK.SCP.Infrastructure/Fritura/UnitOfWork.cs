using Dapper;
using IK.SCP.Domain.Dtos;
using System.Data;
using System.Text.Json;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public async Task<IEnumerable<OrdenFrituraDto>> ListarOrdenXFreidora(int idLinea, string orden = "")
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<OrdenFrituraDto>("FR.LISTAR_ORDEN_FRITURA", new { p_LineaId = idLinea, p_Orden = orden }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> ListarControlAceite(DateTime desde, DateTime hasta, int lineaId, string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Desde = desde,
                    p_Hasta = hasta,
                    p_LineaId = lineaId,
                    p_OrdenId = ordenId
                };

                var result = await cnn.QueryAsync<dynamic>("FR.LISTAR_CONTROL_ACEITE", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarControlAceite(ControlAceiteCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_LineaId = request.LineaId,
                    p_OrdenId  = request.OrdenId,
                    p_SaborId  = request.SaborId,
                    p_OtroSabor  = request.OtroSabor,
                    p_Etapa  = request.Etapa,
                    p_Aceite  = request.Aceite,
                    p_InicioFuente  = request.InicioFuente,
                    p_RellenoFuente  = request.RellenoFuente,
                    p_Agl  = request.Agl,
                    p_Cp  = request.Cp,
                    p_Color  = request.Color,
                    p_Olor  = request.Olor,
                    p_Observacion  = request.Observacion,
                    p_Producto = request.Producto,
                    p_Usuario  = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("FR.INSERTAR_CONTROL_ACEITE", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }



        public async Task<IEnumerable<dynamic>> ListarDefectoCaracterizacion(string articulo)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Articulo = articulo
                };

                var result = await cnn.QueryAsync<dynamic>("FR.LISTAR_DEFECTOS_X_PRODUCTO", parametros, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        
        public async Task<IEnumerable<dynamic>> ListarRegistroCaracterizacion(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = ordenId
                };

                var data = await cnn.QueryAsync<dynamic>("FR.LISTAR_REGISTRO_CARACTERIZACION", parametros, commandType: CommandType.StoredProcedure);

                var result = data
                                .GroupBy(g => new
                                {
                                    g.fechaHora,
                                    g.ordenId,
                                    g.etapa,
                                    g.peso,
                                    g.usuario,
                                    g.inspector,
                                    g.observacion,
                                })
                                .Select(p => new
                                {
                                    p.Key.fechaHora,
                                    p.Key.ordenId,
                                    p.Key.etapa,
                                    p.Key.peso,
                                    p.Key.usuario,
                                    p.Key.inspector,
                                    p.Key.observacion,
                                    defectos = p.Select(x => new
                                    {
                                        id = x.idDefecto,
                                        x.nombreDefecto,
                                        x.nombrePorcentaje,
                                        valor = x.valorDefecto,
                                        porcentaje = x.porcentajeDefecto
                                    }).ToList()
                                });

                return result;
            }
        }

        public async Task<bool> GuardarRegistroCaracterizacion(RegistroCaracterizacionCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId  = request.OrdenId,
                    p_Etapa  = request.Etapa,
                    p_Peso  = request.Peso,
                    p_Inspector  = request.Inspector,
                    p_Observacion  = request.Observacion,
                    p_Defectos  = JsonSerializer.Serialize(request.Defectos),
                    p_Usuario  = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("FR.INSERTAR_REGISTRO_CARACTERIZACION", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }
    }
}
