using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mohafezApi.Data;
using mohafezApi.Models;
using mohafezApi.Profils;
using mohafezApi.Services.TablesService;
using mohafezApi.Services.TeacherService;
using mohafezApi.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDBcontext>(
    options =>
    {
        options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
        options.EnableSensitiveDataLogging();
    }
);
//Services
var config = new AutoMapper.MapperConfiguration(
    cfg =>
    {
        cfg.AddProfile(new AutoMapperProfiles());
    }
);
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IUserService, UserService>();
 builder.Services.AddScoped<ITablesService, TablesService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
//  builder.Services.AddScoped<IHomeService, HomeService>();
// builder.Services.AddScoped<IAddsService, AddsService>();
// builder.Services.AddScoped<ICartsService, CartsService>();
// builder.Services.AddScoped< IOrderItemsServices, OrderItemsServices>();
//  builder.Services.AddScoped<IProductsOptionsServices, ProductsOptionsServices>();
// builder.Services.AddScoped<IOrderItemOptionsServices, OrderItemOptionsServices>();
// builder.Services.AddScoped<IOrdersServices, OrdersServices>();
// builder.Services.AddScoped<IOffersServices, OffersServices>();
// builder.Services.AddScoped<IAddressesServices, AddressesServices>();
// builder.Services.AddScoped<IAlertsServices, AlertsServices>();
// builder.Services.AddScoped<IAppConfigServices, AppConfigServices>();

// builder.Services.AddScoped<IMarketsService, MarketService>();
// builder.Services.AddScoped<ICouponService, CouponService>();
// builder.Services.AddScoped<IRateServices, RateServices>();
// builder.Services.AddScoped<IDashboardService, DashboardService>();

//cors
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            name: "AllowOrigin",
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
        );
    }
);

// For Identity
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDBcontext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    }
);

// Adding Authentication
builder.Services
    .AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    // Adding Jwt Bearer
    .AddJwtBearer(
        options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])
                ),
            };
        }
    );




var app = builder.Build();

// Configure the HTTP request pipeline.
 app.UseRouting();
 app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();
