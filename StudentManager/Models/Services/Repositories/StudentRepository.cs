using StudentManager.BackEnd.Frameworks;
using StudentManager.BackEnd.Frameworks.ResponseFrameworks;
using StudentManager.BackEnd.Frameworks.ResponseFrameworks.Contracts;
using StudentManager.BackEnd.Models.DomainModels.StudentAggregates;
using StudentManager.BackEnd.Models.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;
using StudentManager.Frameworks;

namespace StudentManager.BackEnd.Models.Services.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ProjectDbContext _projectDbContext;

        #region [- ctor -]
        public StudentRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }
        #endregion

        #region [- SelectAll() -]
        public async Task<IResponse<IEnumerable<Student>>> SelectAll()
        {
            try
            {
                var Students = await _projectDbContext.Student.AsNoTracking().ToListAsync();
                return Students is null ?
                    new Response<IEnumerable<Student>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<IEnumerable<Student>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, Students);
            }
            catch (Exception )
            {
                throw;
 
            }

        }
        #endregion

        #region [- Select() -]
        public async Task<IResponse<Student>> Select(Student student)
        {
            try
            {
                var responseValue = new Student();
                if (student.Id.ToString() == "")
                {
                   
                    responseValue = await _projectDbContext.Student.Where(c => c.NationalCode == student.NationalCode).SingleOrDefaultAsync();
                }
                else
                {
                    responseValue = await _projectDbContext.Student.FindAsync(student.Id);
                }
                return responseValue is null ?
                     new Response<Student>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                     new Response<Student>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseValue);
            }
            catch (Exception ex)
            {
              
                return new Response<Student>(false, HttpStatusCode.InternalServerError, ex.Message, null);
            }

        }
        #endregion

        #region [- Insert() -]
        public async Task<IResponse<Student>> Insert(Student model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Student>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _projectDbContext.AddAsync(model);
                await _projectDbContext.SaveChangesAsync();
                var response = new Response<Student>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception ex)
            {
               
                return new Response<Student>(false, HttpStatusCode.InternalServerError, ex.Message, null);
            }

        }
        #endregion

        #region [- Update() -]
        public async Task<IResponse<Student>> Update(Student model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Student>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
               
                _projectDbContext.Entry(model).State = EntityState.Modified;
                await _projectDbContext.SaveChangesAsync();
                var response = new Response<Student>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception ex)
            {
                
                return new Response<Student>(false, HttpStatusCode.InternalServerError, ex.Message, null);
            }

        }
        #endregion

        #region [- Delete() -]
        public async Task<IResponse<Student>> Delete(Student model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Student>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }

                _projectDbContext.Student.Remove(model);
                await _projectDbContext.SaveChangesAsync();
                var response = new Response<Student>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception ex)
            {
               
                return new Response<Student>(false, HttpStatusCode.InternalServerError, ex.Message, null);
            }

        }
        #endregion

    }
}

