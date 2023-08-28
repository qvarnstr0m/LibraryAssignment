using LibraryAssignment.Data.Models;

namespace LibraryAssignment.MinimalApi.Interfaces;

public interface IRepository<T>
{
    Task<T?> Create(T entity);
    Task<T?> Get(int id);
    Task<IEnumerable<T?>> GetAll();
    Task<T?> Update(int id);
    Task<T?> Delete(int id);
}