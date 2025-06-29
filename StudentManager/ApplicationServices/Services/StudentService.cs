
using StudentManager.BackEnd.Frameworks;
using StudentManager.BackEnd.ApplicationServices.Contracts;
using StudentManager.BackEnd.ApplicationServices.Dtos.StudentDtos;
using StudentManager.BackEnd.Frameworks;
using StudentManager.BackEnd.Frameworks.ResponseFrameworks;
using StudentManager.BackEnd.Frameworks.ResponseFrameworks.Contracts;
using StudentManager.BackEnd.Models.DomainModels.StudentAggregates;
using StudentManager.BackEnd.Models.Services.Contracts;
using StudentManager.BackEnd.Models.Services.Repositories;
using System.Net;
using StudentManager.Frameworks;


namespace StudentManager.BackEnd.ApplicationServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        #region [- ctor -]
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        #endregion

        #region [- GetAll() -]
        public async Task<IResponse<GetAllStudentServiceDto>> GetAll()
        {
            var selectAllResponse = await _studentRepository.SelectAll();

            if (selectAllResponse is null)
            {
                return new Response<GetAllStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectAllResponse.IsSuccessful)
            {
                return new Response<GetAllStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }

            var getAllStudentDto = new GetAllStudentServiceDto() { GetStudentServiceDtos = new List<GetStudentServiceDto>() };

            foreach (var item in selectAllResponse.Value)
            {
                var studentDto = new GetStudentServiceDto()
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    NationalCode = item.NationalCode,
                    BirthDate = item.BirthDate
                };
                getAllStudentDto.GetStudentServiceDtos.Add(studentDto);
            }

            var response = new Response<GetAllStudentServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getAllStudentDto);
            return response;
        }
        #endregion

        #region [- Get() -]
        public async Task<IResponse<GetStudentServiceDto>> Get(GetStudentServiceDto dto)
        {
            var student = new Student()
            {
                Id = dto.Id,
                FullName = dto.FullName,
                NationalCode = dto.NationalCode,
                BirthDate = dto.BirthDate
            };
            var selectResponse = await _studentRepository.Select(student);

            if (selectResponse is null)
            {
                return new Response<GetStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectResponse.IsSuccessful)
            {
                return new Response<GetStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }
            var getStudentServiceDto = new GetStudentServiceDto()
            {
                Id = selectResponse.Value.Id,
                FullName = selectResponse.Value.FullName,
                NationalCode = selectResponse.Value.NationalCode,
                BirthDate = selectResponse.Value.BirthDate
            };
            var response = new Response<GetStudentServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getStudentServiceDto);
            return response;
        }
        #endregion

        #region [- Post() -]
        public async Task<IResponse<PostStudentServiceDto>> Post(PostStudentServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PostStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var postStudent = new Student()
            {
               
                FullName = dto.FullName,
                NationalCode = dto.NationalCode,
                BirthDate = dto.BirthDate
            };
            var insertResponse = await _studentRepository.Insert(postStudent);

            if (!insertResponse.IsSuccessful)
            {
                return new Response<PostStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PostStudentServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

        #region [- Put() -]
        public async Task<IResponse<PutStudentServiceDto>> Put(PutStudentServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PutStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var putStudent = new Student()
            {
                Id = dto.Id,
                FullName = dto.FullName,
                NationalCode = dto.NationalCode,
                BirthDate = dto.BirthDate
            };
            var updateResponse = await _studentRepository.Update(putStudent);

            if (!updateResponse.IsSuccessful)
            {
                return new Response<PutStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PutStudentServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

        #region [- Delete() -]
        public async Task<IResponse<DeleteStudentServiceDto>> Delete(DeleteStudentServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<DeleteStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var student = new Student() { Id = dto.Id };

            var deleteResponse = await _studentRepository.Delete(student);

            if (deleteResponse is null || !deleteResponse.IsSuccessful)
            {
                return new Response<DeleteStudentServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            var response = new Response<DeleteStudentServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion
    }
}


