using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SabnauticaWiki.SabnauticaClass;

namespace SabnauticaWiki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BiomesController : ControllerBase
    {
        private readonly SabnauticaContext _context;

        public BiomesController(SabnauticaContext context)
        {
            _context = context;
        }

        [HttpGet("GetBiomes" , Name = "GetBiomes")]

        public ActionResult<Biome> Get (int id)
        {
            var allBiomes = _context.Biomes.Find(id);

            if(allBiomes == null)
            {
                return NotFound();
            }
            return allBiomes;
        }

        [HttpPost("AddBiome", Name = "AddBiome")]

        public ActionResult<Biome> Post(string category, string name, string habitat, string usefulThings)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(habitat) || string.IsNullOrWhiteSpace(usefulThings))
            {
                return BadRequest("All fields must be filled");
            }

            var biomeList = new Biome
            {
                Category = category,
                Name = name,
                Habitat = habitat,
                UsefulThings = usefulThings
            };

            _context.Biomes.Add(biomeList);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = biomeList.Id }, biomeList);
        }

        [HttpPut("UpdateBiome", Name = "UpdateBiome")]

        public ActionResult Put (int id, [FromBody] Biome updateBiome)
        {
            if (id != updateBiome.Id)
            {
                return BadRequest("Id not found");
            }

            var biomes = _context.Biomes.Find(id);

            if(biomes == null)
            {
                return NotFound();
            }

            biomes.Category = updateBiome.Category;
            biomes.Name = updateBiome.Name;
            biomes.Habitat = updateBiome.Habitat;
            biomes.UsefulThings = updateBiome.UsefulThings;

            _context.Entry(biomes).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(biomes);
        }

        [HttpDelete("DeleteBiome", Name = "DeleteBiome")]

        public ActionResult Delete(int id)
        {
            var deleteBiome = _context.Biomes.Find(id);

            if(deleteBiome == null)
            {
                return NotFound();
            }

            _context.Biomes.Remove(deleteBiome);
            _context.SaveChanges();

            return Ok();
        }


    }
}
