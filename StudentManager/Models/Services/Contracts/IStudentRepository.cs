using StudentManager.BackEnd.Models.DomainModels.StudentAggregates;

namespace StudentManager.BackEnd.Models.Services.Contracts
{
    public interface IStudentRepository : IRepository<Student, IEnumerable<Student>>
    {
    }
}
