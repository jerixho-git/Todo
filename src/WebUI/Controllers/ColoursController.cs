using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Common.Mappings;
using Todo_App.Application.Common.Models;
using Todo_App.Domain.Enums;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.WebUI.Controllers;

public class ColoursController : ApiControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ColourDto>> GetColours()
    {
        var colourDtos = Colour.GetSupportedColours()
        .Select(c => new ColourDto
        {
            Name = c.Name,
            Code = c.Code
        });

        return Ok(colourDtos);
    }
}
