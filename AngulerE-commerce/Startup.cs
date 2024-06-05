using E_commerce.Domain.Admin;
using E_commerce.Ef.Adminrepository;
using E_commerce.Ef.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace AngulerE_commerce
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration,IWebHostEnvironment webHost)
        {
            Configuration = configuration;
            env = webHost;
        }
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllersWithViews();
            services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<AdminService>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAdminService, AdminService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                  .AddEntityFrameworkStores<UserDbContext>()
                  .AddDefaultTokenProviders();

            services.AddAuthentication(Option =>
            {
                Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    };

                });
        }
        public void configure(WebApplication app,IWebHostEnvironment web)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=UserController}/{action=Index}/{id?}");
            app.UseCors(Option => Option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.Run();
        }

    }
}
