using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SabnauticaWiki.SabnauticaClass;

namespace SabnauticaWiki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly SabnauticaContext _context;

        public VehicleController(SabnauticaContext context)
        {
            _context = context;
        }

        [HttpGet("GetVehicle", Name = "GetVehicle")]

        public ActionResult<Vehicle> Get(int id)
        {
            var vehicleList = _context.Vehicles.Find(id);
            
            if(vehicleList == null)
            {
                return NotFound();
            }
            return vehicleList;
        }

        [HttpPost("AddVehicle", Name = "AddVehicle")]

        public ActionResult<Vehicle> Post(string category, string description, string name, string imerssionDepth, int health, string fragments, int damage, string locationFragments, string storage)
        {
            if(string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(imerssionDepth) || health <= 0 || string.IsNullOrWhiteSpace(fragments) || damage <= -1 || string.IsNullOrWhiteSpace(locationFragments) || string.IsNullOrWhiteSpace(storage)) 
            {
                return BadRequest("All fields must be filled");
            }

            var vehicleList = new Vehicle
            {
                Category = category,
                Description = description,
                Name = name,
                ImerssionDepth = imerssionDepth,
                Health = health,
                Fragments = fragments,
                Damage = damage,
                LocationFragments = locationFragments,
                Storage = storage
            };

            _context.Vehicles.Add(vehicleList);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = vehicleList.Id }, vehicleList);
        }

        [HttpPut("UpdateVehicle", Name = "UpdateVehicle")]

        public ActionResult Put(int id, [FromBody] Vehicle updateVehicle) 
        {
            if(id != updateVehicle.Id)
            {
                return BadRequest("Id not found");
            }

            var vehicles = _context.Vehicles.Find(id);

            if (vehicles == null)
            {
                return NotFound();
            }

            vehicles.Category = updateVehicle.Category;
            vehicles.Description = updateVehicle.Description;
            vehicles.Name = updateVehicle.Name;
            vehicles.ImerssionDepth = updateVehicle.ImerssionDepth;
            vehicles.Health = updateVehicle.Health;
            vehicles.Fragments = updateVehicle.Fragments;
            vehicles.Damage = updateVehicle.Damage;
            vehicles.LocationFragments = updateVehicle.LocationFragments;
            vehicles.Storage = updateVehicle.Storage;

            _context.Entry(vehicles).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(vehicles);
        }

        [HttpDelete("DeleteVehicle", Name = "DeleteVehicle")]

        public ActionResult Delete(int id)
        {
            var deleteVehicle = _context.Vehicles.Find(id);

            if(deleteVehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(deleteVehicle);
            _context.SaveChanges();

            return Ok();
        }
    }
}
