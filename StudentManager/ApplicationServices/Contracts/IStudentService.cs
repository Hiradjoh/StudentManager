
using StudentManager.BackEnd.ApplicationServices.Dtos.StudentDtos;

namespace StudentManager.BackEnd.ApplicationServices.Contracts
{
    public interface IStudentService:IService<PostStudentServiceDto,GetStudentServiceDto,GetAllStudentServiceDto,PutStudentServiceDto,DeleteStudentServiceDto>
    {
    }
}
