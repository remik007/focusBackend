using FluentValidation;
using FluentValidation.AspNetCore;
using FocusAPI;
using FocusAPI.Data;
using FocusAPI.Middleware;
using FocusAPI.Models;
using FocusAPI.Models.Validators;
using FocusAPI.Services;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var authenticationSettings = new AuthenticationSettings();
var appSettings = new AppSettings();
var emailSettings = new EmailSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Configuration.GetSection("AppSettings").Bind(appSettings);
builder.Configuration.GetSection("EmailSettings").Bind(emailSettings);
// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FocusApiPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FocusDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<ITripCategoryService, TripCategoryService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ISubPageService, SubPageService>();
builder.Services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton(emailSettings);
builder.Services.AddScoped<FocusSeeder>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

//Seeder
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<FocusSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseCors("FocusApiPolicy");
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
