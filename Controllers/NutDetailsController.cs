using Microsoft.AspNetCore.Mvc;

using core.Interfaces;
using core.Models;

namespace core.Controllers;

[ApiController]
[Route("[controller]")]
public class NutDetailsController : ControllerBase
{
    private readonly INutService nutService;

    public NutDetailsController(INutService nutService)
    {
        this.nutService = nutService;
    }

    [HttpGet]
    public ActionResult<List<NutDetails>> GetAll() =>
        nutService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<NutDetails> Get(int id)
    {
        var nut = nutService.Get(id);

        if (nut == null)
            return NotFound();

        return nut;
    }

    [HttpPost]
    public IActionResult Create(NutDetails nut)
    {
        nutService.Add(nut);
        return CreatedAtAction(nameof(Get), new { id = nut.Id }, nut);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, NutDetails nut)
    {
        if (id != nut.Id)
            return BadRequest();

        var existingNut = nutService.Get(id);
        if (existingNut is null)
            return NotFound();

        nutService.Update(id, nut);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var nut = nutService.Get(id);
        if (nut is null)
            return NotFound();

        nutService.Delete(id);

        return Content(nutService.Count.ToString());
    }
}
