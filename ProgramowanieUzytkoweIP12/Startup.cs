using CQRS;
using CQRS.Authors;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using Model.DTO;
using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramowanieUzytkoweIP12
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Wzorce projektowe w API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });

            services.AddDbContext<Database>();
            //repository
            services.AddScoped<BooksRepository>();
            services.AddScoped<AuthorRepository>();
            //CQRS
            services.AddScoped<CommandBus>();
            services.AddScoped<QueryBus>();
            services.AddScoped<IQueryHandler<GetBooksQuery, List<BookDTO>>, GetBooksQueryHandler>();
            services.AddScoped<IQueryHandler<GetBookQuery, BookDTO>, GetBookQueryHandler>();
            services.AddScoped<ICommandHandler<AddBookCommand>, AddBookCommandHandler>();
            services.AddScoped<ICommandHandler<AddRateToBookCommand>, AddRateToBookCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteBookCommand>, DeleteBookCommandHandler>();        
            services.AddScoped<IQueryHandler<GetAuthorsQuery, List<AuthorDTO>>, GetAuthorsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAuthorQuery, AuthorDTO>, GetAuthorQueryHandler>();
            services.AddScoped<ICommandHandler<AddAuthorCommand>, AddAuthorCommandHandler>();
            services.AddScoped<ICommandHandler<AddRateToAuthorCommand>, AddRateToAuthorCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteAuthorCommand>, DeleteAuthorCommandHandler>();
            //MediatR
            var mediatR = AppDomain.CurrentDomain.Load("MediatRCQRS");
            services.AddMediatR(mediatR);

            services.AddControllers(); 
            services.AddSwaggerGen(); //konfiguracja swaggera
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(); //konfiguracja swaggera
            app.UseSwaggerUI(); //konfiguracja swaggera

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
