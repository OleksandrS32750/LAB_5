namespace Task4.Controllers;

using Task4.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation reservation)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
        {
            return BadRequest("Room with ID '" + reservation.RoomId + "' does not exist");
        }

        var error = ReservationValidator.Validate(reservation);
        if (error != null)
        {
            return BadRequest(error);
        }

        reservation.Id = DataStore.NextReservationId;
        DataStore.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
    }

    [HttpGet("{id}")]
    public IActionResult GetReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound("Reservation with ID '" + id + "' does not exist");
        }

        return Ok(reservation);
    }
}