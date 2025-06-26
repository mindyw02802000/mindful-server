using BL;
using BL.Api;
using BL.Services;
using Dal.Do;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddSingleton<Ibl, BlManager>();
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();





//namespace ProjectMain
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            builder.Services.AddSingleton<Ibl, BlManager>();
// //           builder.Services.AddSingleton<EmailService>(provider =>
// //         new EmailService(
// //         Configuration["EmailSettings:SmtpServer"],
// //         int.Parse(Configuration["EmailSettings:SmtpPort"]),
// //         Configuration["EmailSettings:SenderEmail"],
// //         Configuration["EmailSettings:SenderPassword"],
// //         Configuration["EmailSettings:AdminEmail"]

// //    )
// //);
//            builder.Services.AddControllers();

//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            builder.Services.AddCors(c => c.AddPolicy("AllowAll",
//              option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//            var app = builder.Build();
//            app.UseCors("AllowAll");
//            app.UseStaticFiles();
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }
//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}
using Microsoft.EntityFrameworkCore;
using ProjectMain;

namespace ProjectMain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<Ibl, BlManager>();

            // הוספת Entity Framework עם SQLite
            builder.Services.AddDbContext<dbcontext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(c => c.AddPolicy("AllowAll",
                option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            var app = builder.Build();

            app.UseCors("AllowAll");
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}