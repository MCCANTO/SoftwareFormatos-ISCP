using Azure.Core;
using Dapper;
using IK.SCP.Domain.Dtos;
using System.Data;
using System.Text.Json;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public async Task<IEnumerable<SazonadorDto>> ListarSazonadores()
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<SazonadorDto>("SAZ.LISTAR_SAZONADORES", commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<FreidoraDto>> ListarFreidorasXSazonador(int idSazonador)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<FreidoraDto>("SAZ.LISTAR_FREIDORA_X_SAZONADOR", new { p_SazonadorId = idSazonador }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarArranqueSazonado(ArranqueSazonadoCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_SazonadorId = request.SazonadorId,
                    p_Sabor = request.Sabor,
                    p_Otro = request.Otro,
                    p_Ordenes = JsonSerializer.Serialize(request.Ordenes),
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("SAZ.INSERTAR_ARRANQUE_SAZONADO", parametros, commandType: CommandType.StoredProcedure);

                return (result > 0);
            }
        }
        
        public async Task<bool> CerrarArranqueSazonado(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueId = id,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("SAZ.CERRAR_ARRANQUE_SAZONADO", parametros, commandType: CommandType.StoredProcedure);

                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>> ListarArranqueSazonado(int sazonadorId, DateTime? fecha, int? linea, string producto, string sabor)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_SazonadorId = sazonadorId,
                    p_Fecha = fecha,
                    p_Linea = linea,
                    p_Producto = producto,
                    p_Sabor = sabor,
                };
                var result = await cnn.QueryAsync<dynamic>("SAZ.LISTAR_ARRANQUE_SAZONADO", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic?> ObtenerArranqueSazonado(int arranqueId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("SAZ.OBTENER_ARRANQUE_SAZONADO_X_ID", new { p_ArranqueId = arranqueId }, commandType: CommandType.StoredProcedure);

                if (results == null) return null;

                var arranque = results.ReadFirstOrDefault<dynamic>();
                if (arranque == null) return arranque;

                var _productos = results.Read<dynamic>();
                var _condiciones = results.Read<dynamic>();
                var _verificaciones = results.Read<dynamic>();
                var _observaciones = results.Read<dynamic>();

                var data = new
                {
                    arranque.ArranqueId,
                    arranque.Orden,
                    arranque.Fecha,
                    arranque.SaborId,
                    arranque.SaborDescripcion,
                    arranque.Cerrado,
                    arranque.PesoInicio,
                    arranque.PesoFin,
                    arranque.ObservacionInicio,
                    arranque.ObservacionFin,
                    Productos = _productos,
                    Condiciones = _condiciones,
                    Verificaciones = _verificaciones,
                    Observaciones = _observaciones
                };

                return data;
            }
        }


        public async Task<IEnumerable<dynamic>?> ListarArranqueVerificacionSazonado(int id)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new { p_ArranqueVerificacionEquipoId = id };

                var results = await cnn.QueryAsync<dynamic>("SAZ.LISTAR_ARRANQUE_VERIFICACION_EQUIPO_DETALLE", parametros, commandType: CommandType.StoredProcedure);

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
                                        });

                return verificaciones;
            }
        }

        public async Task<bool> GuardarArranqueVerificacionSazonado(ArranqueVerificacionSazonadoCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueVerificacionEquipoId = request.ArranqueVerificacionEquipoId,
                    p_ArranqueId = request.ArranqueId,
                    p_Verificaciones = JsonSerializer.Serialize(request.Verificaciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("SAZ.GUARDAR_ARRANQUE_VERIFICACION_EQUIPO", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }
        
        public async Task<bool> GuardarArranqueVariableSazonado(ArranqueVariableBasicaSazonadoUpdateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueId = request.ArranqueId,
                    p_PesoInicio = request.PesoInicio,
                    p_ObservacionInicio = request.ObservacionInicio,
                    p_PesoFin = request.PesoFin,
                    p_ObservacionFin = request.ObservacionFin,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("SAZ.INSERTAR_ARRANQUE_VARIABLE_BASICA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueCondicionSazonado(ArranqueSazonadoCondicionUpdateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueId = request.ArranqueId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("SAZ.GUARDAR_ARRANQUE_CONDICION_PREVIA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueObservacionSazonado(int arranqueId, string observacion)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueId = arranqueId,
                    p_Observacion = observacion,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("SAZ.GUARDAR_ARRANQUE_OBSERVACION", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }
    }
}
