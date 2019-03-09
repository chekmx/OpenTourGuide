namespace OpenTourInterfaces
{
    public interface ILocation
    {
        double Altitude { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}