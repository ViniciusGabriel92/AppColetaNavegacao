using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppColetaNavegacao.Data;
using AppColetaNavegacao.Models;
using AppColetaNavegacao.Others;
using System.Web;

namespace AppColetaNavegacao.Controllers
{
    public class LivrosController : Controller
    {
        private readonly DBLivros _context;

        public LivrosController(DBLivros context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            if (!MetodosGenericos.UsuarioAtivo(User))
            {
                return RedirectToAction("Login", "Identity/Account");
            }

            return View(await _context.Livros.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)livro, OPERACAO.VISUALIZAR);

            var categoriaLivros = _context.Categorias.Where(c => c.Id == livro.CategoriaId);
            ViewBag.CategoriaLivros = categoriaLivros.ToList();        
            return View(livro);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categoriaLivros = _context.Categorias;
            ViewBag.CategoriaLivros = categoriaLivros.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Autor,DataPublicacao,Anunciante,TelAnunciante,CategoriaId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();

                //Publica informações de navegação do usuário
                (new MetodosGenericos()).PublicaInformacaoNavegacao((object)livro, OPERACAO.INSERIR);

                return RedirectToAction(nameof(Index));
            }

            return View(livro);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)livro, OPERACAO.ALTERAR);

            var categoriaLivros = _context.Categorias;
            ViewBag.CategoriaLivros = categoriaLivros.ToList();

            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,DataPublicacao,Anunciante,TelAnunciante,CategoriaId")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();

                    //Publica informações de navegação do usuário
                    (new MetodosGenericos()).PublicaInformacaoNavegacao((object)livro, OPERACAO.ALTERAR);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)livro, OPERACAO.EXCLUIR);

            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)livro, OPERACAO.EXCLUIR);

            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
