using Microsoft.EntityFrameworkCore;

namespace CRUDGrpcService.Models.CRUD
{
    public class CrudEmployee
    {
        private ApplicationContext _db;

        public CrudEmployee(ApplicationContext db)
        {
            _db = db;
        }

        public List<Employee> GetList()
        {
            return _db.Employees.Include(e => e.Company).ToList();
        }

        public Employee Get(int id)
        {
            List<Employee> employees = _db.Employees.Include(e => e.Company).ToList();

            return employees.Find(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
        }

        public void Update(Employee employee)
        {
            List<Employee> employees = _db.Employees.Include(e => e.Company).ToList();

            Employee? employeeDb = employees.Find(e => e.Id == employee.Id);

            if (employeeDb == null)
            {
                return;
            }

            employeeDb.Surname = employee.Surname;
            employeeDb.Name = employee.Name;
            employeeDb.Patronymic = employee.Patronymic;
            employeeDb.Post = employee.Post;
            employeeDb.Salary = employeeDb.Salary;
            employeeDb.TermContactForData = employee.TermContactForData;

            _db.SaveChanges();
        }

        public void Delete(int id) 
        {
            List<Employee> employees = _db.Employees.Include(e => e.Company).ToList();

            Employee? employee = employees.Find(e => e.Id == id);

            if (employee == null)
            {
                return;
            }

            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}
