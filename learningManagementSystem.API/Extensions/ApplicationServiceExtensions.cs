

namespace learningManagementSystem.API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection ApplicationService(this IServiceCollection service, IConfiguration configuration)
	{
		#region OptionPatterns

		var smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
		service.AddSingleton(smtpSettings);

		var stripeSettings = configuration.GetSection("StripeSettings").Get<StripeSettings>();
		service.AddSingleton(stripeSettings);

		#endregion

		#region ConnectionString

		string dBConnectionString = configuration.GetConnectionString("LMS_Db");
		service.AddDbContext<AppDbContext>(option => option.UseSqlServer(dBConnectionString));

		service.AddHttpContextAccessor();

		#endregion

		#region DepndencyInjection

		service.AddScoped<ICategoryRepo, CategoryRepo>();
		service.AddScoped<ICommentRepo, CommentRepo>();
		service.AddScoped<ICourseRepo, CourseRepo>();
		service.AddScoped<ICoursePaymentRepo, CoursePaymentRepo>();
		service.AddScoped<ILessonRepo, LessonRepo>();
		service.AddScoped<IOrderRepo, OrderRepo>();
		service.AddScoped<IStudentCourseRepo, StudentCourseRepo>();
		service.AddScoped<IUnitOfWork, UnitOfWork>();
		service.AddScoped<IRoleService, RoleService>();
		service.AddScoped<IHandlerService, HandlerService>();
		service.AddScoped<ITokenService, TokenService>();
		service.AddScoped<IEmailService, EmailService>();
		service.AddScoped<IUserService, UserService>();
		service.AddScoped<IUserDashboardService, UserService>();
		service.AddScoped<IUserAccountManagmentService, UserService>();
		service.AddScoped<IStudentRepo, StudentRepo>();
		service.AddScoped<ICommentRelyRepo, CommentRelyRepo>();
		service.AddScoped<IVideoRepo, VideoRepo>();
		service.AddScoped<ICourseAdvantegeRepo, CourseAdvantegeRepo>();
		service.AddScoped<IInstructorRepo, InstructorRepo>();
		service.AddScoped<IRedisService, RedisService>();
		service.AddScoped<ICategoryService, CategoryService>();
		service.AddScoped<ILessonService, LessonService>();
		service.AddScoped<IVideoService, VideoService>();
		service.AddScoped<ICourseService, CourseService>();
		service.AddScoped<ICommentService, CommentService>();
		service.AddScoped<ICommentReplyService, CommentReplyService>();
		service.AddScoped<IPaymentService, PaymentService>();
		service.AddScoped<ICoursePaymentService, CoursePaymentService>();
		service.AddScoped<IOrderService, OrderService>();
		service.AddScoped<ICacheHelper, CacheHelper>();
		service.AddScoped<ICourseDashboardService, CourseService>();
		service.AddScoped<ICourseAccessService, CourseService>();
		service.AddScoped<IOrderDashboardService, OrderService>();
		service.AddScoped<IOrderDashboardRepo, OrderRepo>();
		service.AddScoped<ICourseDashboardRepo, CourseRepo>();
		service.AddScoped<AccessVideosFilter>();

		#endregion

		#region Auth

		service.AddIdentity<ApplicationUser, IdentityRole>(option =>
		{
			option.Password.RequiredLength = 10;
			option.Password.RequireUppercase = true;
			option.Password.RequireDigit = true;

			option.User.RequireUniqueEmail = true;
			option.Lockout.MaxFailedAccessAttempts = 5;
			option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);


		}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


		//> configure token life time which created by Asp 
		service.Configure<DataProtectionTokenProviderOptions>(TokenOptions.DefaultEmailProvider, options =>
		{
			options.TokenLifespan = TimeSpan.FromHours(1);
		});

		var result = service.AddAuthentication(option =>
		{
			option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

		});

		result.AddJwtBearer(option =>
		{
			option.SaveToken = true;
			option.RequireHttpsMetadata = false;

			var theKey = configuration["JWT:tokenKey"];
			var keyInBytes = Encoding.UTF8.GetBytes(theKey);
			var key = new SymmetricSecurityKey(keyInBytes);

			option.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = configuration["JWT:issuer"],
				ValidateAudience = false,
				//> ValidAudience = builder.Configuration["JWT:audience"],
				IssuerSigningKey = key
			};
		});

		//> Roles of the System, Authorization Based Role
		service.AddAuthorization(options =>
		{
			options.AddPolicy("Admin", builder => builder.RequireClaim(ClaimTypes.Role, "Admin"));
			options.AddPolicy("Instructor", builder => builder.RequireClaim(ClaimTypes.Role, "Admin", "Instructor"));
			options.AddPolicy("Student", builder => builder.RequireClaim(ClaimTypes.Role, "Admin", "Instructor", "Student"));
		});

		#endregion

		#region Swagger

		service.AddSwaggerGen(setup =>
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

		#region Model State Validation Exception

		service.Configure<ApiBehaviorOptions>(option =>
		{
			option.InvalidModelStateResponseFactory = actionContext =>
			{
				var errors = actionContext.ModelState
				.Where(error => error.Value?.Errors.Count() > 0)
				.SelectMany(errs => errs.Value?.Errors!)
				.Select(err => err.ErrorMessage)
				.ToArray();

				var errorResponse = new ApiValidationErrorResponse
				{
					Errors = errors
				};

				return new BadRequestObjectResult(errorResponse);
			};
		});

		#endregion

		return service;
	}
}
