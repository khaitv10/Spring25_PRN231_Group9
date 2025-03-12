using BOs.Models;
using BOs.RequestModels.Child;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.User;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels.VaccineStock;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repository.Repositories.AppointmentRepositories;
using Repository.Repositories.AppointmentServiceRepository;
using Repository.Repositories.AuthRepositories;
using Repository.Repositories.ChildRepositories;
using Repository.Repositories.DoseRecordRepositories;
using Repository.Repositories.DoseScheduleRepositories;
using Repository.Repositories.PaymentRepositories;
using Repository.Repositories.ServiceRepository;
using Repository.Repositories.UserRepositories;
using Repository.Repositories.VaccineRepositories;
using Repository.Repositories.VaccineStockRepositories;
using Repository.ServiceVaccine;
using Repository.ServiceVaccineRepository;
using Service.Mapper;
using Service.Service.AppointmentService;
using Service.Service.AppointmentServices;
using Service.Service.ChildServices;
using Service.Service.DoseRecordServices;
using Service.Service.DoseScheduleServices;
using Service.Service.PaymentServices;
using Service.Service.ServiceService;
using Service.Service.VaccineServices;
using Service.Service.VaccineStockServices;
using Service.Services.AuthService;
using Service.Services.EmailServices;
using Service.Services.UserServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var modelBuilder = new ODataConventionModelBuilder();

modelBuilder.EntitySet<UserInfoResponseModel>("users");
modelBuilder.EntityType<UserInfoResponseModel>().HasKey(n => n.Id);

modelBuilder.EntitySet<ChildResponseModel>("children");
modelBuilder.EntityType<ChildResponseModel>().HasKey(c => c.Id);

modelBuilder.EntitySet<DoseRecordResponseModel>("records");
modelBuilder.EntityType<DoseRecordResponseModel>().HasKey(c => c.Id);

modelBuilder.EntitySet<VaccineInfoResponseModel>("vaccine");
modelBuilder.EntityType<VaccineInfoResponseModel>().HasKey(n => n.Id);

modelBuilder.EntitySet<VaccineStockResponseModel>("vaccine-stock");
modelBuilder.EntityType<VaccineStockResponseModel>().HasKey(c => c.Id);

// Thêm cấu hình OData
builder.Services.AddControllers().AddOData(options =>
    options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100)
           .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Jwt",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Cấu hình JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret is missing"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});




builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MapperProfile));


//========================================== REPOSITORY ===========================================
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChildRepository, ChildRepository>();
builder.Services.AddScoped<IDoseScheduleRepository, DoseScheduleRepository>();
builder.Services.AddScoped<IDoseRecordRepository, DoseReordRepository>();
builder.Services.AddScoped<IVaccineRepository, VaccineRepository>();
builder.Services.AddScoped<IVaccineStockRepository, VaccineStockRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepsitory>();
builder.Services.AddScoped<IServiceVaccineRepository, ServiceVaccineRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentServiceRepository, AppointmentServiceRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();




//=========================================== SERVICE =============================================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IChildService, ChildService>();
builder.Services.AddScoped<IDoseScheduleService, DoseScheduleService>();
builder.Services.AddScoped<IDoseRecordService, DoseRecordService>();
builder.Services.AddScoped<IVaccineStockService, VaccineStockService>();
builder.Services.AddScoped<IVaccineService, VaccineService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseODataBatching();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
