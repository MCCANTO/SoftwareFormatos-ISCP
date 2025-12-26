using Azure.Core;
using Dapper;
using IK.SCP.Domain.Dtos;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {
        #region CHECKLIST

        public async Task<bool> ValidarArticuloMezclaBlending(string articulo)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Articulo = articulo
                };

                var result = await cnn.QueryFirstOrDefaultAsync<int>("ENV.BLENDING_VALIDAR_ARTICULO_MEZCLA", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>> ListarComponentesBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Orden = orden
                };

                var result = await cnn.QueryAsync<dynamic>("ENV.BLENDING_LISTAR_COMPONENTES_EXACTUS", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> ListarArranquesBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Orden = orden
                };

                var result = await cnn.QueryAsync<dynamic>("ENV.BLENDING_LISTAR_ARRANQUE", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic> ObtenerArranqueActivoBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Orden = orden
                };

                var results = await cnn.QueryMultipleAsync("ENV.BLENDING_OBTENER_ARRANQUE_ACTIVO", parametros, commandType: CommandType.StoredProcedure);
                return this.ConstruirArranqueBlending(results);
            }
        }
        
        public async Task<dynamic> ObtenerArranquePorIdBlending(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Id = id,
                };

                var results = await cnn.QueryMultipleAsync("ENV.BLENDING_OBTENER_ARRANQUE_X_ID", parametros, commandType: CommandType.StoredProcedure);
                return this.ConstruirArranqueBlending(results);
            }
        }
        
        public async Task<bool> GuardarArranqueBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_INSERTAR_ARRANQUE", parametros, commandType: CommandType.StoredProcedure);

                return (result > 0);
            }
        }
        
        public async Task<bool> CerrarArranqueBlending(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueId = id,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_CERRAR_ARRANQUE", parametros, commandType: CommandType.StoredProcedure);

                return (result > 0);
            }
        }

        private dynamic ConstruirArranqueBlending(dynamic results)
        {
            if (results == null) return null;

            var arranque = results.ReadFirstOrDefault<dynamic>();
            if (arranque == null) return arranque;

            var _componentes = results.Read<dynamic>();
            var _condiciones = results.Read<dynamic>();
            var _verificaciones = results.Read<dynamic>();
            var _observaciones = results.Read<dynamic>();

            var data = new
            {
                arranque.BlendingArranqueId,
                arranque.Orden,
                arranque.Cerrado,
                Componentes = _componentes,
                Condiciones = _condiciones,
                Verificaciones = _verificaciones,
                Observaciones = _observaciones
            };

            return data;
        }


        public async Task<IEnumerable<dynamic>?> ListarArranqueVerificacionBlending(int id)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new { p_ArranqueVerificacionEquipoId = id };

                var results = await cnn.QueryAsync<dynamic>("ENV.BLENDING_LISTAR_ARRANQUE_VERIFICACION_EQUIPO_DETALLE", parametros, commandType: CommandType.StoredProcedure);

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

        public async Task<bool> GuardarArranqueVerificacionBlending(ArranqueVerificacionBlendingCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueVerificacionEquipoId = request.ArranqueVerificacionEquipoId,
                    p_ArranqueId = request.BlendingArranqueId,
                    p_Verificaciones = JsonSerializer.Serialize(request.Verificaciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_GUARDAR_ARRANQUE_VERIFICACION_EQUIPO", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueCondicionBlending(ArranqueCondicionBlendingUpdateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_BlendingArranqueId = request.BlendingArranqueId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_GUARDAR_ARRANQUE_CONDICION_PREVIA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueObservacionBlending(int arranqueId, string observacion)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueId = arranqueId,
                    p_Observacion = observacion,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_GUARDAR_ARRANQUE_OBSERVACION", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        #endregion CHECKLIST

        #region CONTROL


        public async Task<IEnumerable<dynamic>> ListarControlComponentesBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden
                };

                var result = await cnn.QueryAsync<dynamic>("ENV.BLENDING_LISTAR_COMPONENTES_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        
        public async Task<bool> InsertarControlComponentesBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_INSERTAR_COMPONENTES_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }
        
        public async Task<bool> ActualizarControlComponentesBlending(string orden, List<ControlComponenteBlendingUpdateDto> componentes)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden,
                    p_Componentes = JsonSerializer.Serialize(componentes),
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_ACTUALIZAR_COMPONENTES_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }
        
        public async Task<dynamic> ListarControlBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden
                };

                var results = await cnn.QueryMultipleAsync("ENV.BLENDING_LISTAR_CONTROL", parametros, commandType: CommandType.StoredProcedure);

                var componentes = results.Read<dynamic>();
                var controles = results.Read<dynamic>();

                var columnas = componentes.Select(p => new { field = p.Articulo, header = $"{p.Articulo} - {p.Descripcion}" }).ToList();

                var data = new
                {
                    columnas,
                    controles
                };

                return data;
            }
        }

        public async Task<bool> InsertarControlBlending(string orden, string observacion, List<ControlComponenteBlendingDto> componentes)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden,
                    p_Componentes = JsonSerializer.Serialize(componentes),
                    p_Observacion = observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_INSERTAR_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>> ListarControlMermaBlending(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden
                };

                var result = await cnn.QueryAsync<dynamic>("ENV.BLENDING_LISTAR_MERMA_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> ActualizarControlMermaBlending(string orden, List<ControlMermaBlendingUpdateDto> componentes)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden,
                    p_Componentes = JsonSerializer.Serialize(componentes),
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.BLENDING_ACTUALIZAR_MERMA_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        #endregion CONTROL
    }
}
