using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GabrielCommerce.Data;
using GabrielCommerce.Models;

namespace GabrielCommerce.Controllers
{
    public class PedidosController : Controller
    {
        private readonly BDContext _context;

        public PedidosController(BDContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var bDContext = _context.Pedidos.Include(p => p.Usuario).Include(p => p.Itens).Include("Itens.Produto");
            return View(await bDContext.ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.SingleOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }


        // POST: Pedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }

        public IActionResult SelecionarProduto(int? id)
        {
            var produtos = _context.Produtos.ToList();
            ViewBag.PedidoId = id;
            return View(produtos);
        }

        [HttpGet]
        public IActionResult AddToCart(int id, int? pedidoId)
        {
            var produto = _context.Produtos.Find(id);
            var pedido = pedidoId.HasValue ?

                _context.Pedidos.Include(p => p.Itens).First(p => p.Id == pedidoId.Value) :

                new Pedido()
                {
                    Itens = new List<PedidoItem>()
                };

            pedido.Itens.Add(new PedidoItem { ProdutoId = id });

            if (pedidoId.HasValue)
                _context.Update(produto);
            else
                _context.Add(pedido);

            _context.SaveChanges();
            ViewBag.PedidoId = pedido.Id;
            return RedirectToAction("Carrinho", new { pedidoId = pedido?.Id });
        }

        [HttpGet("[controller]/[action]/{pedidoId}")]
        public IActionResult Carrinho(int pedidoId)
        {
            var pedido = _context.Pedidos
                .Include(p => p.Itens)
                .Include("Itens.Produto")
                .First(p => p.Id == pedidoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            return View(pedido);
        }
    }
}
