namespace OkalaExchange.Domain.Seedwork;

public interface IUnitOfWork 
{
    Task<int> CommitAsync();
}
