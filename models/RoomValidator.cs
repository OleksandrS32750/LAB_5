using Microsoft.AspNetCore.Mvc;

namespace Task4.Models;


public class RoomValidator
{

    public static void ValidateName(string Name)
    {
        if (Name == null)
        {
            throw new ArgumentException("Room name cannot be null");
        }
    }

    public static void ValidateBuildingCode(string BuidlingCode)
    {
        if (BuidlingCode == null)
        {
            throw new ArgumentException("Building code cannot be null");
        }
    }


    public static void ValidateTopic(string Topic)
    {
        if (Topic == null)
        {
            throw new ArgumentException("Topic name cannot be null");
        }
    }

    public static void ValidateOrganizerName(string OrganizerName)
    {
        if (OrganizerName == null)
        {
            throw new ArgumentException("OrganizerName name cannot be null");
        }
    }

    public static void ValidateCapacity(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be a positive integer");
        }
    }

    public static void ValidateDate(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            throw new ArgumentException("Start time must be before end time");
        }
    }

    public static string? Validate(Room room)
    {

        if (string.IsNullOrEmpty(room.Name))
        {
            return "Room name cannot be null";
        }

        if (string.IsNullOrEmpty(room.BuildingCode))
        {
            return "Building code cannot be null";
        }

        if (room.Capacity <= 0)
        {
            return "Capacity must be a positive integer";
        }

        return null;

    }
}