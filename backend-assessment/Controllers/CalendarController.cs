using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CPS.Models;
using Newtonsoft.Json;

namespace CPS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CalendarController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CalendarController> _logger;

        public CalendarController(IHttpClientFactory httpClientFactory, ILogger<CalendarController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// This Endpoint Gets a list of Calendar Results.
        /// </summary>
        /// <remarks>
        /// **Sample Response** (*ColorCode property name has been updated to HexaDecimal*)**:** 
        /// 
        ///     {
        ///      "hexaDecimal": "#b096c6",
        ///      "parentID": 0,
        ///      "status": "ACTIVE",
        ///      "tagID": 0,
        ///      "type": "categories",
        ///      "calendarID": 1149,
        ///      "calendarName": "Accountability Redesign"
        ///     }
        ///    
        /// </remarks>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">OK</response>
        /// <response code="500">Failed to fetch calendars</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CalendarList>>> GetCalendarsList()
        {
            try
            {
                // Create an HttpClient using the factory
                var httpClient = _httpClientFactory.CreateClient();

                // Make a request to the CPS API endpoint
                var response = await httpClient.GetAsync("https://api.cps.edu/Calendar/cps/calendarslist");
                response.EnsureSuccessStatusCode();

                // Read the response content
                var content = await response.Content.ReadAsStringAsync();

                // Converted the response to a deserialized object in order to change the model property for colorCode to hexadecimal
                var calendars = JsonConvert.DeserializeObject<List<CalendarList>>(content);


                return calendars;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to fetch calendars from CPS API.");
                return Problem("Failed to fetch calendars.", statusCode: 500);
            }
        }
    }
}