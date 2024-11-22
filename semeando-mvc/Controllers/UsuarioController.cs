using Microsoft.AspNetCore.Mvc;
using semeando_mvc.Application.Dtos;
using semeando_mvc.Application.Services;

namespace semeando_mvc.Controllers;

/// <summary>
/// Controller responsável pelas operações do usuário, como login, registro e gerenciamento de perfil.
/// </summary>
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;

    /// <summary>
    /// Construtor da controller de usuário.
    /// </summary>
    /// <param name="usuarioService">Serviço de usuário para manipulação de dados.</param>
    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Testa a funcionalidade de sessão.
    /// </summary>
    /// <returns>Valor armazenado na sessão.</returns>
    public IActionResult TestSession()
    {
        HttpContext.Session.SetString("TestKey", "TestValue");
        var value = HttpContext.Session.GetString("TestKey");
        return Content($"Session value: {value}");
    }

    /// <summary>
    /// Exibe a tela de login.
    /// </summary>
    /// <returns>Retorna a view de login.</returns>
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Processa o login do usuário.
    /// </summary>
    /// <param name="email">Email do usuário.</param>
    /// <returns>Redireciona para o perfil do usuário ou retorna a view de login com mensagem de erro.</returns>
    [HttpPost]
    public async Task<IActionResult> Login(string email)
    {
        var usuarios = await _usuarioService.GetAllUsuariosAsync();
        var usuario = usuarios.FirstOrDefault(u => u.Email == email);

        if (usuario != null)
        {
            // Salva o ID do usuário na sessão
            HttpContext.Session.SetInt32("UserId", usuario.IdUsuario);

            // Redireciona para a tela de perfil
            return RedirectToAction("Profile", new { id = usuario.IdUsuario });
        }

        ViewBag.ErrorMessage = "Email não encontrado.";
        return View();
    }

    /// <summary>
    /// Exibe a tela de registro.
    /// </summary>
    /// <returns>Retorna a view de registro.</returns>
    public IActionResult Register()
    {
        return View(new UsuarioDto());
    }

    /// <summary>
    /// Processa o registro de um novo usuário.
    /// </summary>
    /// <param name="usuarioDto">Dados do usuário a serem registrados.</param>
    /// <returns>Redireciona para o login ou retorna a view de registro com mensagens de erro.</returns>
    [HttpPost]
    public async Task<IActionResult> Register(UsuarioDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.AddUsuarioAsync(usuarioDto);

            // Após registro, redireciona para o login
            return RedirectToAction("Login");
        }

        return View(usuarioDto);
    }

    /// <summary>
    /// Exibe a tela de perfil do usuário.
    /// </summary>
    /// <param name="id">ID do usuário. Obtém da sessão se não for fornecido.</param>
    /// <returns>Retorna a view do perfil ou redireciona para o login.</returns>
    public async Task<IActionResult> Profile(int id)
    {
        // Obtém o ID do usuário da sessão se não foi passado na URL
        id = id == 0 ? HttpContext.Session.GetInt32("UserId") ?? 0 : id;

        if (id == 0)
        {
            // Redireciona para a tela de login caso não haja usuário autenticado
            return RedirectToAction("Login");
        }

        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    /// <summary>
    /// Atualiza o perfil do usuário.
    /// </summary>
    /// <param name="usuarioDto">Dados atualizados do perfil do usuário.</param>
    /// <returns>Redireciona para o perfil ou retorna a view de perfil com mensagens de erro.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UsuarioDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.UpdateUsuarioAsync(usuarioDto);
            ViewBag.SuccessMessage = "Perfil atualizado com sucesso!";
            return RedirectToAction("Profile", new { id = usuarioDto.IdUsuario });
        }

        ViewBag.ErrorMessage = "Erro ao atualizar o perfil. Verifique os dados fornecidos.";
        return View("Profile", usuarioDto);
    }

    /// <summary>
    /// Exclui a bio do usuário.
    /// </summary>
    /// <param name="id">ID do usuário. Obtém da sessão se não for fornecido.</param>
    /// <returns>Redireciona para o perfil do usuário.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteBio(int id)
    {
        if (id == 0)
        {
            id = HttpContext.Session.GetInt32("UserId") ?? 0; // Obtém o ID do usuário da sessão
        }

        if (id == 0)
        {
            return RedirectToAction("Login");
        }

        await _usuarioService.DeleteBioAsync(id); // Chama o serviço para deletar a bio
        ViewBag.SuccessMessage = "Bio excluída com sucesso!";
        return RedirectToAction("Profile", new { id });
    }
}
