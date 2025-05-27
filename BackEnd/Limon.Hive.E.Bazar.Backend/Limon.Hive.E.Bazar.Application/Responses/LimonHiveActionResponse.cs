namespace Limon.Hive.E.Bazar.Application.Responses;

public class LimonHiveActionResponse
{
    public bool IsSuccessful { get; set; }

    public string? Message { get; set; }

}
public class LimonHiveActionResponse<T> : LimonHiveActionResponse where T : class
{
    public T Model { get; set; }
}
