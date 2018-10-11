using Abdes.Data;
using Abdes.Languages;
using Microsoft.AspNetCore.Mvc;

namespace Abdes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbdesController : ControllerBase
    {
        // GET api/values/5
        [HttpGet]
        public dynamic Get(string language, string code)
        {
            var results = new Results();

            switch (language.ToUpper())
            {
                case "CSHARP":
                    results = (new CSharp()).Results(code);
                    break;

                default:
                    results.Status = Enum.Status.error;
                    results.Message = MessageResult.unkownlanguage;
                    break;
            }

            return results;
        }
    }
}
