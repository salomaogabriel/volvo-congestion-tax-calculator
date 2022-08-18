using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Congestion.API.Models;
using Congestion.API.Services;
using congestion.calculator;


namespace Congestion.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        public IActionResult Calculate(CalculatorRequest request)
        {
            var vehicle = VehicleFactory.CreateVehicle(request.vehicleType);
            var dateTimes = StringToDateTimeConverter.GetDateTimes(request.DateTimes);
            var calculator = new CongestionTaxCalculator(request.City);
            var tax = calculator.GetTax(vehicle, dateTimes);
            var response = new CalculatorResponse()
            {
                Vehicle = vehicle.GetVehicleType(),
                City = request.City,
                TaxAmount = tax
            };

            return Ok(response);
        }

    }
}
