using GestionePraticheApiDonini.DTOs.Input;
using GestionePraticheApiDonini.DTOs.Output;
using GestionePraticheApiDonini.Requests;
using GestionePraticheApiDonini.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionePraticheApiDonini.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PraticaController : ControllerBase
{
    private readonly IPraticaService _praticaService;
    private readonly ILogger<PraticaController> _logger;

    public PraticaController(ILogger<PraticaController> logger, IPraticaService praticaService)
    {
        _logger = logger;
        _praticaService = praticaService;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePratica([FromForm] CreatePraticaRequest model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _praticaService.CreatePratica(
            new CreatePraticaDTO()
            {
                Attachment = model.Attachment,
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate
            }
        );
        return Ok(result.Id);
    }

    [HttpPatch]
    [Route("{id}", Name = nameof(UpdatePratica))]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePratica([FromRoute] int id, [FromBody] UpdatePraticaRequest model)
    {
        var res = new UpdatedPraticaDTO();
        try
        {
            res = await _praticaService.UpdatePratica(new UpdatePraticaDTO()
            {
                Attachment = model.Attachment,
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate
            });
        }
        catch (KeyNotFoundException kEx)
        {
            return BadRequest(kEx.Message);
        }
        catch (TypeAccessException tEx)
        {
            return BadRequest(tEx.Message);
        }

        return Ok(res.Id);
    }

    [HttpGet]
    [Route("{id}/download", Name = nameof(DownloadPratica))]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadPratica([FromRoute] int id)
    {
        var res = new DownloadedPraticaDTO();
        try
        {
            res = await _praticaService.DownloadPratica(new DownloadPraticaDTO()
            {
                PraticaId = id
            });
        }
        catch (KeyNotFoundException kEx)
        {
            return BadRequest(kEx.Message);
        }

        return File(res.DownloadedFile, "application/pdf");
    }
}
