using Microsoft.AspNetCore.Mvc;
using UnitConverter.API.DTOs;
using UnitConverter.Services.Interfaces;

namespace UnitConverter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionsController(IConversionService conversionService) : ControllerBase

    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test successful!");
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            var result = conversionService.Convert(request.category, request.FromUnit, request.ToUnit, request.Value);
            return Ok(result);
        }
    }
}
