using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Autofac.Extensions.DependencyInjection;
using Autofac.Core;
using Autofac;
using System.Reflection;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllers();
//解决跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader()
       );
});

//autofac批量注册服务
string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
string repositoryDllPath = Path.Combine(baseDirectory, "Repository.dll");
string serviceDllPath = Path.Combine(baseDirectory, "Service.dll");


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<DbContext>().As<DbContext>().InstancePerLifetimeScope();
    // 注册你的服务
    containerBuilder.RegisterAssemblyTypes(Assembly.LoadFrom(repositoryDllPath), Assembly.LoadFrom(serviceDllPath))
    .Where(t => (t.Name.EndsWith("Repository") || t.Name.EndsWith("Service")) && t.IsAbstract == false)
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();
    //containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
    //.Where(t => (t.Name.EndsWith("Repository") || t.Name.EndsWith("Service")) && t.IsAbstract == false)
    //.AsImplementedInterfaces()
    //.InstancePerLifetimeScope();
    // 注册你的控制器
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

