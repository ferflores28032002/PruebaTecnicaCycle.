using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCycle.Application.services;
using PruebaTecnicaCycle.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnicaCycle.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _productServices;

        // Inyecta ProductServices a través del constructor
        public ProductController(ProductServices productServices)
        {
            _productServices = productServices ?? throw new ArgumentNullException(nameof(productServices));
        }

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            return Ok(_productServices.Listar());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get(Guid id)
        {
            var producto = _productServices.SeleccionarPorID(id);
            if (producto == null) return NotFound("Producto no encontrado.");
            return Ok(producto);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductDto productDto)
        {

          
            if (productDto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            try
            {
                await _productServices.Agregar(productDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Un error ha ocurrido: " + ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] ProductDto productDto)
        {
            try
            {
                await _productServices.Editar(id, productDto); // Asegúrate de que este método exista y sea asíncrono
                return Ok("Editado Correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto no encontrado.");
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productServices.Eliminar(id);
                return Ok("Eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto no encontrado.");
            }
        }
    }
}
