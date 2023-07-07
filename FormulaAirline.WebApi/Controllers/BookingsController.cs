using FormulaAirline.WebApi.Models;
using FormulaAirline.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;
    private readonly IMessageProducer _messageProducer;

    public static readonly List<Booking> Bookings = new();

    public BookingsController(
        ILogger<BookingsController> logger,
        IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreatingBooking(Booking newBooking)
    {
        if (!ModelState.IsValid) return BadRequest();

        _logger.LogInformation("Creating booking {@Booking}", newBooking);

        Bookings.Add(newBooking);

        _messageProducer.SendingMessage(newBooking);

        return Ok();
    }

}
