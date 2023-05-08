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
        private readonly IVueltoService _vuelto;

        public OperacionController(IVueltoService vuelto)
        {
           _vuelto = vuelto;
        }

        // GET: api/Operacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacion>>> GetOperacion()
        {
            var result = _vuelto.GetOperaciones();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // GET: api/Operacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacion>> GetOperacion(int id)
        {
            var result = _vuelto.GetOperacionByID(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return result;
            }            
        }

        // POST: api/Operacion
        [HttpPost("{montoAPagar}/{montoPagado}")]
        public async Task<ActionResult<Operacion>> PostOperacion(decimal montoAPagar, decimal montoPagado)
        {
            string message = "";

            if (montoAPagar == 0 || montoPagado == 0)
            {
                message = "Ningún monto introducido debe ser cero.";
                return BadRequest(message);
            }
            if (montoAPagar > montoPagado)
            {
                message = "El monto abonado debe ser mayor al monto a abonar.";
                return BadRequest(message);
            }

            message = _vuelto.VueltoQuantity(montoAPagar, montoPagado);

            if (string.IsNullOrEmpty(message))
            {
                return BadRequest();
            }    
            else
            {
                return Ok(message);
            }
        }
    }
}
