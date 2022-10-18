using DataAccessLayer.Models;
using BussinessLayer.Seeds;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DataBaseContextConnection' not found.");

#region Map Classes Into Appsettings
builder.Services.Configure<Helper>(builder.Configuration.GetSection("PATHS"));
#endregion


#region Add CORS
builder.Services.AddCors();
#endregion


builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataBaseContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));

#region Mapped Interfaces Implementations 
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<ITasksRepository, TasksRepository>();
#endregion



var app = builder.Build();


#region Seeding Roles & User
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
await DefaultRoles.SeedRole(roleManager);
await DefaultUsers.SeedAdminUser(userManager, roleManager);
#endregion


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

#region Allow CORS 
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
#endregion


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
