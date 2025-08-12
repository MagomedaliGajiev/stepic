using stepic.Services;
using stepic.Services.ADO.NET;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICertificatesService, stepic.Services.EF.CertificatesService>();
builder.Services.AddTransient<ICommentsService, stepic.Services.EF.CommentsService>();
builder.Services.AddTransient<ICoursesService, stepic.Services.EF.CoursesService>();
builder.Services.AddTransient<IUsersService, stepic.Services.EF.UsersService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();