using System.Reflection;
using System.Web.Http.Filters;
using log4net;

namespace WebAPI.Hmac.Filters
{
    public class UnhandleExceptionAttribute : ExceptionFilterAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(HttpActionExecutedContext context)
        {
            Log.Error("Internal server error", context.Exception);
        }
    }
}