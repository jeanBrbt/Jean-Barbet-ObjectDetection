using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Mvc;
using Jean.Barbet.objectDetection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


app.MapPost("/ObjectDetection", async ([FromForm] IFormFileCollection files) => 
{ 
    if (files.Count < 1)
        return Results.BadRequest();
    using var sceneSourceStream = files[0].OpenReadStream();
    using var sceneMemoryStream = new MemoryStream();
    sceneSourceStream.CopyTo(sceneMemoryStream);
    var imageSceneData = sceneMemoryStream.ToArray(); 

 
    var objectDetection = new ObjectDetection();
    var detectionResults = await objectDetection.DetectObjectInScenesAsync(new List<byte[]> { imageSceneData });
 
    var result = detectionResults[0];
    using var image = Image.FromStream(new MemoryStream(result.ImageData));
    using var outputMemoryStream = new MemoryStream();
    image.Save(outputMemoryStream, ImageFormat.Jpeg);
    var imageData = outputMemoryStream.ToArray();
    return Results.File(imageData, "image/jpg"); 
 
}).DisableAntiforgery(); 

app.Run();

