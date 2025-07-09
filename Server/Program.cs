//using BL;
//using BL.Api;
//using BL.Services;
//using Dal.Do;

////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.
////builder.Services.AddSingleton<Ibl, BlManager>();
////builder.Services.AddControllers();
////// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseHttpsRedirection();

////app.UseAuthorization();

////app.MapControllers();

////app.Run();





////namespace ProjectMain
////{
////    public class Program
////    {
////        public static void Main(string[] args)
////        {
////            var builder = WebApplication.CreateBuilder(args);

////            builder.Services.AddSingleton<Ibl, BlManager>();
//// //           builder.Services.AddSingleton<EmailService>(provider =>
//// //         new EmailService(
//// //         Configuration["EmailSettings:SmtpServer"],
//// //         int.Parse(Configuration["EmailSettings:SmtpPort"]),
//// //         Configuration["EmailSettings:SenderEmail"],
//// //         Configuration["EmailSettings:SenderPassword"],
//// //         Configuration["EmailSettings:AdminEmail"]

//// //    )
//// //);
////            builder.Services.AddControllers();

////            builder.Services.AddEndpointsApiExplorer();
////            builder.Services.AddSwaggerGen();
////            builder.Services.AddCors(c => c.AddPolicy("AllowAll",
////              option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

////            var app = builder.Build();
////            app.UseCors("AllowAll");
////            app.UseStaticFiles();
////            if (app.Environment.IsDevelopment())
////            {
////                app.UseSwagger();
////                app.UseSwaggerUI();
////            }
////            app.UseHttpsRedirection();

////            app.UseAuthorization();


////            app.MapControllers();

////            app.Run();
////        }
////    }
////}
//using Microsoft.EntityFrameworkCore;
//using ProjectMain;

//namespace ProjectMain
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            builder.Services.AddSingleton<Ibl, BlManager>();

//            // הוספת Entity Framework עם SQLite
//            builder.Services.AddDbContext<dbcontext>(options =>
//                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            builder.Services.AddCors(c => c.AddPolicy("AllowAll",
//                option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

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
using BL;
using BL.Api;
using BL.Services;
using Dal.Do;
using Microsoft.EntityFrameworkCore;
using ProjectMain;

namespace ProjectMain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // הגדרת הפורט עבור Render - זה חשוב מאוד!
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

            Console.WriteLine($"Configuring application to run on port: {port}");

            builder.Services.AddSingleton<Ibl, BlManager>();

            // שינוי מ-SQLite ל-PostgreSQL
            builder.Services.AddDbContext<dbcontext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(c => c.AddPolicy("AllowAll",
                option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            var app = builder.Build();

            Console.WriteLine("Application built successfully");

            // יצירת בסיס הנתונים אם לא קיים
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<dbcontext>();
                try
                {
                    Console.WriteLine("Attempting to connect to database...");
                    context.Database.EnsureCreated();
                    Console.WriteLine("Database connection successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database connection failed: {ex.Message}");
                    // לא נעצור את האפליקציה בגלל זה
                }
            }

            app.UseCors("AllowAll");
            app.UseStaticFiles();

            // הסרת HTTPS redirect כי Render מטפל בזה
            // app.UseHttpsRedirection();

            // הצגת Swagger גם בפרודקשן לבדיקות
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();
            app.MapControllers();

            // הוספת endpoints לבדיקה
            app.MapGet("/", () => "Mindy & Tzippy API is running successfully!");
            app.MapGet("/health", () => "OK");

            Console.WriteLine("Starting application...");
            app.Run();
        }
    }
}
