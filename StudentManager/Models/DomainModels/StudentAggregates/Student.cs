namespace StudentManager.BackEnd.Models.DomainModels.StudentAggregates
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }


    }
}
