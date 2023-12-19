namespace Arrear.Persistence
{
    public interface IUnitOfWork
    {
        SepRepository Seps { get; }
        UserRepository Users { get; }
    }
}