namespace FRONTEND;

public static class BackendConstants
{
    private const string TodoBaseUrl = "http://10.0.2.2:5036/api/todo";
    public static readonly string TodoCreateUrl = TodoBaseUrl + "/create";
    public static readonly string TodoReadUrl = TodoBaseUrl + "/read";
    public static readonly string TodoUpdateUrl = TodoBaseUrl + "/update";
    public static readonly string TodoDeleteUrl = TodoBaseUrl + "/delete";
}