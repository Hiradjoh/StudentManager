namespace StudentManager.BackEnd.ApplicationServices.Dtos.StudentDtos 
{ 
    public class GetStudentServiceDto
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }


    }
}