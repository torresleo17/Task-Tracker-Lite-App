using Microsoft.EntityFrameworkCore;
using Task_Tracker_Lite.Data;
using Task_Tracker_Lite.Repository;
using Task_Tracker_Lite.Services;

var builder = WebApplication.CreateBuilder(args);

//Add conection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=tasktrackerlite.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

//Controllers 
builder.Services.AddControllers();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Services Registration
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IListService, ListService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Repositories 
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IListItemRepository, ListItemRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();

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


app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
