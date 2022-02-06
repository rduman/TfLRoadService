namespace TfL.RoadManagement.TFL.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Type entityType, IReadOnlyCollection<string> value)
        : base(value.Count > 1
            ? $"{string.Join(" ", value)} are not valid {entityType.Name.ToLower()}s"
            : $"{value.FirstOrDefault()} is not a valid {entityType.Name.ToLower()}")
    {
    }


}