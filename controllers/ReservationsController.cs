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

        // if for example reservation between startTime and endTime already exists for provided room,we want to prevent createing another reservatino for this room (only if it is not cancelled)
        var hasOverlap = DataStore.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.Status != "cancelled" &&
            ((reservation.StartTime >= r.StartTime && reservation.StartTime < r.EndTime) ||
             (reservation.EndTime > r.StartTime && reservation.EndTime <= r.EndTime) ||
             (reservation.StartTime <= r.StartTime && reservation.EndTime >= r.EndTime)));

        if (hasOverlap)
        {
            return Conflict("Reservation overlaps with an existing reservation for this room on the same date");
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

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound("Reservation with ID '" + id + "' does not exist");
        }

        DataStore.Reservations.Remove(reservation);

        return NoContent();
    }
}