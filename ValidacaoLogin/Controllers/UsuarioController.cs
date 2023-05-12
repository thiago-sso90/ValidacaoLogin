using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValidacaoLogin.Entidades;
using ValidacaoLogin.Persistencia;

namespace ValidacaoLogin.Controllers
{
    [Route("api/Login_Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDbContext _context;
        public UsuarioController(UsuarioDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Mostra todos os Usuarios
        /// </summary>
        /// <returns>Todos Ususarios</returns>
        /// <response code = "200"></response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
            
        {
            var devEvents = _context.usuarios.ToList();

            return Ok(devEvents);
        }
        /// <summary>
        /// Mostra apenas um usuario
        /// </summary>
        /// <param name="email">Identificador do Usuario</param>
        /// <returns>Ddados do Usuario</returns>
        /// <response code = "200">Sucesso</response>
        /// <response code = "404">Usuario não Encontrado</response>
        [HttpPost("email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromBody]string email) 
        {
             var User = _context.usuarios.SingleOrDefault(u => u.Email == email);
            if(User == null) 
            {
                return NotFound();
            }
            return Ok(User);
        }
        /// <summary>
        /// Cadastrar Um Ususario
        /// </summary>
        /// <remarks>
        /// {"id": 0,"email": "string","senha": "string"}
        /// </remarks>
        /// <param name="usuario">Dados Do Usuario</param>
        /// <returns>Usuario recem-criado</returns>
        /// <response code = "201">Usuario criado com Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Usuario usuario)
        {
            _context.usuarios.Add(usuario);
            _context.SaveChangesAsync();
            return Created($"/get-usuarios-by-id?id={usuario.Id}", usuario);
        }
        /// <summary>
        /// Atualiza um Usuario
        /// </summary>
        /// <remarks>
        /// {"id": 0,"email": "string","senha": "string"}
        /// </remarks>
        /// <param name="usuario">Dados do Usuario</param>
        /// <returns>Nada</returns>
        /// <response code = "204">Sucesso</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(Usuario usuario) 
        {
            var user = _context.usuarios.FirstOrDefault();
            if(user == null)
            {
                return NotFound();
            }

            _context.usuarios.Update(usuario);
            _context.SaveChanges();
            return NoContent();
        }
        /// <summary>
        /// Deletar um Ususario
        /// </summary>
        /// <param name="id">Identificador de um Usuario</param>
        /// <returns>Nada</returns>
        /// <response code = "204">Deletado com Sucesso</response>
        /// <response code = "404">Usuario Não Encontardo</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) 
        {
            var usuarioDelete = _context.usuarios.SingleOrDefault(u => u.Id == id);
            if(usuarioDelete == null)
            {
                return NotFound();
            }

            _context.usuarios.Remove(usuarioDelete);
           _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
