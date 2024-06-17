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
                new Gato { Id = 1, Nome = "Maximus Nonius Macrinus", Raca = "Laranja", Idade = 10 },
                new Gato { Id = 2, Nome = "Gato", Raca = "Sei lá kkj", Idade = 11 }
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
