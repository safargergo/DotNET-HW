using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.BLL.Services;
using LeagueTableApp.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(WebApiProfile));
builder.Services.AddTransient<ILeagueService, LeagueService>();
builder.Services.AddTransient<IMatchService, MatchService>();
builder.Services.AddTransient<ITeamService, TeamService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
