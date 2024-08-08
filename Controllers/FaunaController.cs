using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SabnauticaWiki.SabnauticaClass;

namespace SabnauticaWiki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaunaController : ControllerBase
    {
        private readonly SabnauticaContext _context;

        public FaunaController(SabnauticaContext context)
        {
            _context = context;
        }

        [HttpGet("GetFauna", Name = "GetFauna")]

        public ActionResult<Fauna> Get(int id) 
        {
            var faunaList = _context.Faunas.Find(id);

            if(faunaList == null) 
            {
                return NotFound();
            }
            return faunaList;
        }

        [HttpPost("AddFauna", Name = "AddFauna")]

        public ActionResult<Fauna> Post (string category, string name, string attitude, int health, string biome, string damage, string lenght, string weight)
        {
            if(string.IsNullOrEmpty(category) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(attitude) || string.IsNullOrWhiteSpace(biome)) 
            {
                return BadRequest("Category, name, attitude and biome must be filled");
            }

            var faunaList = new Fauna
            {
                Category = category,
                Name = name,
                Attitude = attitude,
                Health = health,
                Biome = biome,
                Damage = damage,
                Length = lenght,
                Weight = weight
            };

            _context.Faunas.Add(faunaList);
            _context.SaveChanges();

            return CreatedAtAction("Get", new {id = faunaList.Id}, faunaList);
        }

        [HttpPut("UpdateFauna", Name = "UpdateFauna")]

        public ActionResult Put (int id, [FromBody]Fauna updateFauna) 
        {
            if(id != updateFauna.Id)
            {
                return BadRequest("Id not found");
            }

            var faunas = _context.Faunas.Find(id);

            if(faunas == null)
            {
                return NotFound();
            }

            faunas.Category = updateFauna.Category;
            faunas.Name = updateFauna.Name;
            faunas.Attitude = updateFauna.Attitude;
            faunas.Health = updateFauna.Health;
            faunas.Damage = updateFauna.Damage;
            faunas.Biome = updateFauna.Biome;
            faunas.Length = updateFauna.Length;
            faunas.Weight = updateFauna.Weight;

            _context.Entry(faunas).State = EntityState.Modified;

            return Ok(faunas);
        }

        [HttpDelete("DeleteFauna", Name = "DeleteFauna")]

        public ActionResult Delete(int id )
        {
            var deleteFauna = _context.Faunas.Find(id );

            if(deleteFauna == null)
            {
                return NotFound();
            }

            _context.Faunas.Remove(deleteFauna);
            _context.SaveChanges();

            return Ok();
        }
    }
}
