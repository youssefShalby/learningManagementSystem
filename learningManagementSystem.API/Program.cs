
using learningManagementSystem.API.Filter;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Option Pattern

var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
builder.Services.AddSingleton(smtpSettings);

var stripeSettings = builder.Configuration.GetSection("StripeSettings").Get<StripeSettings>();
builder.Services.AddSingleton(stripeSettings);

#endregion

#region CustomServices

string dBConnectionString = builder.Configuration.GetConnectionString("LMS_Db");
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(dBConnectionString));

builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<ICoursePaymentRepo, CoursePaymentRepo>();
builder.Services.AddScoped<ILessonRepo, LessonRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IStudentCourseRepo, StudentCourseRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IHandlerService, HandlerService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDashboardService, UserService>();
builder.Services.AddScoped<IUserAccountManagmentService, UserService>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<ICommentRelyRepo, CommentRelyRepo>();
builder.Services.AddScoped<IVideoRepo, VideoRepo>();
builder.Services.AddScoped<ICourseAdvantegeRepo, CourseAdvantegeRepo>();
builder.Services.AddScoped<IInstructorRepo, InstructorRepo>();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentReplyService, CommentReplyService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICoursePaymentService, CoursePaymentService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<AccessVideosFilter>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
	option.Password.RequiredLength = 10;
	option.Password.RequireUppercase = true;
	option.Password.RequireDigit = true;

	option.User.RequireUniqueEmail = true;
	option.Lockout.MaxFailedAccessAttempts = 5;
	option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);


}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


//> configure token life time which created by Asp 
builder.Services.Configure<DataProtectionTokenProviderOptions>(TokenOptions.DefaultEmailProvider, options =>
{
	options.TokenLifespan = TimeSpan.FromHours(1);
});

var result = builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

});

result.AddJwtBearer(option =>
{
	option.SaveToken = true;
	option.RequireHttpsMetadata = false;

	var theKey = builder.Configuration["JWT:tokenKey"];
	var keyInBytes = Encoding.UTF8.GetBytes(theKey);
	var key = new SymmetricSecurityKey(keyInBytes);

	option.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["JWT:issuer"],
		ValidateAudience = false,
		//> ValidAudience = builder.Configuration["JWT:audience"],
		IssuerSigningKey = key
	};
});

//> Roles of the System, Authorization Based Role
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", builder => builder.RequireClaim(ClaimTypes.Role, "Admin"));
	options.AddPolicy("Instructor", builder => builder.RequireClaim(ClaimTypes.Role, "Admin", "Instructor"));
	options.AddPolicy("Student", builder => builder.RequireClaim(ClaimTypes.Role, "Admin", "Instructor", "Student"));
});



//> swager
builder.Services.AddSwaggerGen(setup =>
{
	// Include 'SecurityScheme' to use JWT Authentication
	var jwtSecurityScheme = new OpenApiSecurityScheme
	{
		BearerFormat = "JWT",
		Name = "JWT Authentication",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = JwtBearerDefaults.AuthenticationScheme,
		Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};

	setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

	setup.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{ jwtSecurityScheme, Array.Empty<string>() }
	});

});

#endregion


var app = builder.Build();


#region Middleware

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

app.Run();


#endregion
