using Microsoft.AspNetCore.Mvc;
using UnitConverter.API.DTOs;
using UnitConverter.Domain.Enums;
using UnitConverter.Services.Interfaces;

namespace UnitConverter.API.Controllers
{
    /// <summary>
    /// Endpoints for converting numeric values between units of measurement.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ConversionsController(IConversionService conversionService, ILogger<ConversionsController> logger) : ControllerBase
    {
        /// <summary>
        /// Converts a numeric value from one unit of measurement to another.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/conversions/convert
        ///     {
        ///        "category": "Length",
        ///        "fromUnit": "meters",
        ///        "toUnit": "feet",
        ///        "value": 10
        ///     }
        ///
        /// Supported categories: <c>Length</c>, <c>Temperature</c>, and <c>Weight</c>.
        /// Unit names are case-insensitive.
        /// </remarks>
        /// <param name="request">The conversion request containing the category, source unit, target unit, and value.</param>
        /// <returns>The converted value wrapped in a standard API response envelope.</returns>
        /// <response code="200">Returns the converted value.</response>
        /// <response code="400">The request is invalid (missing fields, unknown category, or unsupported unit/conversion).</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("convert")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApiResponse<ConversionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            logger.LogInformation("Convert endpoint hit for category {Category}", request.Category);
            if (!Enum.TryParse<ConversionCategory>(request.Category, ignoreCase: true, out var category)
                || !Enum.IsDefined(category))
            {
                logger.LogWarning("Invalid conversion category received: {Category}", request.Category);
                var validCategories = string.Join(", ", Enum.GetNames<ConversionCategory>());
                return BadRequest(ApiResponse<ConversionResponse>.Failure(
                    $"Category '{request.Category}' is not supported.Some valid categories are: {validCategories}."));
            }

            double result = conversionService.Convert(category, request.FromUnit, request.ToUnit, request.Value);

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

            logger.LogInformation("Conversion completed successfully: {Result}", result);

            return Ok(response);
        }
    }
}


