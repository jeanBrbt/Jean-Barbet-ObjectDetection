namespace Jean.Barbet.objectDetection;

public record ObjectDetectionResult 
{ 
    public byte[] ImageData { get; set; } 
    public IList<BoundingBox> Box { get; set; } 
} 