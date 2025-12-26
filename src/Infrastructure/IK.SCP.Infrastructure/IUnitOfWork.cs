using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure.Data;

namespace IK.SCP.Infrastructure
{
    public partial interface IUnitOfWork
    {
        Context Context { get; }
        string UserName { get; }
        int Perfil { get; }
        DateTime DateTime { get; }

    }
}
