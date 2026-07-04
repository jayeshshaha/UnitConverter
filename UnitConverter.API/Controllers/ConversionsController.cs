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
            double result = conversionService.Convert(request.category, request.FromUnit, request.ToUnit, request.Value);
            
            ConversionResponse conversionResponse = new ConversionResponse
            {
                OriginalValue = request.Value,
                FromUnit = request.FromUnit,
                ToUnit = request.ToUnit,
                ConvertedValue = result
            };

            ApiResponse<ConversionResponse> response = new ApiResponse<ConversionResponse>
            {
                IsSuccess = true,
                Message = "Conversion successful",
                Data = conversionResponse
            };

            return Ok(response);
        }
    }
}
