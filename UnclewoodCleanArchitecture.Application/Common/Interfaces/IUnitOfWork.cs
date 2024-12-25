namespace UnclewoodCleanArchitecture.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}