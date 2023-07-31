using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CRUDGrpcService.Models.CRUD
{
    public class CrudCompany
    {
        private ApplicationContext _db;

        public CrudCompany(ApplicationContext db)
        {
            _db = db;
        }

        public List<Company> GetList()
        {
            return _db.Companys.ToList();
        }

        public Company GetById(int id)
        {
            List<Company> companies = _db.Companys.Include(c => c.Employees).ToList();

            return companies.Find(c => c.Id == id);
        }

        public void Add(Company company)
        {
            _db.Companys.Add(company);
            _db.SaveChanges();
        }

        public void Update(Company company)
        {
            List<Company> companies = _db.Companys.Include(c => c.Employees).ToList();

            Company? companyDb = companies.Find(c => c.Id == company.Id);

            if(companyDb == null)
            {
                return;
            }

            companyDb.Name = company.Name;
            companyDb.Sity = company.Sity;
            companyDb.IncomeYear = company.IncomeYear;

            _db.SaveChanges();

        }

        public void Delete(int id)
        {
            List<Company> companies = _db.Companys.Include(c => c.Employees).ToList();

            Company? company = companies.Find(c => c.Id == id);
            
            if (company == null)
            {
                return;
            }

            _db.Companys.Remove(company);
            _db.SaveChanges();
        }
    }
}
