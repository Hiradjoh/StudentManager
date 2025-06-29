

using StudentManager.BackEnd.Frameworks.ResponseFrameworks.Contracts;

namespace StudentManager.BackEnd.ApplicationServices.Contracts
{
    public interface IService<TPost, TGet, TGetAll, TUpdate, TDelete>
    {
        Task<IResponse<TGetAll>> GetAll();
        Task<IResponse<TGet>> Get(TGet dto);
        Task<IResponse<TPost>> Post(TPost dto);
        Task<IResponse<TUpdate>> Put(TUpdate dto);
        Task<IResponse<TDelete>> Delete(TDelete dto);
    }

}