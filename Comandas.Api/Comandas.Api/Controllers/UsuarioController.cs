using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // lista usuarios
        static List<Usuario> usuarios = new List<Usuario>(){
            new Usuario
            {
                Id = 1,
                Nome = "Admin",
                Email = "admin@admin.com",
                Senha = "admin"
            },
            new Usuario
            {
                Id = 2,
                Nome = "Usuario",
                Email = "usuario@usuario.com",
                Senha = "usuario"
            }
        };

        // iresult  que retorna a lista de usuarios
        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(usuarios);
        }
        // iresult  que retorna um usuario pelo id
        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = usuarios.
                    FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound("Usuário não encontrado!");

            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if (usuarioCreate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter no mínimo 6 caracteres.");
            if (usuarioCreate.Nome.Length < 3)
                return Results.BadRequest("O nome deve ter no mínimo 3 caracteres.");
            if (usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"))
                return Results.BadRequest("O email deve ser válido.");
            var usuario = new Usuario
            {
                Id = usuarios.Count + 1,
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };

            // adiciona o usuario na lista
            usuarios.Add(usuario);

            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }

        // PUT api/<UsuarioController>/5
        /// <summary>
        /// ATUALIZA UM USUARIO
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="usuarioUpdate">Dados do usuario </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            // busca o usuario na lista pelo id
            var usuario = usuarios.
                    FirstOrDefault(u => u.Id == id);
            // se nao encontrar retorna notfound
            if (usuario is null)
                return Results.NotFound($"Usuário do id {id} nao encontrado.");
            // atualiza o usuario
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            //retorna no content
            return Results.NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound($"Usuário do id {id} nao encontrado.");

            usuarios.Remove(usuario);
            return Results.NoContent();
        }
    }
}
