using BLL_Solution.Profiles;
using BLL_Solution.Services.Classes;
using BLL_Solution.Services.Interfaces;
using DAL_Solution.Data.Contexts;
using DAL_Solution.Models.IdentityModels;
using DAL_Solution.Repositories.DepartmentRepo;
using DAL_Solution.Repositories.EmployeeRepo;
using DAL_Solution.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL_Solution.Utilties;

namespace PL_Solution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            // Add services to the Dependancy Injection (DI) container.
            // Regester in Database
            builder.Services.AddControllersWithViews(option =>
                {
                    option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
                {
                    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                    option.UseLazyLoadingProxies(); // Enable lazy loading for navigation properties
                });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    //password configrations
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredUniqueChars = 3; // Minmam UniqueCharacters Count in password
                                                              //user configrations
                    options.User.RequireUniqueEmail = true; // onley one user for each email
                                                            //lockout
                    options.Lockout.AllowedForNewUsers = true; // 
                    options.Lockout.MaxFailedAccessAttempts = 5; // inter password times incorrect
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(2); // Lock Account After 2 day without using

                }).AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/LogIn";
                options.LogoutPath = "/Account/LogOut";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // for cookie expire time of login
            });
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=LogIn}/{id?}");

            #endregion

            app.Run();
        }
    }
}
