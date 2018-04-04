using GabrielCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GabrielCommerce.Data
{
    public class DbInitializer
    {
        public static void Initialize(BDContext context)
        {
            context.Database.EnsureCreated();

            if (context.Produtos.Any())
            {
                return;
            }

            var usuario = new Usuario { Nome = "Gabriel Matheus", Email = "gabriel@hotmail.com", Cpf = "0123456789101" };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            var produto1 = new Produto { Nome = "Barra de ferro", PrecoUnitario = 200 };
            var produto2 = new Produto { Nome = "Barra de aço", PrecoUnitario = 500 };
            var produtos = new[] { produto1, produto2 };
            context.Produtos.AddRange(produtos);
            context.SaveChanges();

            var pedido = new Pedido { UsuarioId = usuario.Id, Data = DateTime.Now };
            context.Pedidos.Add(pedido);
            context.SaveChanges();

            var produtosComprados = new List<Produto> { produto1, produto2 };

            foreach (var prod in produtosComprados)
            {
                var pedidoItem = new PedidoItem { PedidoId = pedido.Id, ProdutoId = prod.Id };
                context.PedidosItens.Add(pedidoItem);
                context.SaveChanges();
            }

        }
    }
}
