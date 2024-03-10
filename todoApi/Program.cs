using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>();
 builder.Services.AddControllers();
 builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ToDoDbContext>();
builder.Services.AddCors(option => option.AddPolicy("AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));


var app = builder.Build();
if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

app.MapControllers();
//שליפה
app.MapGet("/", async (ToDoDbContext db) =>
{
    var y = await db.Items.ToListAsync();
    return y;
});



app.MapPost("/{name}", async ( string name,ToDoDbContext db) =>
{
    var data=await db.Items.ToListAsync();
   Item i=new Item();
   i.Name=name;
   i.IsComplete=false;
    db.Items.Add(i);
    await db.SaveChangesAsync();
    return Results.Ok(data);    
});
//מחיקה
app.MapDelete("/{id}", async (int id, ToDoDbContext db) =>
{
    if (await db.Items.FindAsync(id) is Item todo)
    {
        db.Items.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
//עדכון
app.MapPut("/{id}/{isComplete}", async (int id, bool isComplete, ToDoDbContext db) =>
{
    var todo = await db.Items.FindAsync(id);

    if (todo is null) return Results.NotFound();
             todo.IsComplete=isComplete;
             await db.SaveChangesAsync();
             return Results.Ok(db.Items);
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");  
app.Run();

