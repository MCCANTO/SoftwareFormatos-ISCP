
using IK.SCP.Application.Common.Response;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class UpdateArranqueMaquinaCommand : UpdateArranqueMaquinaDto, IRequest<StatusResponse<int>>
    {
    }

    public class UpdateArranqueMaquinaCommandHandler : IRequestHandler<UpdateArranqueMaquinaCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public UpdateArranqueMaquinaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(UpdateArranqueMaquinaCommand request, CancellationToken cancellationToken)
        {

            using (var cnn = _uow.Context.CreateConnection)
            {
                //cnn.Open();

                //using var trx = cnn.BeginTransaction();

                try
                {

                    //var id = await _uow.EnvArranqueMaquina.Guardar(request.principal, _uow.UserName, cnn, trx);

                    //if (id == 0)
                    //    return new StatusResponse<int> { Ok = false, Message = "No se pudo guardar la información" };

                    //if (request.condicionesPrevias != null)
                    //{
                    //    await _uow.EnvArranqueMaquina.GuardarCondicionesPrevias(id, request.condicionesPrevias, _uow.UserName, cnn, trx);
                    //}

                    //if (request.variablesBasicas != null)
                    //{
                    //    await _uow.EnvArranqueMaquina.GuardarVariablesBasicas(id, request.variablesBasicas, _uow.UserName, cnn, trx);
                    //}

                    //trx.Commit();

                    return new StatusResponse<int> { Ok = true, Data = 1, Message = "Información guardada correctamente" };
                }
                catch (Exception ex)
                {
                    //trx.Rollback();
                    return new StatusResponse<int> { Ok = false };
                }

            }

            //using (var cnn = _uow.Context.CreateConnection)
            //{
            //    cnn.Open();
            //    using var trx = cnn.BeginTransaction();

            //    try
            //    {

            //        var parametros = new
            //        {
            //            p_ArranqueMaquinaId = request.arranqueMaquinaId,
            //            p_PesoSobreVacio = request.pesoSobreVacio,
            //            p_PesoSobreProducto1 = request.pesoSobreProducto1,
            //            p_PesoSobreProducto2 = request.pesoSobreProducto2,
            //            p_PesoSobreProducto3 = request.pesoSobreProducto3,
            //            p_PesoSobreProducto4 = request.pesoSobreProducto4,
            //            p_PesoSobreProducto5 = request.pesoSobreProducto5,
            //            p_PesoSobreProductoProm = request.pesoSobreProductoProm,
            //            p_Observacion = request.observacion ?? "",
            //            p_Cerrado = request.cerrado,
            //            p_UsuarioModificacion = _uow.UserName
            //        };

            //        var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.PA_ACTUALIZAR_ARRANQUE_MAQUINA", parametros, commandType: System.Data.CommandType.StoredProcedure, transaction: trx);

            //        var condiciones = request.condicionesPrevias.Where(f => f.id == 0).ToList();

            //        foreach (var item in condiciones)
            //        {
            //            var detalles = item.detalles
            //                            .Select(p => new InsertCondicionPreviaDetalleDto()
            //                            {
            //                                condicionPreviaId = p.condicionPreviaId,
            //                                valor = p.valor,
            //                                observacion = p.observacion
            //                            })
            //                            .ToList();
            //            var detalle = DataConvertHelper.ToDataTable<InsertCondicionPreviaDetalleDto>(detalles);

            //            var parametros_2 = new
            //            {
            //                p_ArranqueMaquinaId = id_arranque,
            //                p_TipoId = item.tipoId,
            //                p_Condiciones = detalle,
            //                p_UsuarioCreacion = _uow.UserName
            //            };

            //            await cnn.ExecuteAsync("ENV.PA_INSERTAR_ARRANQUE_MAQUINA_CONDICION_PREVIA", parametros_2, commandType: System.Data.CommandType.StoredProcedure, transaction: trx);
            //        }

            //        var variables = request.variablesBasicas.Where(f => f.id == 0).ToList();

            //        foreach (var item in variables)
            //        {
            //            var detalles = item.detalles
            //                            .Select(p => new InsertVariableBasicaDetalleDto()
            //                            {
            //                                variableBasicaId = p.variableBasicaId,
            //                                valor = p.valor,
            //                                observacion = p.observacion
            //                            })
            //                            .ToList();

            //            var detalle = DataConvertHelper.ToDataTable<InsertVariableBasicaDetalleDto>(detalles);

            //            var parametros_3 = new
            //            {
            //                p_ArranqueMaquinaId = id_arranque,
            //                p_Variables = detalle,
            //                p_UsuarioCreacion = _uow.UserName
            //            };

            //            await cnn.ExecuteAsync("ENV.PA_INSERTAR_ARRANQUE_MAQUINA_VARIABLE_BASICA", parametros_3, commandType: System.Data.CommandType.StoredProcedure, transaction: trx);

            //        }

            //        trx.Commit();

            //        return new StatusResponse<int> { Ok = true, Data = id_arranque };
            //    }
            //    catch (Exception ex)
            //    {
            //        trx.Rollback();
            //        return new StatusResponse<int> { Ok = false, Message = "Error al registrar arranque de máquina." };
            //    }
            //}
        }
    }
}
