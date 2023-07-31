using CRUDGrpcService.Models;
using CRUDGrpcService.Models.CRUD;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.ComponentModel.Design;
using UsersCRUDClassLibrary.CrudCompanysProto;
using UsersCRUDClassLibrary.CrudEmployeesProto;
using UsersCRUDClassLibrary.CrudLibraryProto;

namespace CRUDGrpcService.Services
{
    public class CrudEmployeeService : CrudEmployees.CrudEmployeesBase
    {
        private ApplicationContext _db;

        public CrudEmployeeService(ApplicationContext db)
        {
            _db = db;
        }

        public override Task<ListEmployeesReply> ListEmployees(Empty request, ServerCallContext context)
        {
            CrudEmployee crudEmployee = new CrudEmployee(_db);

            List<EmployeeMessage> employeeMessages = crudEmployee.GetList()
                .Select(ce => ce.ToEmployeeMessage()).ToList();

            ListEmployeesReply listEmployeesReply = new ListEmployeesReply();
            listEmployeesReply.Employees.AddRange(employeeMessages);

            return Task.FromResult(listEmployeesReply);
        }

        public override Task<EmployeeMessage> GetEmployee(GetEmployeeRequest request, ServerCallContext context)
        {
            CrudEmployee crudEmployee = new CrudEmployee(_db);

            EmployeeMessage employeeMessage = crudEmployee.Get(request.Id).ToEmployeeMessage();

            return Task.FromResult(employeeMessage);
        }

        public override Task<MessageReply> CreateEmployee(CreateEmployeeRequest request, ServerCallContext context)
        {
            EmployeeMessage employeeMessage = request.Employee;

            Employee employee = new Employee()
            {
                Surname = employeeMessage.Surname,
                Name = employeeMessage.Name,
                Patronymic = employeeMessage.Patronymic,
                Post = employeeMessage.Post,
                CompanyId = employeeMessage.CompanyId,
                Salary = employeeMessage.Salary,
                TermContactForData = employeeMessage.TermContactForData.ToDateTime()
            };

            CrudEmployee crudEmployee = new CrudEmployee(_db);
            crudEmployee.Add(employee);

            return Task.FromResult(new MessageReply() { Text = "Employee create and add in database." });
        }

        public override Task<MessageReply> UpdateEmployee(UpdateEmployeeRequest request, ServerCallContext context)
        {
            EmployeeMessage employeeMessage = request.Employee;

            Employee employee = new Employee()
            {
                Id = employeeMessage.Id,
                Surname = employeeMessage.Surname,
                Name = employeeMessage.Name,
                Patronymic = employeeMessage.Patronymic,
                Post = employeeMessage.Post,
                Salary = employeeMessage.Salary,
                TermContactForData = employeeMessage.TermContactForData.ToDateTime()
            };

            CrudEmployee crudEmployee = new CrudEmployee(_db);
            crudEmployee.Update(employee);

            return Task.FromResult(new MessageReply() { Text = "Employee update." });
        }

        public override Task<MessageReply> DeleteEmployee(DeleteEmployeeRequest request, ServerCallContext context)
        {
            CrudEmployee crudEmployee = new CrudEmployee(_db);
            crudEmployee.Delete(request.Id);
            return Task.FromResult(new MessageReply() { Text = "Employee remove." });
        }
    }
}
