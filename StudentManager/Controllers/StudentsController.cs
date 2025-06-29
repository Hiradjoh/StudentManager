using StudentManager.BackEnd.ApplicationServices.Contracts;
using StudentManager.BackEnd.ApplicationServices.Dtos.StudentDtos;
using Microsoft.AspNetCore.Mvc;


namespace StudentManager.BackEnd.Controllers
{
  
        [ApiController]
        [Route("[controller]")]
        public class StudentsController : ControllerBase
        {
            private readonly IStudentService _studentService;
            private readonly ILogger<StudentsController> _logger;
            #region [- ctor -]
            public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
            {
            _studentService = studentService;
                _logger = logger;
            }
            #endregion


            #region [- GetAll() -]
            [HttpGet("all")]
            public async Task<IActionResult> GetAll()
            {
                Guard_StudentService();
                var getAllResponse = await _studentService.GetAll();
                var response = getAllResponse.Value.GetStudentServiceDtos;
                return new JsonResult(response);
            }
            #endregion

            #region [- Get() -]
            [HttpGet("{id:int}")]
            public async Task<IActionResult> Get(int id)
            {
                Guard_StudentService();
                var dto = new GetStudentServiceDto() { Id = id };
                var getResponse = await _studentService.Get(dto);
                var response = getResponse.Value;
                if (response is null)
                {
                    return new JsonResult("NotFound");
                }
                return new JsonResult(response);
            }
            #endregion

            #region [- Post() -]
            [HttpPost(Name = "PostStudent")]
            public async Task<IActionResult> Post([FromBody] PostStudentServiceDto dto)
            {
                Guard_StudentService();
                var postDto = new GetStudentServiceDto() { NationalCode = dto.NationalCode };
                var getResponse = await _studentService.Get(postDto);

                switch (ModelState.IsValid)
                {
                    case true when getResponse.Value is null:
                        {
                            var postResponse = await _studentService.Post(dto);
                            return postResponse.IsSuccessful ? Ok() : BadRequest();
                        }
                    case true when getResponse.Value is not null:
                        return Conflict(dto);
                    default:
                        return BadRequest();
                }
            }
            #endregion

            #region [- Put() -]
            [HttpPut("{id:int}")]
            public async Task<IActionResult> Put([FromBody] PutStudentServiceDto dto)
            {
                Guard_StudentService();

                //Pay attention to Email uniqueness problem in the app.

                var putDto = new GetStudentServiceDto() { NationalCode = dto.NationalCode };


                if (ModelState.IsValid)
                {
                    var putResponse = await _studentService.Put(dto);
                    return putResponse.IsSuccessful ? Ok() : BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            #endregion

            #region [- Delete() -]
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete([FromBody] DeleteStudentServiceDto dto)
            {
                Guard_StudentService();
                var deleteResponse = await _studentService.Delete(dto);
                return deleteResponse.IsSuccessful ? Ok() : BadRequest();
            }
            #endregion

            #region [- StudentServiceGuard() -]
            private ObjectResult Guard_StudentService()
            {
                if (_studentService is null)
                {
                    return Problem($" {nameof(_studentService)} is null.");
                }

                return null;
            }
        #endregion
      
    }
}

    

