namespace Task4.Models;

public class ReservationValidator
{
    public static void ValidateOrganizerName(string organizerName)
    {
        if (string.IsNullOrEmpty(organizerName))
        {
            throw new ArgumentException("Organizer name cannot be null");
        }
    }

    public static void ValidateTopic(string topic)
    {
        if (string.IsNullOrEmpty(topic))
        {
            throw new ArgumentException("Topic cannot be null");
        }
    }

    public static void ValidateDate(DateOnly date)
    {
        if (date == default)
        {
            throw new ArgumentException("Date cannot be null");
        }
    }

    public static void ValidateTime(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            throw new ArgumentException("Start time must be before end time");
        }
    }

    public static string? Validate(Reservation reservation)
    {
        if (string.IsNullOrEmpty(reservation.OrganizerName))
        {
            return "Organizer name cannot be null";
        }

        if (string.IsNullOrEmpty(reservation.Topic))
        {
            return "Topic cannot be null";
        }

        if (reservation.Date == default)
        {
            return "Date cannot be null";
        }

        if (reservation.StartTime >= reservation.EndTime)
        {
            return "Start time must be before end time";
        }

        return null;
    }
}