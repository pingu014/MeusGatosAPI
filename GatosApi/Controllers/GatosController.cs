using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GatosApi.Models;
using GatosApi.Services;

namespace GatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatosController : ControllerBase
    {
        private readonly GatoService _service;

        public GatosController()
        {
            _service = new GatoService();
        }

        // Método GET para obter todos os gatos
        [HttpGet]
        public ActionResult<IEnumerable<Gato>> Get()
        {
            return Ok(_service.GetAll());
        }

        // Método GET para obter um gato específico pelo ID
        [HttpGet("{id}")]
        public ActionResult<Gato> Get(int id)
        {
            var gato = _service.GetById(id);
            if (gato == null)
            {
                return NotFound();
            }
            return Ok(gato);
        }

        // Método POST para adicionar um novo gato
        [HttpPost]
        public ActionResult<Gato> Post([FromBody] Gato novoGato)
        {
            var gato = _service.Add(novoGato);
            return CreatedAtAction(nameof(Get), new { id = gato.Id }, gato);
        }

        // Método PUT para atualizar um gato existente
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Gato gatoAtualizado)
        {
            var gato = _service.GetById(id);
            if (gato == null)
            {
                return NotFound();
            }

            _service.Update(id, gatoAtualizado);
            return NoContent();
        }

        // Método DELETE para excluir um gato
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gato = _service.GetById(id);
            if (gato == null)
            {
                return NotFound();
            }

            _service.Delete(id);
            return NoContent();
        }
    }
}
