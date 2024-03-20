namespace University.Services
{
    public sealed class CsvStudentModelMap : ClassMap<Student>
    {
        public CsvStudentModelMap()
        {
            Map(m => m.Human.LastName);
            Map(m => m.Human.FirstName);
            Map(m => m.Course);
            Map(m => m.Speciality);
            Map(m => m.Human.DateOfBirth).TypeConverterOption.Format("dd/MM/yyyy", "dd-MM-yyyy", "yyyy-dd-MM", "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM-dd-yyyy");
            Map(m => m.Human.Gender);
            Map(m => m.Human.Address);
            Map(m => m.Human.Email);
            Map(m => m.Human.Phone);
        }
    }
}
