using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComandaController : ControllerBase
{
    private readonly ComandasDbContext _context;

    public ComandaController(ComandasDbContext context)
    {
        _context = context;
    }
    // GET: api/<ComandaController>
    [HttpGet]
    public IResult Get()
    {
        return Results.Ok(_context.Comandas.ToList());
    }

    // GET api/<ComandaController>/5
    [HttpGet("{id}")]
    public IResult Get(int id)
    {
        var comanda = _context.Comandas
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
            NomeCliente = comandaCreate.NomeCliente,
            NumeroMesa = comandaCreate.NumeroMesa,
        };
        var comandaItens = new List<ComandaItem>();

        foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
        {
            var comandaItem = new ComandaItem
            {
                CardapioItemId = cardapioItemId,
                Comanda = novaComanda
            };

            comandaItens.Add(comandaItem);

            var cardapioItem = _context.CardapioItems
                .FirstOrDefault(ci => ci.Id == cardapioItemId);

            if (cardapioItem!.PossuiPreparo)
            {
                var pedido = new PedidoCozinha
                {
                    Comanda = novaComanda
                };
                var pedidoItem = new PedidoCozinhaItem
                {
                    ComandaItem = comandaItem,
                    PedidoCozinha = pedido
                };
                _context.PedidoCozinhas.Add(pedido);
                _context.PedidoCozinhaItens.Add(pedidoItem);
            }
        }
        novaComanda.Itens = comandaItens;
        _context.Comandas.Add(novaComanda);
        _context.SaveChanges();
        return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);
    }

    // PUT api/<ComandaController>/5
    [HttpPut("{id}")]
    public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
    {   // pesquisa uma comanda na lista de comandas pelo id da comanda que veio no parametro da request
        var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
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
        _context.SaveChanges();
        // retorna 204 Sem conteudo
        return Results.NoContent();
    }

    // DELETE api/<ComandaController>/5
    [HttpDelete("{id}")]
    public IResult Delete(int id)
    {
        // pesquisa uma comanda na lista de comandas pelo id da comanda
        // que veio no parametro da request
        var comanda = _context.Comandas
                .FirstOrDefault(c => c.Id == id);
        // se não encontrou a comanda pesquisda
        if (comanda is null)
            // retorna um codigo 404 Não encontrado
            return Results.NotFound("Comanda nao encontrada");
        // remove a comanda da lista de comandas
        _context.Comandas.Remove(comanda);
        var removidoComSucesso = _context.SaveChanges();
        if (removidoComSucesso > 0)
            // retorna 204 Sem conteudo
            return Results.NoContent();
        return Results.StatusCode(500);
    }
}
