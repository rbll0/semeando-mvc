using Microsoft.AspNetCore.Mvc;
using semeando_mvc.Application.Dtos;
using semeando_mvc.Application.Services;

namespace semeando_mvc.Controllers;

/// <summary>
/// Controller para gerenciamento de usuários na área administrativa.
/// </summary>
public class AdminController : Controller
{
    private readonly IUsuarioService _usuarioService;

    public AdminController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Exibe a lista de todos os usuários.
    /// </summary>
    /// <returns>Retorna a view com a lista de usuários.</returns>
    public async Task<IActionResult> Index()
    {
        var usuarios = await _usuarioService.GetAllUsuariosAsync();
        return View(usuarios);
    }

    /// <summary>
    /// Exibe o formulário para adicionar um novo usuário.
    /// </summary>
    /// <returns>Retorna a view de criação de usuário.</returns>
    public IActionResult Add()
    {
        return View();
    }

    /// <summary>
    /// Processa a criação de um novo usuário.
    /// </summary>
    /// <param name="usuarioDto">Objeto contendo os dados do novo usuário.</param>
    /// <returns>Redireciona para a lista de usuários.</returns>
    [HttpPost]
    public async Task<IActionResult> Add(UsuarioDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.AddUsuarioAsync(usuarioDto);
            return RedirectToAction("Index");
        }
        return View(usuarioDto);
    }

    /// <summary>
    /// Exibe o formulário de edição de um usuário existente.
    /// </summary>
    /// <param name="id">ID do usuário a ser editado.</param>
    /// <returns>Retorna a view de edição de usuário.</returns>
    public async Task<IActionResult> Edit(int id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        if (usuario == null)
        {
            TempData["ErrorMessage"] = "Usuário não encontrado.";
            return RedirectToAction("Index");
        }
        return View(usuario);
    }

    /// <summary>
    /// Processa a atualização de um usuário existente.
    /// </summary>
    /// <param name="usuarioDto">Objeto contendo os dados atualizados do usuário.</param>
    /// <returns>Redireciona para a lista de usuários.</returns>
    [HttpPost]
    public async Task<IActionResult> EditConfirmed(UsuarioDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _usuarioService.UpdateUsuarioAsync(usuarioDto);
                TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao atualizar o usuário: {ex.Message}";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Dados inválidos. Verifique as informações fornecidas.";
        }

        return View("Edit", usuarioDto);
    }

    /// <summary>
    /// Exibe o formulário de confirmação para excluir um usuário.
    /// </summary>
    /// <param name="id">ID do usuário a ser excluído.</param>
    /// <returns>Retorna a view de exclusão de usuário.</returns>
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    /// <summary>
    /// Processa a exclusão de um usuário.
    /// </summary>
    /// <param name="id">ID do usuário a ser excluído.</param>
    /// <returns>Redireciona para a lista de usuários.</returns>
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            TempData["SuccessMessage"] = "Usuário excluído com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao excluir o usuário: {ex.Message}";
        }
        return RedirectToAction("Index");
    }
}
