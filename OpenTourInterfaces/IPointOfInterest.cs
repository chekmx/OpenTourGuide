namespace OpenTourInterfaces
{
    public interface IPointOfInterest
    {
        ILocation Location { get; set; }
        string Name { get; set; }
    }
}