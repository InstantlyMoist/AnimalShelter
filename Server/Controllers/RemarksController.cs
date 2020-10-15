using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemarksController : ControllerBase
    {
        private readonly AnimalShelterContext _context;

        public RemarksController(AnimalShelterContext context)
        {
            _context = context;
        }

        // GET: api/Remarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remark>>> GetRemark()
        {
            return await _context.Remark.ToListAsync();
        }

        // GET: api/Remarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remark>> GetRemark(int id)
        {
            var remark = await _context.Remark.FindAsync(id);

            if (remark == null)
            {
                return NotFound();
            }

            return remark;
        }

        // PUT: api/Remarks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemark(int id, Remark remark)
        {
            if (id != remark.Id)
            {
                return BadRequest();
            }

            _context.Entry(remark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemarkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Remarks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Remark>> PostRemark(Remark remark)
        {
            _context.Remark.Add(remark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRemark", new { id = remark.Id }, remark);
        }

        // DELETE: api/Remarks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Remark>> DeleteRemark(int id)
        {
            var remark = await _context.Remark.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }

            _context.Remark.Remove(remark);
            await _context.SaveChangesAsync();

            return remark;
        }

        private bool RemarkExists(int id)
        {
            return _context.Remark.Any(e => e.Id == id);
        }
    }
}
