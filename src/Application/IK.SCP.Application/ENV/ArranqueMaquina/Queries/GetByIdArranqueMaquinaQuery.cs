using Dapper;
using IK.SCP.Application.Common.Constants;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;

namespace IK.SCP.Application.ENV.Queries
{
    public class GetByIdArranqueMaquinaQuery : IRequest<StatusResponse>
    {
        public int id { get; set; }
    }

    public class GetByIdArranqueMaquinaQueryHandler : IRequestHandler<GetByIdArranqueMaquinaQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetByIdArranqueMaquinaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetByIdArranqueMaquinaQuery request, CancellationToken cancellationToken)
        {

            using (var cnn = _uow.Context.CreateConnection)
            {
                var results = await cnn.QueryMultipleAsync("ENV.PA_OBTENER_ARRANQUE_MAQUINA_X_ID", new { p_ArranqueMaquinaId = request.id }, commandType: CommandType.StoredProcedure);

                var arranque = await results.ReadFirstOrDefaultAsync<ArranqueMaquinaViewModel>();

                if (arranque == null) return StatusResponse.False(QueryConst.MSJ_GET_ERROR);

                var observaciones = await results.ReadAsync<ArranqueMaquinaObservacionViewModel>();
                var condiciones = await results.ReadAsync<ArranqueMaquinaCondPrevCabViewModel>();
                var variables = await results.ReadAsync<ArranqueMaquinaVarBasCabViewModel>();

                arranque.Observaciones = observaciones.ToList();
                arranque.Condiciones = condiciones.ToList();
                arranque.Variables = variables.ToList();

                return StatusResponse.True(QueryConst.MSJ_GET_OK, data: arranque);


                //var arranque = await results.ReadFirstOrDefaultAsync<ArranqueMaquinaViewModel>();

                //if (arranque != null)
                //{
                //    var observaciones = await results.ReadAsync<ArranqueMaquinaObservacionViewModel>();

                //    var condiciones = await results.ReadAsync<ArranqueMaquinaCondPrevCabViewModel>();
                //    var condicionesDetalle = await results.ReadAsync<dynamic>();

                //    var variables = await results.ReadAsync<ArranqueMaquinaVarBasCabViewModel>();
                //    var variablesDetalle = await results.ReadAsync<dynamic>();

                //    arranque.Observaciones = observaciones.ToList();

                //    arranque.Condiciones = condiciones.ToList();

                //    if (condicionesDetalle.ToList().Count > 0)
                //    {
                //        arranque.Condiciones.ForEach(p =>
                //        {
                //            p.detalles = condicionesDetalle
                //                            .Where(f => f.ArranqueMaquinaCondPrevCabId == p.Id)
                //                            .Select(x => new ArranqueMaquinaCondPrevViewModel()
                //                            {
                //                                Id = x.Id,
                //                                CondicionPreviaId = x.CondicionPreviaId,
                //                                Nombre = x.Nombre,
                //                                Comentario = x.Comentario,
                //                                Orden = x.Orden,
                //                                Valor = x.Valor,
                //                                Observacion = x.Observacion
                //                            }).ToList(); 
                //        });

                //    }

                //    arranque.Variables = variables.ToList();

                //    if (variablesDetalle.ToList().Count > 0)
                //    {
                //        arranque.Variables.ForEach(p =>
                //        {
                //            p.detalles = variablesDetalle
                //                            .Where(f => f.ArranqueMaquinaVarBasCabId == p.Id)
                //                            .Select(x => new ArranqueMaquinaVarBasViewModel()
                //                            {
                //                                id = x.Id,
                //                                variableBasicaId = x.VariableBasicaId,
                //                                padre = x.Padre,
                //                                nombre = x.Nombre,
                //                                comentario = x.Comentario,
                //                                primerOrden = x.PrimerOrden,
                //                                segundoOrden = x.SegundoOrden,
                //                                valor = x.Valor,
                //                                observacion = x.Observacion,
                //                                cerrado = x.Cerrado
                //                            }).ToList();
                //        });
                //    }
                //}

                //return new StatusResponse<ArranqueMaquinaViewModel> { Ok = true, Data = arranque };
            }
        }
    }
}
