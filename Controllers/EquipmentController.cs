using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SabnauticaWiki.SabnauticaClass;

namespace SabnauticaWiki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly SabnauticaContext _context;

        public EquipmentController(SabnauticaContext context)
        {
            _context = context;
        }

        [HttpGet("GetEquipment", Name = "GetEquipment")]
        public ActionResult<Equipment> Get (int id)
        {
            var equipmentList = _context.Equipments.Find(id);

            if(equipmentList == null)
            {
                return NotFound();
            }

            return equipmentList;
        }

        [HttpPost("AddEquipment", Name = "AddEquipment")]

        public ActionResult<Equipment> Post (string category, string description, string name, string forGetting, string inventorySlot)
        {
            if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(forGetting) || string.IsNullOrWhiteSpace(inventorySlot)) 
            {
                return BadRequest("All fields must be filled");
            }

            var equipmentList = new Equipment
            {
                Category = category,
                Description = description,
                Name = name,
                ForGetting = forGetting,
                InventorySlot = inventorySlot
            };

            _context.Equipments.Add(equipmentList);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = equipmentList.Id }, equipmentList);
        }

        [HttpPut("UpdateEquipment", Name = "UpdateEquipment")]

        public ActionResult Put (int id, [FromBody] Equipment updateEquipment) 
        {
            if(id != updateEquipment.Id)
            {
                return BadRequest("Id not found");
            }

            var equipments = _context.Equipments.Find(id);

            if(equipments == null)
            {
                return NotFound();
            }

            equipments.Category = updateEquipment.Category;
            equipments.Description = updateEquipment.Description;
            equipments.Name = updateEquipment.Name;
            equipments.ForGetting = updateEquipment.ForGetting;
            equipments.InventorySlot = updateEquipment.InventorySlot;

            _context.Entry(equipments).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(equipments);
        }

        [HttpDelete("DeleteEquipment", Name = "DeleteEquipment")]

        public ActionResult Delete (int id)
        {
            var deleteEquipment = _context.Equipments.Find(id);

            if (deleteEquipment == null)
            {
                return NotFound();
            }

            _context.Equipments.Remove(deleteEquipment);
            _context.SaveChanges();

            return Ok();
        }
    }
}
