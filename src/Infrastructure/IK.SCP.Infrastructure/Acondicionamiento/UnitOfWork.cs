using Dapper;
using IK.SCP.Domain.Dtos;
using System.Data;
using System.Text.Json;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {
        #region GENERALES
        public async Task<IEnumerable<dynamic>> ListarMateriaPrimaAcond()
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_MATERIA_PRIMA", commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> ListarProcesoXMateriaPrimaAcond(int materiaPrimaId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_DOCUMENTO_X_MATERIA_PRIMA", new { p_MateriaPrimaId = materiaPrimaId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> ListarOrdenAcond(string orden, DateTime? fechaInicio, DateTime? fechaFin, int materiaPrimaId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden,
                    p_FechaInicio  = fechaInicio,
                    p_FechaFin = fechaFin,
                    p_MateriaPrimaId = materiaPrimaId
                };
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_ORDEN", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic> ObtenerOrdenPorIdAcond(string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = orden,
                };
                var result = await cnn.QueryAsync<dynamic>("ACO.OBTENER_ORDEN_POR_ID", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Tuple<bool, string>> InsertarOrdenAcond(OrdenAcondCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_MateriaPrimaId = request.MateriaPrimaId,
                    p_OrdenId = request.OrdenId,
                    p_FechaEjecucion = request.FechaEjecucion,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<string>("ACO.INSERTAR_ORDEN", parametros, commandType: CommandType.StoredProcedure);
                var datos = result.Split('-');
                return new Tuple<bool, string>(Convert.ToBoolean(datos[0]), datos[1]);
            }
        }

        #endregion GENERALES

        #region CHECKLIST DE ARRANQUE DE MAIZ

        public async Task<IEnumerable<dynamic>> ListarArranqueMaizAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_ARRANQUE_MAIZ", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        
        public async Task<dynamic> ObtenerArranqueMaizPorIdAcond(int arranqueMaizId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_ARRANQUE_MAIZ_X_ID", new { p_ArranqueMaizId = arranqueMaizId }, commandType: CommandType.StoredProcedure);
                
                var arranque = results.ReadFirstOrDefault();

                if (arranque != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var verificaciones = results.Read<dynamic>();
                    var observaciones = results.Read<dynamic>();

                    arranque.Condiciones = condiciones;
                    arranque.Verificaciones = verificaciones;
                    arranque.Observaciones = observaciones;
                }

                return arranque;
            }
        }

        public async Task<dynamic> ObtenerArranqueMaizAbiertoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_ARRANQUE_MAIZ_ABIERTO", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var arranque = results.ReadFirstOrDefault();

                if (arranque != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var verificaciones = results.Read<dynamic>();
                    var observaciones = results.Read<dynamic>();

                    arranque.Condiciones = condiciones;
                    arranque.Verificaciones = verificaciones;
                    arranque.Observaciones = observaciones;
                }

                return arranque;
            }
        }

        public async Task<bool> InsertarArranqueMaizAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = ordenId,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.INSERTAR_ARRANQUE_MAIZ", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<bool> CerrarArranqueMaizAcond(int arranqueMaizId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaizId = arranqueMaizId,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.CERRAR_ARRANQUE_MAIZ", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>?> ListarArranqueMaizVerificacionAcond(int id)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new { p_ArranqueMaizVerificacionEquipoId = id };

                var results = await cnn.QueryAsync<dynamic>("ACO.LISTAR_ARRANQUE_MAIZ_VERIFICACION_EQUIPO_DETALLE", parametros, commandType: CommandType.StoredProcedure);

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

        public async Task<bool> GuardarArranqueMaizVerificacionAcond(ArranqueMaizVerificacionAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaizVerificacionEquipoId = request.ArranqueMaizVerificacionEquipoId,
                    p_ArranqueMaizId = request.ArranqueMaizId,
                    p_Verificaciones = JsonSerializer.Serialize(request.Verificaciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_MAIZ_VERIFICACION_EQUIPO", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueMaizVariableAcond(ArranqueMaizVariableBasicaAcondUpdateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaizId = request.ArranqueMaizId,
                    p_Temperatura = request.Temperatura,
                    p_ObservacionTemperatura = request.ObservacionTemperatura,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_MAIZ_VARIABLE_BASICA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueMaizCondicionAcond(ArranqueMaizCondicionAcondUpdateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaizId = request.ArranqueMaizId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_MAIZ_CONDICION_PREVIA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueMaizObservacionAcond(int arranqueMaizId, string observacion)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaizId = arranqueMaizId,
                    p_Observacion = observacion,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_MAIZ_OBSERVACION", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        #endregion CHECKLIST DE ARRANQUE DE MAIZ

        #region CONTROL DE MAIZ

        public async Task<IEnumerable<dynamic>> ListarControlMaizMateriaPrimaAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_PELADO_MAIZ_MATERIA_PRIMA", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarControlMaizMateriaPrimaAcond(PeladoMaizMateriaPrimaAcondCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = request.OrdenId,
                    p_Calidad = request.Calidad,
                    p_CantidadKg = request.Cantidad,
                    p_Lote = request.Lote,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_PELADO_MAIZ_MATERIA_PRIMA", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>> ListarControlMaizInsumoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_PELADO_MAIZ_INSUMO", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarControlMaizInsumoAcond(PeladoMaizInsumoAcondUpdateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_PeladoMaizInsumoId = request.PeladoMaizInsumoId,
                    p_OrdenId = request.OrdenId,
                    p_Insumo = request.Insumo,
                    p_Unidad = request.Unidad,
                    p_CantidadInicio = request.Inicio,
                    p_CantidadFinal = request.Fin,
                    p_Lote = request.Lote,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_PELADO_MAIZ_INSUMO", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<dynamic> ListarControlMaizPeladoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.LISTAR_PELADO_MAIZ_CONTROL", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var resumen = results.ReadFirstOrDefault();
                var controles = results.Read<dynamic>();

                var data = new
                {
                    resumen, 
                    controles
                };

                return data;
            }
        }

        public async Task<bool> GuardarControlMaizPeladoAcond(PeladoMaizControlAcondUpdateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_PeladoMaizControlId = request.Id,
                    p_OrdenId = request.OrdenId,
                    p_NumeroBatch = request.NumeroBatch,
                    p_TemperaturaInicio = request.TemperaturaInicio,
                    p_TemperaturaFin = request.TemperaturaFin,
                    p_CalKg = request.Cal,
                    p_NumeroTanque = request.NumeroTanque,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_PELADO_MAIZ_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<dynamic> ListarObservacionMaizPeladoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_PELADO_MAIZ_OBSERVACION", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarObservacionMaizPeladoAcond(string ordenId, string observacion)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = ordenId,
                    p_Observacion = observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.INSERTAR_PELADO_MAIZ_OBSERVACION", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<dynamic> ListarControlMaizRemojoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.LISTAR_REMOJO_MAIZ_CONTROL", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);
                
                var resumen = results.Read<dynamic>();
                var controles = results.Read<dynamic>();

                var data = new
                {
                    resumen,
                    controles
                };

                return data;
            }
        }

        public async Task<bool> GuardarControlMaizRemojoAcond(RemojoMaizControlAcondUpdateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_RemojoMaizControlId = request.Id,
                    p_OrdenId = request.OrdenId,
                    p_NumeroTanque = request.NumeroTanque,
                    p_Olor = request.Olor,
                    p_PhAguaInicio = request.PhAntes,
                    p_PhAguaFin = request.PhDespues,
                    p_FechaHoraAgitacionInicio = request.InicioAgitacion,
                    p_FechaHoraAgitacionFin = request.FinAgitacion,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_REMOJO_MAIZ_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<dynamic> ListarControlMaizSancochadoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.LISTAR_SANCOCHADO_MAIZ_CONTROL", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var resumen = results.ReadFirstOrDefault<dynamic>();
                var controles = results.Read<dynamic>();

                var data = new
                {
                    resumen,
                    controles
                };

                return data;
            }
        }

        public async Task<bool> GuardarControlMaizSancochadoAcond(SancochadoMaizControlAcondUpdateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_SancochadoMaizControlId = request.Id,
                    p_OrdenId = request.OrdenId,
                    p_NumeroBatch = request.NumeroBatch,
                    p_NumeroTanque = request.NumeroTanque,
                    p_TemperaturaInicio = request.TemperaturaInicio,
                    p_TemperaturaFin = request.TemperaturaFin,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_SANCOCHADO_MAIZ_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        #endregion CONTROL DE MAIZ

        #region CONTROL REPOSO DE MAIZ

        public async Task<dynamic> ListarControlReposoMaizAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.LISTAR_REPOSO_MAIZ_CONTROL", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var resumen = results.Read<dynamic>();
                var controles = results.Read<dynamic>();

                var data = new
                {
                    resumen,
                    controles
                };

                return data;
            }
        }
        
        public async Task<dynamic> ObtenerDataSancochadoControlReposoMaizAcond(string ordenId, int numeroBatch)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = ordenId,
                    p_NumeroBatch = numeroBatch
                };
                var result = await cnn.QueryAsync<dynamic>("ACO.OBTENER_SANCOCHADO_CONTROL_BATCH", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        
        public async Task<bool> GuardarControlReposoMaizAcond(ReposoMaizControlAcondCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ReposoMaizControlId = request.Id,
                    p_OrdenId = request.OrdenId ,
                    p_NumeroBatch = request.NumeroBatch ,
                    p_CantidadBatch = request.CantidadBatch ,
                    p_FechaHoraInicioReposo = request.FechaHoraInicioReposo ,
                    p_FechaHoraInicioFritura = request.FechaHoraInicioFritura ,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_REPOSO_MAIZ_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        #endregion CONTROL REPOSO DE MAIZ
        
        #region CONTROL REMOJO DE HABAS

        public async Task<dynamic> ListarControlRemojoHabaAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.LISTAR_REMOJO_HABA_CONTROL", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var resumen = results.Read<dynamic>();
                var controles = results.Read<dynamic>();

                var data = new
                {
                    controles
                };

                return data;
            }
        }
                
        public async Task<bool> GuardarControlRemojoHabaAcond(RemojoHabaControlAcondCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_RemojoHabaControlId = request.Id,
                    p_OrdenId = request.OrdenId ,
                    p_NumeroBatch = request.NumeroBatch ,
                    p_CantidadBatch = request.CantidadBatch ,
                    p_FechaHoraInicioReposo = request.FechaHoraInicioReposo ,
                    p_FechaHoraInicioFritura = request.FechaHoraInicioFritura ,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_REMOJO_HABA_CONTROL", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        #endregion CONTROL REMOJO DE HABAS

        #region CHECKLIST DE ARRANQUE DE LAVADO DE TUBERCULO

        public async Task<IEnumerable<dynamic>> ListarArranqueLavadoTuberculoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_ARRANQUE_LAVADO_TUBERCULO", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic?> ObtenerArranqueLavadoTuberculoPorIdAcond(int arranqueLavadoTuberculoId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_ARRANQUE_LAVADO_TUBERCULO_X_ID", new { p_ArranqueLavadoTuberculoId = arranqueLavadoTuberculoId }, commandType: CommandType.StoredProcedure);

                var arranque = results.ReadFirstOrDefault();

                if (arranque != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var verificaciones = results.Read<dynamic>();

                    arranque.Condiciones = condiciones;
                    arranque.Verificaciones = verificaciones;
                }

                return arranque;
            }
        }

        public async Task<dynamic?> ObtenerArranqueLavadoTuberculoAbiertoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_ARRANQUE_LAVADO_TUBERCULO_ABIERTO", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var arranque = results.ReadFirstOrDefault();

                if (arranque != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var verificaciones = results.Read<dynamic>();

                    arranque.Condiciones = condiciones;
                    arranque.Verificaciones = verificaciones;
                }

                return arranque;
            }
        }

        public async Task<bool> InsertarArranqueLavadoTuberculoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = ordenId,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.INSERTAR_ARRANQUE_LAVADO_TUBERCULO", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<bool> CerrarArranqueLavadoTuberculoAcond(int arranqueLavadoTuberculoId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueLavadoTuberculoId = arranqueLavadoTuberculoId,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.CERRAR_ARRANQUE_LAVADO_TUBERCULO", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>?> ListarArranqueLavadoTuberculoVerificacionAcond(int id)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new { p_ArranqueLavadoTuberculoVerificacionEquipoId = id };

                var results = await cnn.QueryAsync<dynamic>("ACO.LISTAR_ARRANQUE_LAVADO_TUBERCULO_VERIFICACION_EQUIPO_DETALLE", parametros, commandType: CommandType.StoredProcedure);

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

        public async Task<bool> GuardarArranqueLavadoTuberculoVerificacionAcond(ArranqueLavadoTuberculoVerificacionAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueLavadoTuberculoVerificacionEquipoId = request.ArranqueLavadoTuberculoVerificacionEquipoId,
                    p_ArranqueLavadoTuberculoId = request.ArranqueLavadoTuberculoId,
                    p_Verificaciones = JsonSerializer.Serialize(request.Verificaciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_LAVADO_TUBERCULO_VERIFICACION_EQUIPO", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueLavadoTuberculoCondicionAcond(ArranqueLavadoTuberculoCondicionAcondUpdateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueLavadoTuberculoId = request.ArranqueLavadoTuberculoId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_LAVADO_TUBERCULO_CONDICION_PREVIA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        #endregion CHECKLIST DE ARRANQUE DE LAVADO DE TUBERCULO

        #region CONTROL RAYOS X

        public async Task<IEnumerable<dynamic>> ListarControlRayosXAcond(string periodo)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_CONTROL_RAYOS_X", new { p_Periodo = periodo }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarControlRayosXAcond(ControlRayosXAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_MateriaPrimaId = request.MateriaPrima,
                    p_DeteccionUno = request.DeteccionUno,
                    p_DeteccionDos = request.DeteccionDos,
                    p_Conformidad = request.Conformidad,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_CONTROL_RAYOS_X", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> RevisarControlRayosXAcond(List<int> ids)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_IdsControl = string.Join(',', ids),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.REVISAR_CONTROL_RAYOS_X", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        #endregion CONTROL RAYOS X

        #region CHECKLIST PEF

        public async Task<IEnumerable<dynamic>> ListarArranqueElectroporadorAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.LISTAR_ARRANQUE_ELECTROPORADOR", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic?> ObtenerArranqueElectroporadorPorIdAcond(int arranqueElectroporadorId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_ARRANQUE_ELECTROPORADOR_X_ID", new { p_ArranqueElectroporadorId = arranqueElectroporadorId }, commandType: CommandType.StoredProcedure);

                var arranque = results.ReadFirstOrDefault();

                if (arranque != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var verificaciones = results.Read<dynamic>();
                    var variables = results.Read<dynamic>();

                    arranque.Condiciones = condiciones;
                    arranque.Verificaciones = verificaciones;
                    arranque.Variables = variables;
                }

                return arranque;
            }
        }

        public async Task<dynamic?> ObtenerArranqueElectroporadorAbiertoAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_ARRANQUE_ELECTROPORADOR_ABIERTO", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var arranque = results.ReadFirstOrDefault();

                if (arranque != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var verificaciones = results.Read<dynamic>();
                    var variables = results.Read<dynamic>();

                    arranque.Condiciones = condiciones;
                    arranque.Verificaciones = verificaciones;
                    arranque.Variables = variables;
                }

                return arranque;
            }
        }

        public async Task<bool> InsertarArranqueElectroporadorAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = ordenId,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.INSERTAR_ARRANQUE_ELECTROPORADOR", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<bool> CerrarArranqueElectroporadorAcond(int arranqueElectroporadorId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueElectroporadorId = arranqueElectroporadorId,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.CERRAR_ARRANQUE_ELECTROPORADOR", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>?> ListarArranqueElectroporadorVerificacionAcond(int id)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new { p_ArranqueElectroporadorVerificacionEquipoId = id };

                var results = await cnn.QueryAsync<dynamic>("ACO.LISTAR_ARRANQUE_ELECTROPORADOR_VERIFICACION_EQUIPO_DETALLE", parametros, commandType: CommandType.StoredProcedure);

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

        public async Task<bool> GuardarArranqueElectroporadorVerificacionAcond(ArranqueElectroporadorVerificacionAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueElectroporadorVerificacionEquipoId = request.ArranqueElectroporadorVerificacionEquipoId,
                    p_ArranqueElectroporadorId = request.ArranqueElectroporadorId,
                    p_Verificaciones = JsonSerializer.Serialize(request.Verificaciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_ELECTROPORADOR_VERIFICACION_EQUIPO", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        public async Task<bool> GuardarArranqueElectroporadorCondicionAcond(ArranqueElectroporadorCondicionBasicaAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueElectroporadorId = request.ArranqueElectroporadorId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_ELECTROPORADOR_CONDICION_PREVIA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }
        
        public async Task<bool> GuardarArranqueElectroporadorVariableBasicaAcond(ArranqueElectroporadorVariableBasicaAcondUpdateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueElectroporadorId = request.ArranqueElectroporadorId,
                    p_Variables = JsonSerializer.Serialize(request.Variables),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_ARRANQUE_ELECTROPORADOR_VARIABLE_BASICA", parametros, commandType: System.Data.CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        #endregion CHECKLIST PEF

        #region CONTROL PEF

        public async Task<dynamic?> ObtenerControlPefAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_CONTROL_TRATAMIENTO", new { p_OrdenId = ordenId }, commandType: CommandType.StoredProcedure);

                var control = results.ReadFirstOrDefault();

                if (control != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var fuerzaCortes = results.Read<dynamic>();
                    var tiempos = results.Read<dynamic>();
                    var detalleFuerzaCortes = results.Read<dynamic>();

                    control.Condiciones = condiciones;
                    
                    if (fuerzaCortes.Any())
                    {
                        foreach (var item in fuerzaCortes)
                        {
                            item.detalle = detalleFuerzaCortes.Where(f => f.controlTratamientoFuerzaCorteId == item.controlTratamientoFuerzaCorteId).ToList();
                        }
                        control.FuerzaCortes = fuerzaCortes;
                    }
                    
                    control.Tiempos = tiempos;

                }

                return control;
            }
        }
        
        public async Task<dynamic?> ObtenerControlPefPorIdAcond(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_CONTROL_TRATAMIENTO_X_ID", new { p_ControlTratamientoId = id }, commandType: CommandType.StoredProcedure);

                var control = results.ReadFirstOrDefault();

                if (control != null)
                {
                    var condiciones = results.Read<dynamic>();
                    var fuerzaCortes = results.Read<dynamic>();
                    var tiempos = results.Read<dynamic>();

                    control.Condiciones = condiciones;
                    control.FuerzaCortes = fuerzaCortes;
                    control.Tiempos = tiempos;
                }

                return control;
            }
        }

        public async Task<bool> InsertarControlPefAcond(string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_OrdenId = ordenId,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.INSERTAR_CONTROL_TRATAMIENTO", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<bool> ActualizarControlPefAcond(ControlPefUpdateAcondDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ControlTratamientoId = request.ControlTratamientoId,
                    p_Proveedor = request.Proveedor,
                    p_Lote = request.Lote,
                    p_Humedad = request.Humedad,
                    p_Brix = request.Brix,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ACO.ACTUALIZAR_CONTROL_TRATAMIENTO", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<dynamic?> ListarControlPefCondicionDetalleAcond(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ACO.OBTENER_CONTROL_TRATAMIENTO_CONDICION_PREVIA", new { p_ControlTratamientoCondicionPreviaId = id }, commandType: CommandType.StoredProcedure);

                var condicion = results.ReadFirstOrDefault();
                var detalles = results.Read<dynamic>();

                var data = new
                {
                    condicion,
                    detalles
                };

                return data;
            }
        }

        public async Task<bool> GuardarControlPefCondicionAcond(ControlPefCondicionBasicaAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ControlTratamientoId = request.ControlTratamientoId,
                    p_TipoId = request.TipoId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_CONTROL_TRATAMIENTO_CONDICION_PREVIA", parametros, commandType: CommandType.StoredProcedure);

                return (id > 0);
            }
        }
        
        public async Task<bool> GuardarControlPefFuerzaCorteAcond(ControlPefFuerzaCorteAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ControlTratamientoId = request.ControlTratamientoId,
                    p_FuerzaCortes = JsonSerializer.Serialize(request.FuerzaCortes),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_CONTROL_TRATAMIENTO_FUERZA_CORTE", parametros, commandType: CommandType.StoredProcedure);

                return (id > 0);
            }
        }
        public async Task<dynamic?> ListarControlPefFuerzaCorteDetalleAcond(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ACO.OBTENER_CONTROL_TRATAMIENTO_FUERZA_CORTE", new { p_ControlTratamientoFuerzaCorteId = id }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }


        public async Task<bool> GuardarControlPefTiempoAcond(ControlPefTiempoAcondCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_ControlTratamientoId = request.ControlTratamientoId,
                    p_NumeroPaleta = request.NumeroPaleta,
                    p_CantidadKg = request.CantidadKg,
                    p_HoraInicioPef = request.HoraInicioPef,
                    p_HoraInicioFritura = request.HoraInicioFritura,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ACO.GUARDAR_CONTROL_TRATAMIENTO_TIEMPO_PEF", parametros, commandType: CommandType.StoredProcedure);

                return (id > 0);
            }
        }

        #endregion CONTROL PEF
    }
}
