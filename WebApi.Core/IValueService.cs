namespace WebApi.Core
{
    public interface IValueService
    {
        string[] GetValues();

        string GetValue(int valueId);
    }
}