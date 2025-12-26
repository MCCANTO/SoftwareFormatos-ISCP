namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        Task<IEnumerable<dynamic>> ListarAccionXRol(int rolId);
    }
}
