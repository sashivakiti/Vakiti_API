global using Vakiti_API.Data;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataCon>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-------------Add CORS Policy----------------
var dbContext = builder.Services.BuildServiceProvider().GetService<DataCon>();
List<string> originList = new List<string>();
if(dbContext != null)
{
    var originData = dbContext.corsOrigins.Where(Origins => Origins.IsActive == 1).ToList();
    if (originData != null && originData.Count > 0)
    {
        originData.ForEach(origin =>
        {
            originList.Add(origin.OriginName);
        });
    }

}

builder.Services.AddCors(cors => cors.AddDefaultPolicy(defaultCorsPolicy =>
{
    defaultCorsPolicy.WithOrigins(originList.ToArray());
    defaultCorsPolicy.AllowAnyMethod();
    defaultCorsPolicy.AllowAnyHeader();
}));

//To set individually for a controller, we can use below policy name in that controller
builder.Services.AddCors(cors => cors.AddPolicy("CORSpolicy", corspolicy =>
 {
     corspolicy.WithOrigins("https://localhost:7252");
     corspolicy.AllowAnyHeader();
     corspolicy.AllowAnyMethod();
 }));

//or 
//Add CORS by HARD CODING in below 2 ways
//1) default way
//builder.Services.AddCors(cors => cors.AddDefaultPolicy(defaultCorsPolicy =>
//{
//    defaultCorsPolicy.WithOrigins("https://localhost:7252;http://localhost:5252");
//    defaultCorsPolicy.AllowAnyMethod();
//    defaultCorsPolicy.AllowAnyHeader();
//}));

//2) Using a Policy name, Ex : CORSpolicy
//builder.Services.AddCors(cors => cors.AddPolicy("CORSpolicy",corspolicy =>
//{
//    corspolicy.WithOrigins("http://localhost:1234");
//    corspolicy.AllowAnyHeader();
//    corspolicy.AllowAnyMethod();
//}));

//------------------End CORS Policy------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//enable CORS policy
app.UseCors(); // for default CORS policy
app.UseCors("CORSpolicy"); // for names CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();
