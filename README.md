### MeusGatosAPI :smile_cat: @alexmota-dev
Downloads [.NET SDK](https://dotnet.microsoft.com/download) & [Visual Studio](https://visualstudio.microsoft.com/)


> Criar a Classe Gato > Criar um arquivo Gato.cs dentro da pasta "Models":
```csharp
namespace GatosApi.Models
{
    public class Gato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public int Idade { get; set; }
    }
}
```
> Criar a Classe GatoService > Criar um arquivo GatoService.cs dentro da pasta "Services":
```csharp
using System.Collections.Generic;
using System.Linq;
using GatosApi.Models;

namespace GatosApi.Services
{
    public class GatoService
    {
        private readonly List<Gato> _gatos;

        public GatoService()
        {
            _gatos = new List<Gato>
            {
                new Gato { Id = 1, Nome = "Mimi", Raca = "Siamês", Idade = 2 },
                new Gato { Id = 2, Nome = "Felix", Raca = "Persa", Idade = 4 }
            };
        }

        public IEnumerable<Gato> GetAll() => _gatos;

        public Gato GetById(int id) => _gatos.FirstOrDefault(g => g.Id == id);

        public Gato Add(Gato novoGato)
        {
            novoGato.Id = _gatos.Max(g => g.Id) + 1;
            _gatos.Add(novoGato);
            return novoGato;
        }

        public void Update(int id, Gato gatoAtualizado)
        {
            var gato = _gatos.FirstOrDefault(g => g.Id == id);
            if (gato != null)
            {
                gato.Nome = gatoAtualizado.Nome;
                gato.Raca = gatoAtualizado.Raca;
                gato.Idade = gatoAtualizado.Idade;
            }
        }

        public void Delete(int id)
        {
            var gato = _gatos.FirstOrDefault(g => g.Id == id);
            if (gato != null)
            {
                _gatos.Remove(gato);
            }
        }
    }
}
```
> Criar a Classe GatosController > Criar um arquivo GatosController.cs dentro da pasta "Controllers":
```csharp
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
```
> Configurar tudo no Program.cs:
```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GatosApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<GatoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

> Só testar a API
```bash
C:\Users\pingo014> cd GatosApi
C:\Users\pingo014\GatosApi> dotnet run
```
> [!NOTE]
> Pode ser que dê algum conflito(foi codado com o pc cheio de problemas).
