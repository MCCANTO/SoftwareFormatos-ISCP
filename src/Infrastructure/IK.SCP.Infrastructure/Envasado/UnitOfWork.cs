using IK.SCP.Domain.Dtos.ENV;
using IK.SCP.Domain.Dtos;
using Dapper;
using System.Data;
using System.Text.Json;
using Azure.Core;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {

        #region ARRANQUE MAQUINA

        public async Task<int> GuardarEnvasadoArranqueMaquina(ArranqueMaquinaDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaquinaId = request.arranqueMaquinaId,
                    p_EnvasadoraId = request.envasadoraId,
                    p_OrdenId = request.ordenId,
                    p_PesoSobreVacio = request.pesoSobreVacio,
                    p_PesoSobreProducto1 = request.pesoSobreProducto1,
                    p_PesoSobreProducto2 = request.pesoSobreProducto2,
                    p_PesoSobreProducto3 = request.pesoSobreProducto3,
                    p_PesoSobreProducto4 = request.pesoSobreProducto4,
                    p_PesoSobreProducto5 = request.pesoSobreProducto5,
                    p_PesoSobreProductoProm = request.pesoSobreProductoProm,
                    p_Observacion = request.observacion,
                    p_Usuario = this.UserName,
                    p_Cerrado = request.cerrado
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_ARRANQUE_MAQUINA", parametros, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<ArranqueMaquinaCondicionPreviaDto?> ObtenerEnvasadoArranqueMaquinaCondicionPrevia(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaquinaCondPrevCabId = id
                };

                var results = await cnn.QueryMultipleAsync("ENV.PA_LISTAR_ARRANQUE_MAQUINA_CONDICION_PREVIA_POR_ID", parametros, commandType: CommandType.StoredProcedure);

                if (results == null) return null;

                var _arranqueMaquina = results.ReadFirstOrDefault<ArranqueMaquinaCondicionPreviaDto>();
                if (_arranqueMaquina == null) _arranqueMaquina = new ArranqueMaquinaCondicionPreviaDto();

                var _condiciones = results.Read<ArranqueMaquinaCondicionPreviaItemDto>();
                if (_condiciones.Any()) _arranqueMaquina.items = _condiciones.ToList();

                return _arranqueMaquina;
            }
        }

        public async Task<int> GuardarEnvasadoArranqueMaquinaCondicionPrevia(ArranqueMaquinaCondicionPreviaCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaquinaId = request.arranqueMaquinaId,
                    p_TipoId = request.tipoId,
                    p_Condiciones = JsonSerializer.Serialize(request.condiciones),
                    p_Usuario = this.UserName
                };

                return await cnn.ExecuteScalarAsync<int>("ENV.PA_INSERTAR_ARRANQUE_MAQUINA_CONDICION_PREVIA", parametros, commandType: CommandType.StoredProcedure); 
            }
        }

        public async Task<dynamic?> ObtenerEnvasadoArranqueMaquinaVariableBasica(int id)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaquinaVarBasCabId = id
                };

                var results = await cnn.QueryAsync("ENV.PA_LISTAR_ARRANQUE_MAQUINA_VARIABLE_BASICA_X_ID", parametros, commandType: CommandType.StoredProcedure);

                if (results == null) return null;

                var data = results.ToList();

                var variables = data.GroupBy(g => g.Padre)
                                    .Select(x => new ArranqueMaquinaVariableBasicaItemDto
                                    { 
                                        padre = x.Key,
                                        items = x.Select(y => new ArranqueMaquinaVariableBasicaSubItemDto
                                        {
                                            id = y.Id,
                                            variableBasicaId = y.VariableBasicaId,
                                            nombre = y.Nombre,
                                            comentario = y.Comentario,
                                            valor = y.Valor,
                                            observacion = y.Observacion,
                                            cerrado = y.Cerrado ?? false,
                                            primerOrden = y.PrimerOrden,
                                            segundoOrden = y.SegundoOrden
                                        }).ToList()
                                    });

                return variables;
            }
        }

        public async Task<int> GuardarEnvasadoArranqueMaquinaVariableBasica(ArranqueMaquinaVariableBasicaCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaquinaVarBasCabId = request.arranqueMaquinaVarBasCabId,
                    p_ArranqueMaquinaId = request.arranqueMaquinaId,
                    p_Variables = JsonSerializer.Serialize(request.variables),
                    p_UsuarioCreacion = this.UserName
                };

                return await cnn.ExecuteScalarAsync<int>("ENV.PA_INSERTAR_ARRANQUE_MAQUINA_VARIABLE_BASICA", parametros, commandType: CommandType.StoredProcedure);
            }

        }

        public async Task<int> GuardarEnvasadoArranqueMaquinaObservacion(int arranqueMaquinaId, string observacion)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueMaquinaId = arranqueMaquinaId,
                    p_Observacion = observacion,
                    p_UsuarioCreacion = this.UserName
                };

                return await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_MAQUINA_OBSERVACION", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion ARRANQUE MAQUINA

        #region REGISTRO PEDACERIA

        public async Task<List<dynamic>> ListarEnvasadoRegistroPedaceria(int envasadoraId, string ordenId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_EnvasadoraId = envasadoraId,
                    p_OrdenId = ordenId,
                };

                var result = await cnn.QueryAsync<dynamic>("ENV.LISTAR_REGISTRO_PEDACERIA", parametros, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<bool> GuardarEnvasadoRegistroPedaceria(EnvasadoRegistroPedaceriaCreateDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_EnvasadoraId = request.EnvasadoraId,
                    p_OrdenId = request.OrdenId,
                    p_Peso = request.Peso,
                    p_Pedaceria = request.Pedaceria,
                    p_PorcentajePedaceria = request.PorcentajePedaceria,
                    p_HojuelaEntera = request.HojuelasEnteras,
                    p_PorcentajeHojuelaEntera = request.PorcentajeHojuelasEnteras,
                    p_Inspector = request.Inspector,
                    p_Observacion = request.Observacion,
                    p_Usuario = this.UserName
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_REGISTRO_PEDACERIA", parametros, commandType: CommandType.StoredProcedure);
                return (result > 0);
            }
        }

        public async Task<int> GuardarEnvasadoArranqueEnvasado(ArranqueEnvasadoDto request)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var parametros = new
                {
                    p_ArranqueId = request.arranqueId,
                    p_EnvasadoraId = request.envasadoraId,
                    p_OrdenId = request.ordenId,
                    p_Usuario = this.UserName,
                    p_Cerrado = request.cerrado
                };

                var result = await cnn.ExecuteScalarAsync<int>("ENV.GUARDAR_ARRANQUE_ENVASADO", parametros, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        #endregion REGISTRO PEDACERIA
    }
}
