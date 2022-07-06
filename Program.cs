var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//for swagger document
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "xyz Api",

        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


//get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//services
//dependency injection
builder.Services.AddScoped<SendMail>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IMainCategoryService, MainCategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAgreementService, AgreementService>();
builder.Services.AddScoped<IShoppingDetailService, ShoppingDetailService>();
builder.Services.AddScoped<IShoppingService, ShoppingService>();
builder.Services.AddScoped<IShoppingAgreementService, ShoppingAgreementService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();


var lockoutOptions = new LockoutOptions()
{
    AllowedForNewUsers = true,
    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
    MaxFailedAccessAttempts = 10,
};

//user identity and password işlemleri
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 6;
    options.Lockout = lockoutOptions;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
