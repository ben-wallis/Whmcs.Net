namespace Whmcs.Services
{
    public interface IJSONService
    {
        T DeserialiseJSON<T>(string inputJSON);
    }
}