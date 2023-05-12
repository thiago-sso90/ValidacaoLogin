
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ValidacaoLogin.Persistencia;

namespace ValidacaoLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<UsuarioDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("LoginDeUsuario"));
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LoginDeUsuario.Api",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Thiago De Souza",
                        Email = "thiagobarbosa@pmenos.com.br",
                        Url = new Uri ("https://github.com/thiago-sso90")
                    }
                });
                var xmlPasta = "ValidacaoLogin.xml";
                var xmlCaminho = Path.Combine(AppContext.BaseDirectory, xmlPasta);
                c.IncludeXmlComments(xmlCaminho);
            } );

            //builder.Services.AddCors();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
          

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}