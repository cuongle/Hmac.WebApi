namespace Hmac.Application.Impl
{
    public class ValueService : IValuesService
    {
        public string[] GetValues()
        {
            return new[] { "value1", "value2" };
        }

        public string GetValue(int valueId)
        {
            return "value";
        }
    }
}
