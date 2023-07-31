using CRUDGrpcService.Models;
using CRUDGrpcService.Services;
using Microsoft.EntityFrameworkCore;

namespace CRUDGrpcService
{
    public class Startup
    {

        private string CONN_STR_DB = "Data Source=DESKTOP-77URCG8;Initial Catalog=UsersCRUDDb;Integrated Security=True;TrustServerCertificate=True";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(CONN_STR_DB));
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapGrpcService<CrudCompanyService>();
                endpoint.MapGrpcService<CrudEmployeeService>();
                endpoint.MapFallbackToFile("index.html");
            });
        }
    }
}
