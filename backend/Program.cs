using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SportsAuctionSystem.Data;
using SportsAuctionSystem.Filters;
using SportsAuctionSystem.Repositories;
using SportsAuctionSystem.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//Addrepositories
builder.Services.AddScoped<AuctionService>();
builder.Services.AddScoped<BidsService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<FinanceService>();
builder.Services.AddScoped<AuctionResultsService>();
builder.Services.AddScoped<ReportsService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddHostedService<AuctionStatusUpdateService>();
builder.Services.AddScoped<IPlayerRepository,PlayerRepository>();
builder.Services.AddScoped<IAuctionRepository,AuctionRepository>();
builder.Services.AddScoped<ITeamRepository,TeamRepository>();
builder.Services.AddScoped<IBidsRepository, BidsRepository>();
builder.Services.AddScoped<IAuctionResultsRepository, AuctionResultsRepository>();
builder.Services.AddScoped<IPerformanceReportsRepository, PerformanceReportsRepository>();
builder.Services.AddScoped<IFinanceRepository,FinanceRepository>();
builder.Services.AddScoped<IContractsRepository, ContractsRepository>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotificationRepository,NotificationRepository>();

// Add services to the container.
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IBidsService, BidsService>();
builder.Services.AddScoped<IAuctionResultsService, AuctionResultsService>();
builder.Services.AddScoped<IPerformanceReportsService, PerformanceReportsService>();
builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IContractsService, ContractsService>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Context
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<AddValidationFilter>();
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Add your Angular app URL here
              .AllowAnyHeader() // Allow headers (e.g., Content-Type, Authorization)
              .AllowAnyMethod(); // Allow HTTP methods (GET, POST, PUT, DELETE)
    });
});

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply CORS
app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
