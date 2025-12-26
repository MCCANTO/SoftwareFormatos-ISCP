
using System.Data;
using Dapper;

namespace IK.SCP.Infrastructure
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public async Task<IEnumerable<dynamic>> ListarAccionXRol(int rolId)
        {
            using (var cnn = this._context.CreateConnection)
            {
                var result = await cnn.QueryAsync<dynamic>("SEG.LISTAR_ACCIONES_X_ROL", new { p_RolId = rolId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
