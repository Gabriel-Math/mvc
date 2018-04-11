using GabrielCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GabrielCommerce.Data
{
    public class DbInitializer
    {
        public static void Initialize(BDContext context)
        {
            context.Database.EnsureCreated();

            if (context.Produtos.Any())
            {
                return;   // DB has been seeded
            }

            //Simulando um usuário já cadastrado e uma compra
            var usuario = new Usuario { Nome = "Gabriel Matheus", Email = "gabriel@gmail.com", Cpf = "12345678901" };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            var produto1 = new Produto { Nome = "Mouse", Categoria= "Perifericos", Preco = 30 };
            var produto2 = new Produto { Nome = "Teclado", Categoria = "Perifericos", Preco = 100 };
            var produto3 = new Produto { Nome = "Placa de video", Categoria = "Componentes", Preco = 550 };
            var produto4 = new Produto { Nome = "Gabinete", Categoria = "Componentes", Preco = 100 };
            var produto5 = new Produto { Nome = "Fonte", Categoria = "Componentes", Preco = 250 };
            var produto6 = new Produto { Nome = "Placa mãe", Categoria = "Componentes", Preco = 400 };
            var produto7 = new Produto { Nome = "Hard Disk", Categoria = "Componentes", Preco = 400 };
            var produto8 = new Produto { Nome = "Memoria RAM", Categoria = "Componentes", Preco = 180 };
            var produto9 = new Produto { Nome = "Processador", Categoria = "Componentes", Preco = 1500 };

            var produtos = new[] { produto1, produto2, produto3, produto4, produto5, produto6, produto7, produto8, produto9 };
            context.Produtos.AddRange(produtos);
            context.SaveChanges();

            var pedido = new Pedido { UsuarioId = usuario.Id, Data = DateTime.Now };
            context.Pedidos.Add(pedido);
            context.SaveChanges();

            var produtosComprados = new List<Produto> { produto1, produto3, produto8 };

            //Salvando cada item de Pedido
            foreach (var prod in produtosComprados)
            {
                var pedidoItem = new PedidoItem { PedidoId = pedido.Id, ProdutoId = prod.Id };
                context.Itens.Add(pedidoItem);
                context.SaveChanges();
            }

        }
    }
}
