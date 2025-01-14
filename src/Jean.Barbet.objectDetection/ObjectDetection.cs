namespace Jean.Barbet.objectDetection;

public class ObjectDetection 
{ 
    public async Task<IList<ObjectDetectionResult>> 
        DetectObjectInScenesAsync(IList<byte[]> imagesSceneData) 
    { 
        await Task.Delay(1000); 
        var tasks = imagesSceneData.Select(imageData => Task.Run(() => DetectObjectInScene(imageData))).ToList();
        var results = await Task.WhenAll(tasks);
        return results.ToList();

    }

    private ObjectDetectionResult DetectObjectInScene( byte[] imageSceneData)
    {

        var tinyYolo = new Yolo(); 
        var result = tinyYolo.Detect(imageSceneData);
       // throw new NotImplementedException(); 

        return new ObjectDetectionResult()
        {
            ImageData = result.ImageData,
            Box = result.Boxes
        };
        
    }

} 