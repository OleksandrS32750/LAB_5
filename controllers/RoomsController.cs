namespace Task4.Controllers;

using Task4.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRooms(
        [FromQuery] string? buildingCode,
        [FromQuery] int? minCapacity,
        [FromQuery] bool? isActive)
    {
        var rooms = DataStore.Rooms.AsEnumerable();

        if (!string.IsNullOrEmpty(buildingCode))
        {
            rooms = rooms.Where(r => r.BuildingCode == buildingCode);
        }

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
        }


        if (isActive.HasValue)
        {
            rooms = rooms.Where(r => r.IsActive == isActive.Value);
        }

        return Ok(rooms.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound("Room with ID '" + id + "' does not exist");
        }

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoomsByBuilding(string buildingCode)
    {
        var rooms = DataStore.Rooms.Where(r => r.BuildingCode == buildingCode).ToList();

        if (rooms.Count == 0)
        {
            return NotFound("No rooms found for building code '" + buildingCode + "'");
        }

        return Ok(rooms);
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room newRoom)
    {
        var error = RoomValidator.Validate(newRoom);

        if (error != null)
        {
            return BadRequest(error);
        }

        newRoom.Id = DataStore.NextRoomId;
        DataStore.Rooms.Add(newRoom);

        return CreatedAtAction(nameof(GetRoom), new { id = newRoom.Id }, newRoom);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound("Room with ID '" + id + "' does not exist");
        }

        var error = RoomValidator.Validate(updatedRoom);
        if (error != null)
        {
            return BadRequest(error);
        }

        room.Name = updatedRoom.Name;
        room.BuildingCode = updatedRoom.BuildingCode;
        room.Floor = updatedRoom.Floor;
        room.Capacity = updatedRoom.Capacity;
        room.HasProjector = updatedRoom.HasProjector;
        room.IsActive = updatedRoom.IsActive;

        return Ok(room);
    }
}