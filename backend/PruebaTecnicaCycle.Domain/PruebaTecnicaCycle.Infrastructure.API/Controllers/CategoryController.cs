using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCycle.Application.services;
using PruebaTecnicaCycle.Infrastructure.Data.context;
using PruebaTecnicaCycle.Infrastructure.Data.repositories;
using PruebaTecnicaCycle.Application.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace PruebaTecnicaCycle.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly PruebaTecnicaCycleContext _dbContext;

        public CategoryController(PruebaTecnicaCycleContext dbContext)
        {
            _dbContext = dbContext;
        }

        private CategoryServices CrearServicio()
        {
            CategoryRepository repo = new CategoryRepository(_dbContext);
            return new CategoryServices(repo);
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<List<CategoryDto>> Get()
        {
            var servicio = CrearServicio();
            return Ok(servicio.Listar());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get(Guid id)
        {
            var servicio = CrearServicio();
            var categoria = servicio.SeleccionarPorID(id);
            if (categoria == null) return NotFound("Categoría no encontrada.");
            return Ok(categoria);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult Post([FromBody] CategoryDto categoryDto)
        {
            var servicio = CrearServicio();
            servicio.Agregar(categoryDto);
            return Ok();
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] CategoryDto categoryDto)
        {
            var servicio = CrearServicio();
            try
            {
                servicio.Editar(id, categoryDto);
                return Ok("Editado Correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoría no encontrada.");
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var servicio = CrearServicio();
            try
            {
                servicio.Eliminar(id);
                return Ok("Eliminado correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoría no encontrada.");
            }
        }
    }
}
