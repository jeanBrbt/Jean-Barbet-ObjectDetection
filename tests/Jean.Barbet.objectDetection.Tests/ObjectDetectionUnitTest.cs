namespace Jean.Barbet.objectDetection.Tests;
using System.IO; 
using System.Reflection; 
using System.Text.Json; 
using System.Threading.Tasks; 
using Xunit;

public class ObjectDetectionUnitTest
{
    private readonly string _expectedResultOne = "[{\"Dimensions\":{\"X\":70.86438,\"Y\":82.6473,\"Height\":202.84636,\"Width\":276.5385},\"Label\":\"diningtable\",\"Confidence\":0.9093967},{\"Dimensions\":{\"X\":85.240746,\"Y\":178.4934,\"Height\":181.61374,\"Width\":177.0483},\"Label\":\"chair\",\"Confidence\":0.6334971},{\"Dimensions\":{\"X\":316.7584,\"Y\":66.207275,\"Height\":212.64368,\"Width\":90.03749},\"Label\":\"chair\",\"Confidence\":0.5038318},{\"Dimensions\":{\"X\":211.10083,\"Y\":100.48026,\"Height\":216.61082,\"Width\":133.65247},\"Label\":\"chair\",\"Confidence\":0.3917155}]";
    private readonly string _expectedResultTwo = "[{\"Dimensions\":{\"X\":66.58703,\"Y\":181.48398,\"Height\":59.011986,\"Width\":88.60431},\"Label\":\"car\",\"Confidence\":0.92616254},{\"Dimensions\":{\"X\":252.76392,\"Y\":176.23587,\"Height\":46.623096,\"Width\":95.58128},\"Label\":\"car\",\"Confidence\":0.74657506},{\"Dimensions\":{\"X\":253.5869,\"Y\":178.27357,\"Height\":35.451916,\"Width\":47.316875},\"Label\":\"car\",\"Confidence\":0.31115672}]";
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenesAsync(imageScenesData);
        
        Assert.Equal(_expectedResultOne,JsonSerializer.Serialize(detectObjectInScenesResults[0].Box));
        Assert.Equal(_expectedResultTwo, JsonSerializer.Serialize(detectObjectInScenesResults[1].Box));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
} 