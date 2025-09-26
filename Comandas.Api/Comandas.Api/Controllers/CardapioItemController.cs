using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace Comandas.Api.Controllers
{
    // CRIA A ROTA DO CONTROLADOR
    [Route("api/[controller]")]
    [ApiController] // DEFINE QUE ESSA CLASSE É UM CONTROLADOR DE API
    public class CardapioItemController : ControllerBase // HERDA DE ControllerBase para PODER RESPONDER A REQUISICOES HTTP
    {
        // METODO GET que retorna a lista de cardapio
        // GET: api/<CardapioItemController>
        [HttpGet] // ANOTACAO QUE INDICA SE O METODO RESPONDE A REQUISICOES GET
        public IEnumerable<CardapioItem> Get()
        {
            // CRIA UMA LISTA ESTATICA DE CARDAPIO e TRANSFORMA EM JSON
            return new CardapioItem[]
            {
               new CardapioItem // CRIA O PRIMEIRO ELEMENTO DA LISTA DE CARDAPIO
               {
                    Id = 1,
                    Titulo = "Coxinha",
                    Descricao = "Deliciosa coxinha de frango com catupiry",
                    Preco = 5.50m,
                    PossuiPreparo = true
               },
               new CardapioItem // CRIA O SEGUNDO ELEMENTO DA LISTA DE CARDAPIO
               {
                    Id = 2,
                    Titulo = "X-Salada",
                    Descricao = "Bife, Alface, cebola, tomate, maionese, mostarda, tempero verde",
                    Preco = 25.50m,
                    PossuiPreparo = true
               },

            };
        }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
