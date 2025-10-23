using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        static List<Comanda> comandas = new List<Comanda>()
        {
            new Comanda
            {
                Id = 1,
                NomeCliente = "Rafael Suarez",
                NumeroMesa = 1,
                Itens = new List<ComandaItem>
                {
                    new ComandaItem
                    {
                        Id = 1,
                        CardapioItemId = 1,
                        ComandaId = 1,
                    }
                }
            },
            new Comanda
            {
                Id = 2,
                NomeCliente = "Maria Silva",
                NumeroMesa = 2,
                Itens = new List<ComandaItem>
                {
                    new ComandaItem
                    {
                        Id = 2,
                        CardapioItemId = 2,
                        ComandaId = 2,
                    }
                }
            }
        };
        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = comandas
                    .FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda nao encontrada");
            return Results.Ok(comanda);
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            if (comandaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (comandaCreate.CardapioItemIds.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos um item do cardápio.");
            var novaComanda = new Comanda
            {
                Id = comandas.Count + 1,
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
            };
            // cria uma variavel do tipo lista de itens
            var comandaItens = new List<ComandaItem>();
            // percorre os ids dos itens do cardapio
            foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
            {
                // cria um novo item de comanda
                var comandaItem = new ComandaItem
                {
                    Id = comandaItens.Count + 1,
                    CardapioItemId = cardapioItemId,
                    ComandaId = novaComanda.Id,
                };
                // adiciona o item na lista de itens
                comandaItens.Add(comandaItem);
            }
            // atribui os itens a nota comanda
            novaComanda.Itens = comandaItens;
            // adiciona a nova comanda na lista de comandas
            comandas.Add(novaComanda);
            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {   // pesquisa uma comanda na lista de comandas pelo id da comanda que veio no parametro da request
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null) // se não encontrou a comanda pesquisda
                // retorna um codigo 404 Não encontrado 
                return Results.NotFound("Comanda nao encontrada");
            // validar o nome do cliente
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            // validar o numero da mesa
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            // Atualiza as informações da comanda
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;

            // retorna 204 Sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // pesquisa uma comanda na lista de comandas pelo id da comanda
            // que veio no parametro da request
            var comanda = comandas
                    .FirstOrDefault(c => c.Id == id);
            // se não encontrou a comanda pesquisda
            if (comanda is null)
                // retorna um codigo 404 Não encontrado
                return Results.NotFound("Comanda nao encontrada");
            // remove a comanda da lista de comandas
            var removidoComSucesso = comandas.Remove(comanda);

            if (removidoComSucesso)
                // retorna 204 Sem conteudo
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
