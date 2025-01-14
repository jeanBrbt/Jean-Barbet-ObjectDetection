using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Jean.Barbet.objectDetection;


class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 1)
        {
            System.Console.WriteLine("Usage: dotnet run <scenesDirectoryPath>");
            return;
        }

        string scenesDirectoryPath = args[0];


        if (!Directory.Exists(scenesDirectoryPath))
        {
            System.Console.WriteLine($"Scenes directory not found: {scenesDirectoryPath}");
            return;
        }


        var imagesSceneData = new List<byte[]>();

        foreach (var imagePath in Directory.EnumerateFiles(scenesDirectoryPath))
        {
            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);
            imagesSceneData.Add(imageBytes);
        }

        var objectDetection = new ObjectDetection();
        var detectObjectInScenesResults = await objectDetection.DetectObjectInScenesAsync(imagesSceneData);

 
        foreach (var objectDetectionResult in detectObjectInScenesResults) 
        { 
            System.Console.WriteLine($"Box: {JsonSerializer.Serialize(objectDetectionResult.Box)}");
        } 
    }
}
