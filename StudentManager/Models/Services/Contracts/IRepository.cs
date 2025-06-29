
using StudentManager.BackEnd.Frameworks.ResponseFrameworks.Contracts;

namespace StudentManager.BackEnd.Models.Services.Contracts
{
    public interface IRepository<T, TCollection>
    {
        Task<IResponse<TCollection>> SelectAll();
        Task<IResponse<T>> Select(T obj);
        Task<IResponse<T>> Insert(T obj);
        Task<IResponse<T>> Update(T obj);
        Task<IResponse<T>> Delete(T obj);
    }
    
    
}
