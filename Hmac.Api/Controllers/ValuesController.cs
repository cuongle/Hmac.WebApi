using System.Web.Http;
using Hmac.Api.Filters;
using Hmac.Application;

namespace Hmac.Api.Controllers
{
    [Authenticate]
    public class ValuesController : ApiController
    {
        private readonly IValuesService _valueService;

        public ValuesController(IValuesService valueService)
        {
            _valueService = valueService;
        }

        public string[] Get()
        {
            return _valueService.GetValues();
        }

        public string Get(int id)
        {
            return _valueService.GetValue(id);
        }

        public void Post(string value)
        {
        }

        public void Put(int id, string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
