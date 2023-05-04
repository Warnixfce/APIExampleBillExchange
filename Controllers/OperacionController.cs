using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBillExchange.Context;
using APIBillExchange.Models;
using APIBillExchange.Services;
using APIBillExchange.Interfaces;

namespace APIBillExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly MoneyExchangeContext _context;
        private readonly IVueltoService _vuelto;

        public OperacionController(MoneyExchangeContext context, IVueltoService vuelto)
        {
            _context = context;
            _vuelto = vuelto;
        }

        // GET: api/Operacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacion>>> GetOperacion()
        {
          if (_context.Operacion == null)
          {
              return NotFound();
          }
            return await _context.Operacion.ToListAsync();
        }

        // GET: api/Operacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacion>> GetOperacion(int id)
        {
          if (_context.Operacion == null)
          {
              return NotFound();
          }
            var operacion = await _context.Operacion.FindAsync(id);

            if (operacion == null)
            {
                return NotFound();
            }

            return operacion;
        }

        // PUT: api/Operacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacion(int id, Operacion operacion)
        {
            if (id != operacion.IdOperacion)
            {
                return BadRequest();
            }

            _context.Entry(operacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperacionExists(id))
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

        // POST: api/Operacion
        [HttpPost]
        public async Task<ActionResult<Operacion>> PostOperacion(Operacion operacion)
        {
          if (_context.Operacion == null)
          {
              return Problem("Entity set 'MoneyExchangeContext.Operacion'  is null.");
          }
            _context.Operacion.Add(operacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperacion", new { id = operacion.IdOperacion }, operacion);
        }

        // DELETE: api/Operacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperacion(int id)
        {
            if (_context.Operacion == null)
            {
                return NotFound();
            }
            var operacion = await _context.Operacion.FindAsync(id);
            if (operacion == null)
            {
                return NotFound();
            }

            _context.Operacion.Remove(operacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperacionExists(int id)
        {
            return (_context.Operacion?.Any(e => e.IdOperacion == id)).GetValueOrDefault();
        }
    }
}
