using ContossoPizza.Models;
using ContossoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContossoPizza.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController() { }

    // Get all the pizzas in the system
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    // Get a specific pizza in the system
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        return (pizza == null) ? NotFound() : pizza;
    }

    // Create a new pizza onto the system
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    // Update a specific pizza
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
        {
            return BadRequest();
        }

        var existing = PizzaService.Get(id);
        if (existing is null)
        {
            return NotFound();
        }

        PizzaService.Update(pizza);

        return NoContent();
    }

    // Delete a pizza
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
        {
            return NotFound();
        }

        PizzaService.Delete(id);

        return NoContent();
    }
}