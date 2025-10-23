using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace Comandas.Api.Controllers
{
    // CRIA A ROTA DO CONTROLADOR
    [Route("api/[controller]")]
    [ApiController] // DEFINE QUE ESSA CLASSE É UM CONTROLADOR DE API
    public class CardapioItemController : ControllerBase // HERDA DE ControllerBase para PODER RESPONDER A REQUISICOES HTTP
    {
        static List<CardapioItem> cardapios = new List<CardapioItem>(){
            new CardapioItem
            {
                Id = 1,
                Descricao = "Coxinha de frango com catupiry",
                Preco = 5.50m,
                PossuiPreparo = true
            },
            new CardapioItem
            {
                Id = 2,
                Descricao = "X-Salada",
                Preco = 25.50m,
                PossuiPreparo = true
            }
        };
        // METODO GET que retorna a lista de cardapio
        // GET: api/<CardapioItemController>
        [HttpGet] // ANOTACAO QUE INDICA SE O METODO RESPONDE A REQUISICOES GET
        public IResult Get()
        {
            // CRIA UMA LISTA ESTATICA DE CARDAPIO e TRANSFORMA EM JSON
            return Results.Ok(cardapios);
        }

        // GET api/<CardapioItemController>/1
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // BUSCAR NA LISTA de cardapios de acordo com o Id do parametro
            // joga o valor para a variavel o primeiro elemento de acordo com o id
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound("Cardápio não encontrado!");
            }
            // retorna o valor para o endpoint da api
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if (cardapio.Titulo.Length < 3)
                return Results.BadRequest("O título do item do cardápio deve ter no mínimo 3 caracteres.");
            if (cardapio.Descricao.Length < 3)
                return Results.BadRequest("A descrição do item do cardápio deve ter no mínimo 3 caracteres.");
            if (cardapio.Preco <= 0)
                return Results.BadRequest("O preço do item do cardápio deve ser maior que zero.");
            var cardapioItem = new CardapioItem
            {
                Id = cardapios.Count + 1,
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
            // adiciona o cardapio na lista
            cardapios.Add(cardapioItem);
            return Results.Created($"/api/cardapioitem/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        /// <summary>
        /// Atualiza um cardapio item.
        /// </summary>
        /// <remarks>The specified ID must correspond to an existing menu item. If the ID does not exist,
        /// the update operation will fail. Ensure that the update request contains valid data for the menu item
        /// fields.</remarks>
        /// <param name="id">The unique identifier of the menu item to update.</param>
        /// <param name="cardapio">The update request containing the new values for the menu item. This parameter must not be null.</param>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
            var cardapioItem = cardapios.
                    FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            cardapioItem.Titulo = cardapio.Titulo;
            cardapioItem.Descricao = cardapio.Descricao;
            cardapioItem.Preco = cardapio.Preco;
            cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            return Results.NoContent();
        }

        // DELETE http:5100/api/cardapioitem/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // buscar o cardapio na lista pelo id
            var cardapioItem = cardapios
                .FirstOrDefault(c => c.Id == id);
            // se estiver nulo, retorna 404
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            // remove o objeto cardapio da lista
            var removidoComSucesso = cardapios.Remove(cardapioItem);
            // retorna 204 sem conteudo
            if (removidoComSucesso)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
