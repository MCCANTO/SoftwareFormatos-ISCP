namespace IK.SCP.Application.FR.Queries
{
    //public class GetArranqueMaquinaQuery : IRequest<StatusResponse<object>>
    //{
    //    public int linea { get; set; }
    //    public string orden { get; set; }
    //}

    //public class GetArranqueMaquinaQueryHandler : IRequestHandler<GetArranqueMaquinaQuery, StatusResponse<object>>
    //{
    //    private readonly IUnitOfWork _uow;

    //    public GetArranqueMaquinaQueryHandler(IUnitOfWork uow)
    //    {
    //        _uow = uow;
    //    }

    //    public async Task<StatusResponse<object>> Handle(GetArranqueMaquinaQuery request, CancellationToken cancellationToken)
    //    {
    //        using (var cnn = _uow.Context.CreateConnection)
    //        {
    //            var arranqueMaquina = await cnn.QueryMultipleAsync("FR.OBTENER_ARRANQUE_MAQUINA_ACTIVO", new { p_LineaId = request.linea, p_Orden = request.orden }, commandType: CommandType.StoredProcedure);



    //            return new StatusResponse<object> { Ok = true };
    //        }
    //    }
    //}
}
