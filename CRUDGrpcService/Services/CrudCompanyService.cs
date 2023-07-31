using CRUDGrpcService.Models;
using CRUDGrpcService.Models.CRUD;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using UsersCRUDClassLibrary.CrudCompanysProto;
using UsersCRUDClassLibrary.CrudLibraryProto;

namespace CRUDGrpcService.Services
{
    public class CrudCompanyService : CrudCompanys.CrudCompanysBase
    {
        private ApplicationContext _db;

        public CrudCompanyService(ApplicationContext db)
        {
            _db = db;
        }

        public override Task<ListCompanysReply> ListCompanys(Empty request, ServerCallContext context)
        {
            CrudCompany crudCompany = new CrudCompany(_db);
            List<Company> companys = crudCompany.GetList();
            List<CompanyMessage> companyMessages = companys.Select(c => c.ToCompanyMessage()).ToList();

            ListCompanysReply listCompanysReply = new ListCompanysReply();
            listCompanysReply.Companys.AddRange(companyMessages);

            return Task.FromResult(listCompanysReply);
        }

        public override Task<CompanyMessage> GetCompany(GetCompanyRequest request, ServerCallContext context)
        {
            CrudCompany crudCompany = new CrudCompany(_db);
            return Task.FromResult(crudCompany.GetById(request.Id).ToCompanyMessage());
        }

        public override Task<MessageReply> CreateCompany(CreateCompanyRequest request, ServerCallContext context)
        {
            CompanyMessage companyMessage = request.Company;

            Company newCompany = new Company()
            {
                Name = companyMessage.Name,
                Sity = companyMessage.Sity,
                IncomeYear = companyMessage.IncomeYear,
            };

            CrudCompany crudCompany = new CrudCompany(_db);

            crudCompany.Add(newCompany);

            return Task.FromResult(new MessageReply() { Text ="Create new compony."});
        }

        public override Task<MessageReply> UpdateCompany(UpdateCompanyRequest request, ServerCallContext context)
        {
            CompanyMessage companyMessage = request.Company;

            Company upCompany = new Company()
            {
                Id = companyMessage.Id,
                Name = companyMessage.Name,
                Sity = companyMessage.Sity,
                IncomeYear = companyMessage.IncomeYear,
            };

            CrudCompany crudCompany = new CrudCompany(_db);

            crudCompany.Update(upCompany);

            return Task.FromResult(new MessageReply() { Text = "Update compony." });
        }

        public override Task<MessageReply> DeleteCompany(DeleteCompanyRequest request, ServerCallContext context)
        {
            CrudCompany crudCompany = new CrudCompany(_db);
            crudCompany.Delete(request.Id);
            return Task.FromResult(new MessageReply() { Text = "Remove compony."});
        }
    }
}
