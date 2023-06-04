using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HairSalonWEB.Models;
using Pomelo.EntityFrameworkCore.MySql;
using HairSalonWEB.Interfaces;
using HairSalonWEB.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Регистрация сервисов
builder.Services.AddDbContext<DataContext>(options =>
{
    string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=hair_salon;Convert Zero Datetime=True";
    string serverVersion = "10.4.25-MariaDB";
    options.UseMySql(MySqlConnectionString, new MySqlServerVersion(serverVersion));
});

builder.Services.AddTransient<IMaster, MasterRepository>();
builder.Services.AddTransient<IAdministrator, AdministratorRepository>();
builder.Services.AddTransient<IClient, ClientRepository>();
builder.Services.AddTransient<ICompany, CompanyRepository>();
builder.Services.AddTransient<IRecord, RecordRepository>();
builder.Services.AddTransient<IProcedure, ProceduresRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
