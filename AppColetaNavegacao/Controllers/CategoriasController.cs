using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppColetaNavegacao.Data;
using AppColetaNavegacao.Models;

namespace AppColetaNavegacao.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly DBLivros _context;

        public CategoriasController(DBLivros context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            if (!MetodosGenericos.UsuarioAtivo(User))
            {
                return RedirectToAction("Login", "Identity/Account");
            }

            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)categoria, OPERACAO.VISUALIZAR);

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)categoria, OPERACAO.INSERIR);

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)categoria, OPERACAO.ALTERAR);

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();

                    //Publica informações de navegação do usuário
                    (new MetodosGenericos()).PublicaInformacaoNavegacao((object)categoria, OPERACAO.ALTERAR);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)categoria, OPERACAO.EXCLUIR);

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            //Publica informações de navegação do usuário
            (new MetodosGenericos()).PublicaInformacaoNavegacao((object)categoria, OPERACAO.EXCLUIR);

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
