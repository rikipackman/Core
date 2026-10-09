using Microsoft.AspNetCore.Mvc;
using core.Models;

namespace core.Controllers;

[ApiController]
[Route("[controller]")]
public class NutDetailsController : ControllerBase
{
    private static readonly string[] NutName = new[]
    {
        "cashew", "Black sunflower seeds", "Pistachio", "peanuts", "Almonds", "walnut", "Watermelon seeds"
    };
    private static List<NutDetails>? arr;
    public NutDetailsController()
    {
        arr = Enumerable.Range(1, 5).Select(index => new NutDetails
        {
            Id = index,
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index+(index*4))),
            Weight = Random.Shared.Next(100,1000),
            Name = NutName[Random.Shared.Next(NutName.Length)]
        }).ToList();
    }




    [HttpGet]
    public IEnumerable<NutDetails?> Get()
    {
        return arr;
    }



   [HttpGet("{id}")]
    public ActionResult<NutDetails> Get(int id)
    {
        //TODO basic checks
        var res = arr?.FirstOrDefault(N => N.Id == id); 

        if (res == null)
            return NotFound();
        
        return res;
    }
    }
