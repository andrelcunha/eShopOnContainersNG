namespace ALC.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
