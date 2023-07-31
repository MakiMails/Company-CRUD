using UsersCRUDClassLibrary.CrudLibraryProto;

namespace CRUDGrpcService.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sity { get; set; } = string.Empty;
        public double IncomeYear { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public CompanyMessage ToCompanyMessage()
        {
            CompanyMessage companyMessage = new CompanyMessage()
            {
                Id = Id,
                Name = Name,
                Sity = Sity,
                IncomeYear = IncomeYear,
            };

            companyMessage.Employees.Add(Employees.Select(e => e.ToEmployeeMessageNotCompony()));

            return companyMessage;
        }

        public CompanyMessage ToCompanyMessangeLessEmployees()
        {
            CompanyMessage companyMessage = new CompanyMessage()
            {
                Id = Id,
                Name = Name,
                Sity = Sity,
                IncomeYear = IncomeYear,
            };

            return companyMessage;
        }
    }
}
