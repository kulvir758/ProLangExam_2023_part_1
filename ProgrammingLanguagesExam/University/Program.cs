using Microsoft.EntityFrameworkCore;
using System;
using University.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UniversityDbContext>(options => options.UseInMemoryDatabase("UniversityDatabase"));
// builder.Services.AddSingleton<UniversityDbContext>();

builder.Services.AddControllers();

var app = builder.Build();
AddSeedData(app);
app.MapControllers();

app.Run();


static void AddSeedData(IHost app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<UniversityDbContext>();

    db.Teachers.AddRange(new()
    {
        Id = 1,
        Age = 30,
        FirstName = "James",
        LastName = "Bond",
    }, new()
    {
        Id = 2,
        Age = 45,
        FirstName = "Andrzej",
        LastName = "Go�ota",
    });


    db.Skills.AddRange(new()
        {
            Id = 1,
            Name = "Fighting",
            TeacherId = 1
        },
        new()
        {
            Id = 2,
            Name = "Boxing",
            TeacherId = 2
        },
        new()
        {
            Id = 3,
            Name = "Dancing",
            TeacherId = 2
        }
    );

    db.SaveChanges();
}