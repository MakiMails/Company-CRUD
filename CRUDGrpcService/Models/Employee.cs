using Google.Protobuf.WellKnownTypes;
using UsersCRUDClassLibrary.CrudLibraryProto;

namespace CRUDGrpcService.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public string Post { get; set; } = string.Empty;
        public double Salary { get; set; }
        public DateTime TermContactForData { get; set; }

        public EmployeeMessage ToEmployeeMessage()
        {
            EmployeeMessage employeeMessage = new EmployeeMessage()
            {
                Id = Id,
                Surname = Surname,
                Name = Name,
                Patronymic = Patronymic,
                CompanyId = CompanyId,
                Company = Company.ToCompanyMessangeLessEmployees(),
                Post = Post,
                Salary = Salary,
                TermContactForData = Timestamp.FromDateTime(TermContactForData.ToUniversalTime())
            };

            return employeeMessage;
        }

        public EmployeeMessage ToEmployeeMessageNotCompony()
        {
            EmployeeMessage employeeMessage = new EmployeeMessage()
            {
                Id = Id,
                Surname = Surname,
                Name = Name,
                Patronymic = Patronymic,
                CompanyId = CompanyId,
                Post = Post,
                Salary = Salary,
                TermContactForData = Timestamp.FromDateTime(TermContactForData.ToUniversalTime())
            };

            return employeeMessage;
        }
    }
}
