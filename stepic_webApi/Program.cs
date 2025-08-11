using stepic.Services;
using stepic.Services.ADO.NET;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<CertificatesService>();
builder.Services.AddTransient<CommentsService>();
builder.Services.AddTransient<CoursesService>();
builder.Services.AddTransient<IUsersService, stepic.Services.EF.UsersService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();