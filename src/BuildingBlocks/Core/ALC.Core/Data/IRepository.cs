using ALC.Core.DomainObjects;

namespace ALC.Core.Data
{
    public interface IRepository<T>: IDisposable where T : IAggregationRoot
    {
        IUnitOfWork UnitOfWork {get;}
    }
}
