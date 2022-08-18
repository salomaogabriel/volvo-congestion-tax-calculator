using congestion.calculator;

namespace Congestion.API.Services
{
    public static class VehicleFactory
    {
      public static Vehicle CreateVehicle(string vehicle)
      {
        return vehicle.ToLower() switch  
         {
        "car" => new Car(),
        "motorcycle" => new Motorcycle(),
        _ => new Car()
    };
      }
    }
}
