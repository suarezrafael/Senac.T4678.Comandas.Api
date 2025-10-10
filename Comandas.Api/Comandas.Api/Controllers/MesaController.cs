﻿using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        List<Mesa> mesas = new List<Mesa>()
        {
            new Mesa
            {
                Id = 1,
                NumeroMesa = 1,
                SituacaoMesa = (int)SituacaoMesa.Livre
            },
            new Mesa
            {
                Id = 2,
                NumeroMesa = 2,
                SituacaoMesa = (int)SituacaoMesa.Ocupada

            }
        };

        // GET: api/<MesaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            {
                return Results.NotFound("Mesa não encontrada!");
            }
            return Results.Ok(mesa);
        }

        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody] MesaCreateRequest mesaCreate)
        {
            // valida se o numero da mesa é maior que zero
            if (mesaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            // cria uma nova mesa
            var novaMesa = new Mesa
            {
                Id = mesas.Count + 1,
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = (int)SituacaoMesa.Livre
            };
            // adiciona a nova mesa na lista
            mesas.Add(novaMesa);
            // retorna a nova mesa criada e o codigo 201 CREATED
            return Results.Created($"/api/mesa/{novaMesa.Id}", novaMesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
            if (mesaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (mesaUpdate.SituacaoMesa < 0 || mesaUpdate.SituacaoMesa > 2)
                return Results.BadRequest("A situação da mesa deve ser 0 (Livre), " +
                    "1 (Ocupada) ou 2 (Reservada).");

            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada!");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
