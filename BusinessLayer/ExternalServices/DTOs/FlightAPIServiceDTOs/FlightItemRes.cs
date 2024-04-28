namespace BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;

public class FlightItemRes
{
    public string DepartureStation { get; set; }
    public string ArrivalStation { get; set; }
    public string FlightCarrier { get; set; }
    public string FlightNumber { get; set; }
    public double Price { get; set; }
}