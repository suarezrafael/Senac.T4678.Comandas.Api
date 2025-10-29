using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace Comandas.Api.Controllers
{
    // CRIA A ROTA DO CONTROLADORc
    [Route("api/[controller]")]
    [ApiController] // DEFINE QUE ESSA CLASSE É UM CONTROLADOR DE API
    public class CardapioItemController : ControllerBase // HERDA DE ControllerBase para PODER RESPONDER A REQUISICOES HTTP
    {
        private readonly ComandasDbContext _context;
        public CardapioItemController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/cardapioitens
        [HttpGet]
        public IResult Get()
        {
            var cardapios = _context.CardapioItems.ToList();
            return Results.Ok(cardapios);
        }

        // GET api/<CardapioItemController>/1
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // BUSCAR NA LISTA de cardapios de acordo com o Id do parametro
            // joga o valor para a variavel o primeiro elemento de acordo com o id
            var cardapio = _context.CardapioItems.FirstOrDefault(c => c.Id == id);
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
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
            // adiciona o cardapio na lista
            _context.CardapioItems.Add(cardapioItem);
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
            var cardapioItem = _context.CardapioItems.
                    FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            cardapioItem.Titulo = cardapio.Titulo;
            cardapioItem.Descricao = cardapio.Descricao;
            cardapioItem.Preco = cardapio.Preco;
            cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            _context.SaveChanges();
            return Results.NoContent();
        }

        // DELETE http:5100/api/cardapioitem/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // buscar o cardapio na lista pelo id
            var cardapioItem = _context.CardapioItems
                .FirstOrDefault(c => c.Id == id);
            // se estiver nulo, retorna 404
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            // remove o objeto cardapio da lista
            _context.CardapioItems.Remove(cardapioItem);
            _context.SaveChanges();
            return Results.NoContent();


        }
    }
}
