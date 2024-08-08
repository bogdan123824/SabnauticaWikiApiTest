using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SabnauticaWiki.SabnauticaClass;
using System.Xml;

namespace SabnauticaWiki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FloraController : ControllerBase
    {
        private readonly SabnauticaContext _context;

        public FloraController(SabnauticaContext context)
        {
            _context = context;
        }

        [HttpGet("GetFlora", Name = "GetFlora")]
        public ActionResult<Flora> Get(int id)
        {
            var floraList = _context.Floras.Find(id);

            if (floraList == null)
            {
                return NotFound();
            }
            return floraList;
        }

        [HttpPost("AddFlora", Name = "AddFlora")]

        public ActionResult<Flora> Post (string category, string name, string biome, int inventorySlot, string seed)
        {
            if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(biome) || inventorySlot <= 0 || string.IsNullOrWhiteSpace(seed))
            {
                return BadRequest("All fields must be filled");
            }

            var floraList = new Flora
            {
                Category = category,
                Name = name,
                Biome = biome,
                InventorySlot = inventorySlot,
                Seed = seed,
            };

            _context.Floras.Add(floraList);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = floraList }, floraList);
        }

        [HttpPut("UpdateFlora", Name = "UpdateFlora")]

        public ActionResult<Flora> Put(int id, [FromBody] Flora updateFlora)
        {
            if (id != updateFlora.Id)
            {
                return BadRequest("Id not found");
            }

            var floras = _context.Floras.Find(id);

            if(floras == null)
            {
                return NotFound();
            }

            floras.Category = updateFlora.Category;
            floras.Name = updateFlora.Name;
            floras.Biome = updateFlora.Biome;
            floras.InventorySlot = updateFlora.InventorySlot;
            floras.Seed = updateFlora.Seed;

            _context.Entry(floras).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(floras);
        }

        [HttpDelete("DeleteFlora", Name = "DeleteFlora")]

        public ActionResult Delete(int id)
        {
            var deleteFlora = _context.Floras.Find(id);

            if (deleteFlora == null)
            {
                return NotFound();
            }

            _context.Floras.Remove(deleteFlora);
            _context.SaveChanges();

            return Ok();
        }
    }
}
