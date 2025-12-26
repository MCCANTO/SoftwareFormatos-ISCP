using Dapper;
using IK.SCP.Domain.Dtos;
using System.Data;
using System.Text.Json;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {

        #region CHECKLIST GRANEL

        public async Task<IEnumerable<GetAllChecklistGranelDto>> GetAllChecklistGranel(int envasadoraId, string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden
                };

                var result = await cnn.QueryAsync<GetAllChecklistGranelDto>("ENV.LISTAR_GRANEL_CHECKLIST", parametros, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        
        public async Task<int> CreateChecklistGranel(int envasadoraId, string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_GRANEL_CHECKLIST", parametros, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        
        public async Task<bool> UpdateChecklistGranel(ChecklistGranelUpdateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelId = request.ArranqueGranelId,
                    p_TipoId = request.TipoId,
                    p_FechaVencimiento = request.FechaVencimiento,
                    p_LineaFrituraId = request.LineaFrituraId,
                    p_Maquinista = request.Maquinista,
                    p_Selladora = request.Selladora,
                    p_PersonalPesa = request.PersonalPesa,
                    p_PersonalSella = request.PersonalSella,
                    p_Usuario = this.UserName,
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.ACTUALIZAR_GRANEL_CHECKLIST", parametros, commandType: CommandType.StoredProcedure);

                return (result > 0);
            }
        }

        public async Task<bool> CloseChecklistGranel(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelId = id,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.CERRAR_GRANEL_CHECKLIST", parametros, commandType: CommandType.StoredProcedure);

                return (result > 0);
            }
        }

        public async Task<ChecklistGranelDto?> ObtenerChecklistGranelPorId(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelId = id
                };

                var results = await cnn.QueryMultipleAsync("ENV.OBTENER_GRANEL_CHECKLIST_X_ID", parametros, commandType: CommandType.StoredProcedure);

                if (results == null) return null;

                var arranque = results.ReadFirstOrDefault<ChecklistGranelDto>();
                if (arranque == null) return arranque;

                var especificaciones = results.Read<EspecificacionChecklistGranelDto>();
                var condicionesOperativas = results.Read<CondicionOperativaChecklistGranelDto>();
                var condicionesProceso = results.Read<CondicionProcesoChecklistGranelDto>();
                var observaciones = results.Read<ObservacionChecklistGranelDto>();
                var revisiones = results.Read<dynamic>();

                arranque.Especificaciones = especificaciones.ToList();
                arranque.CondicionesOperativas = condicionesOperativas.ToList();
                arranque.CondicionesProceso = condicionesProceso.ToList();
                arranque.Observaciones = observaciones.ToList();
                arranque.Revisiones = revisiones.ToList();
                return arranque;

            }
        }
        
        public async Task<ChecklistGranelDto?> ObtenerChecklistGranel(int envasadoraId, string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden
                };

                var results = await cnn.QueryMultipleAsync("ENV.OBTENER_GRANEL_CHECKLIST", parametros, commandType: CommandType.StoredProcedure);

                if (results == null) return null;

                var arranque = results.ReadFirstOrDefault<ChecklistGranelDto>();
                if (arranque == null) return arranque;

                var especificaciones = results.Read<EspecificacionChecklistGranelDto>();
                var condicionesOperativas = results.Read<CondicionOperativaChecklistGranelDto>();
                var condicionesProceso = results.Read<CondicionProcesoChecklistGranelDto>();
                var observaciones = results.Read<ObservacionChecklistGranelDto>();
                var revisiones = results.Read<dynamic>();

                arranque.Especificaciones = especificaciones.ToList();
                arranque.CondicionesOperativas = condicionesOperativas.ToList();
                arranque.CondicionesProceso = condicionesProceso.ToList();
                arranque.Observaciones = observaciones.ToList();
                arranque.Revisiones = revisiones.ToList();
                return arranque;

            }
        }


        public async Task<IEnumerable<EspecificacionGranelDto>> ListarChecklistGranelEspecificaciones(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelId = id
                };

                var results = await cnn.QueryAsync<dynamic>("ENV.LISTAR_ARRANQUE_GRANEL_ESPECIFICACIONES", parametros, commandType: CommandType.StoredProcedure);

                var data = results
                                .GroupBy(g => new { g.Id, g.EspecificacionId, g.Nombre, g.Valor, g.Otro })
                                .Select(p => new EspecificacionGranelDto()
                                {
                                    Id = p.Key.Id,
                                    EspecificacionId = p.Key.EspecificacionId,
                                    Nombre = p.Key.Nombre,
                                    Valor = p.Key.Valor,
                                    Otro = p.Key.Otro,
                                    Valores = p.Select(p => new EspecificacionGranelDetalleDto()
                                    {
                                        Id = p.ParametroGeneralId,
                                        Nombre = p.NombreParametro
                                    }).ToList()
                                });

                return data;

            }
        }

        public async Task<bool> GuardarEspecificacionesGranel(List<EspecificacionGranelUpdateDto> especificaciones)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_Especificaciones = JsonSerializer.Serialize(especificaciones),
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_GRANEL_ESPECIFICACION", parametros, commandType: CommandType.StoredProcedure);

                return (result != 0);
            }
        }
        
        public async Task<CondicionOperativaGranelDto?> ListarCondicionOperativaGranelDetalle(int arranqueGranelCondicionOperativaId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ENV.LISTAR_GRANEL_CONDICION_OPERATIVA_DETALLE", new { p_ArranqueGranelCondicionOperativaId = arranqueGranelCondicionOperativaId }, commandType: CommandType.StoredProcedure);

                var _arranque = results.ReadFirstOrDefault<CondicionOperativaGranelDto>();
                if (_arranque == null) _arranque = new CondicionOperativaGranelDto();

                var _condiciones = results.Read<CondicionOperativaGranelDetalleDto>();
                if (_condiciones.Any()) _arranque.Detalle = _condiciones.ToList();

                return _arranque;
            }
        }

        public async Task<bool> GuardarCondicionOperativaGranel(CondicionOperativaGranelCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelId = request.ArranqueGranelId,
                    p_TipoId = request.TipoId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_GRANEL_CONDICION_OPERATIVA", parametros, commandType: CommandType.StoredProcedure);

                return (id != 0);
            }
        }


        public async Task<IEnumerable<CondicionProcesoGranelDto>> ListarCondicionProcesoGranelDetalle(int arranqueGranelCondicionProcesoId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var results = await cnn.QueryAsync<CondicionProcesoGranelDto>("ENV.LISTAR_GRANEL_CONDICION_PROCESO", new { p_ArranqueGranelCondicionProcesoId = arranqueGranelCondicionProcesoId }, commandType: CommandType.StoredProcedure);

                return results;
            }
        }

        public async Task<bool> GuardarCondicionProcesoGranel(CondicionProcesoGranelCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelCondicionProcesoId = request.ArranqueGranelCondicionProcesoId,
                    p_ArranqueGranelId = request.ArranqueGranelId,
                    p_Condiciones = JsonSerializer.Serialize(request.Condiciones),
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_GRANEL_CONDICION_PROCESO", parametros, commandType: CommandType.StoredProcedure);

                return (id != 0);
            }
        }


        public async Task<bool> GuardarChecklistObservacionGranel(int arranqueGranelId, string observacion)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelId = arranqueGranelId,
                    p_Observacion = observacion,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_GRANEL_OBSERVACION", parametros, commandType: CommandType.StoredProcedure);

                return (id != 0);
            }
        }
        

        public async Task<bool> GuardarChecklistRevisionGranel(int arranqueGranelId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueGranelId = arranqueGranelId,
                    p_Usuario = this.UserName
                };

                var id = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_ARRANQUE_GRANEL_REVISION", parametros, commandType: CommandType.StoredProcedure);

                return (id != 0);
            }
        }

        #endregion CHECKLIST GRANEL

        #region CONTROL GRANEL

        public async Task<ControlParametroGranelDto?> ListarControlGranel(int envasadoraId, string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden,
                };
                var results = await cnn.QueryMultipleAsync("ENV.LISTAR_GRANEL_CONTROL", parametros, commandType: CommandType.StoredProcedure);

                if (results == null) return null;

                var controlParametro = new ControlParametroGranelDto();

                var cabeceras = results.Read<DateTime>();
                if (cabeceras == null) return controlParametro;

                var controles = results.Read<dynamic>();

                controlParametro.Cabeceras = cabeceras.ToList();
                controlParametro.Controles = controles.ToList();

                return controlParametro;
            }
        }

        public async Task<dynamic> ListarParametroControlGranel()
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("ENV.LISTAR_GRANEL_PARAMETROS_CONTROL", commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarControlGranel(int envasadoraId, string orden, object parametros)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parameters = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden,
                    p_Parametros = JsonSerializer.Serialize(parametros),
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_GRANEL_CONTROL", parameters, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<IEnumerable<dynamic>> ListarObservacionControlGranel(int envasadoraId, string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parameters = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden
                };
                var result = await cnn.QueryAsync<dynamic>("ENV.LISTAR_GRANEL_CONTROL_OBSERVACION", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> GuardarObservacionControlGranel(int envasadoraId, string orden, string observacion)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parameters = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden,
                    p_Observacion = observacion,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_GRANEL_CONTROL_OBSERVACION", parameters, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }


        #endregion CONTROL GRANEL

        #region EVALUACION PT GRANEL

        public async Task<IEnumerable<dynamic>> ListarEvaluacionPTGranel(int envasadoraId, string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parameters = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden
                };
                var result = await cnn.QueryAsync<dynamic>("ENV.LISTAR_GRANEL_EVALUACION_PT", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        
        public async Task<dynamic> ObtenerEvaluacionPTGranel(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parameters = new
                {
                    p_Id = id
                };
                var results = await cnn.QueryMultipleAsync("ENV.OBTENER_GRANEL_EVALUACION_PT_X_ID", parameters, commandType: CommandType.StoredProcedure);

                var evaluacion = await results.ReadFirstOrDefaultAsync<EvaluacionPtDto>();

                var panelistas = await results.ReadAsync<EvaluacionPtDtoPanelista>();

                evaluacion.Panelistas = panelistas.ToList();


                return evaluacion;
            }
        }

        public async Task<bool> GuardarEvaluacionPTGranel(EvaluactionPTCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parameters = new
                {
                    p_EnvasadoraId = request.EnvasadoraId,
                    p_Orden = request.Orden,
                    p_Olor = request.Olor,
                    p_Color = request.Color,
                    p_Sabor = request.Sabor,
                    p_Textura = request.Textura,
                    p_Apariencia = request.AparienciaGeneral,
                    p_CalificacionFinal = request.CalificacionFinal,
                    p_Panelista = request.Panelistas,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };
                var result = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_GRANEL_EVALUACION", parameters, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        #endregion EVALUACION PT GRANEL

        #region CODIFICACION GRANEL

        public async Task<IEnumerable<dynamic>> ListarCodificacionGranel(int envasadoraId, string orden)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parameters = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_Orden = orden
                };
                var result = await cnn.QueryAsync<dynamic>("ENV.LISTAR_GRANEL_CODIFICACION_CAJA", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<bool> GuardarCodificacionGranel(CodificacionCajaGranelCreateDto request)
        {
            using (var cnn = this.Context.CreateConnection)
            {
                var parametros = new
                {
                    p_EnvasadoraId = request.EnvasadoraId,
                    p_Orden = request.Orden,
                    p_Nombre = request.Nombre,
                    p_Ruta = request.Ruta,
                    p_Tamanio = request.Tamanio,
                    p_TipoArchivo = request.TipoArchivo,
                    p_Usuario = this.UserName
                };

                int result = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_GRANEL_CODIFICACION_CAJA", parametros, commandType: CommandType.StoredProcedure);

                return (result > 0);

            }
        }

        #endregion CODIFICACION GRANEL
    }
}
