using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> Get() {
            _logger.LogInfo("He is the info from our values controller");
            _logger.LogDebug("Here is a debug message from our values controller");
            _logger.LogWarn("Here us a warn message from our values controller");
            _logger.LogError("Here is an error message from our values controller");

            return new string[] { "Value1", "Value2" };
        }
        
        
    }
}
