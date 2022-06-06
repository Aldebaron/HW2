using HW2.Models;
using HW2.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW2.Controllers;

[ApiController]
[Route("[controller]")]
public class RectangleController : ControllerBase
{
    
    public RectangleController()
    { 
    }

    [HttpGet]
    public ActionResult<List<Rectangle>> GetAll() =>
    RectangleService.GetAll();

    [HttpGet("Area {id}")]
    public ActionResult<string> Area(int id) {
        var r = RectangleService.Get(id);

        if (r == null)
            return NotFound();

        decimal a = r.Width * r.Height;

        

            return a.ToString();
        }

    [HttpGet("Perimeter {id}")]
    public ActionResult<string> Perimeter(int id)
    {
        var r = RectangleService.Get(id);

        if (r == null)
            return NotFound();

        decimal a = (r.Width*2)+(2 * r.Height);



        return a.ToString();
    }

    [HttpGet("1Contains2 {id1} {id2}")]
    public ActionResult<string> Contains(int id1, int id2)
    {
        var r1 = RectangleService.Get(id1);
        var r2 = RectangleService.Get(id2);

        if (r1 == null || r2 == null)
            return NotFound();

        if (r1.Y - ((1 / 2) * r1.Height) <= r2.Y - ((1 / 2) * r2.Height) && r1.Y + ((1 / 2) * r1.Height) >= r2.Y + (1 / 2) * r2.Height && r1.X - (1 / 2) * r1.Width <= r2.X - (1 / 2) * r2.Width && r1.X + (1 / 2) * r1.Width >= r2.X + (1 / 2) * r2.Width)
            return "True";


        else return "False";


        
    }


    [HttpGet("{id}")]
    public ActionResult<Rectangle> Get(int id)
    {
        var r = RectangleService.Get(id);

        if (r == null)
            return NotFound();

        return r;
    }

    [HttpPost]
    public IActionResult Create(Rectangle rectangle)
    {
        RectangleService.Add(rectangle);
        return CreatedAtAction(nameof(Create), new { id = rectangle.Id }, rectangle);
    }

    [HttpGet("1Intersects2 {id1} {id2}")]
    public ActionResult<string> Intersects(int id1, int id2)
    {
        

        var r1 = RectangleService.Get(id1);
        var r2 = RectangleService.Get(id2);

        if (r1 == null || r2 == null)
            return NotFound();

        var sl = r1.X - 0.5 * r1.Width;
        var sr = r1.X + 0.5 * r1.Width;
        var st = r1.Y + 0.5 * r1.Height;
        var sb = r1.Y - 0.5 * r1.Height;
        var ol = r2.X - 0.5 * r2.Width;
        var ori = r2.X + 0.5 * r2.Width;
        var ot = r2.Y + 0.5 * r2.Height;
        var ob = r2.Y - 0.5 * r2.Height;
        if ((ol <= sl && sl <= ori) && ((sb <= ob && ob <= st) || (sb <= ot && ot <= st))) {
        return "True"; }
    else if ((ol <= sr && sr <= ori) && ((sb <= ob && ob <= st) || (sb <= ot && ot <= st)))
      return "True";
    else if ((ob <= st && st <= ot) && ((sl <= ol && ol <= sr) || (sl <= ori && ori <= sr)))
      return "True";
    else if ((ob <= sb && sb <= ot) && ((sl <= ol && ol <= sr) || (sl <= ori && ori <= sr)))
      return "True";
    else return "False";


    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, Rectangle rectangle)
    {
        if (id != rectangle.Id)
            return BadRequest();

        var existingRectangle = RectangleService.Get(id);
        if (existingRectangle is null)
            return NotFound();

        RectangleService.Update(rectangle);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var r = RectangleService.Get(id);

        if (r is null)
            return NotFound();

        RectangleService.Delete(id);

        return NoContent();
    }
}
