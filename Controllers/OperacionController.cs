﻿using System;
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
        //private readonly MoneyExchangeContext _context;
        private readonly IVueltoService _vuelto;

        public OperacionController(/*MoneyExchangeContext context, */IVueltoService vuelto)
        {
            //_context = context;
            _vuelto = vuelto;
        }

        // GET: api/Operacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacion>>> GetOperacion()
        {
            if (_vuelto.GetOperaciones() == null)
            {
                return NotFound();
            }

            return _vuelto.GetOperaciones();
            //return await _context.Operacion.ToListAsync();
        }

        // GET: api/Operacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacion>> GetOperacion(int id)
        {
            if (_vuelto.GetOperacionPorID(id) == null)
            {
                return NotFound();
            }
            else
            {
                return _vuelto.GetOperacionPorID(id);
            }
            //if (_context.Operacion == null)
            //{
            //    return NotFound();
            //}
            //var operacion = await _context.Operacion.FindAsync(id);

            //if (operacion == null)
            //{
            //    return NotFound();
            //}

            

            //return operacion;
        }

        // POST: api/Operacion
        [HttpPost("{montoAPagar}/{montoPagado}")]
        public async Task<ActionResult<Operacion>> PostOperacion(/*Operacion operacion, */decimal montoAPagar, decimal montoPagado)
        {
            //if (_context.Operacion == null)
            //{
            //    return Problem("Entity set 'MoneyExchangeContext.Operacion'  is null.");
            //}

            string mensaje = _vuelto.CantidadVuelto(montoAPagar, montoPagado/*, operacion*/);

            if (string.IsNullOrEmpty(mensaje))
            {
                return BadRequest();
            }
            else
            {
                return Ok(mensaje);
            }


            //return CreatedAtAction("GetOperacion", mensaje, new { id = operacion.IdOperacion }, operacion);
        }

        //// PUT: api/Operacion/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOperacion(int id, Operacion operacion)
        //{
        //    if (id != operacion.IdOperacion)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(operacion).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OperacionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}



        //// DELETE: api/Operacion/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOperacion(int id)
        //{
        //    if (_context.Operacion == null)
        //    {
        //        return NotFound();
        //    }
        //    var operacion = await _context.Operacion.FindAsync(id);
        //    if (operacion == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Operacion.Remove(operacion);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool OperacionExists(int id)
        //{
        //    return (_context.Operacion?.Any(e => e.IdOperacion == id)).GetValueOrDefault();
        //}
    }
}
