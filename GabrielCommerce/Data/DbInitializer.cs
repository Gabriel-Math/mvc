﻿using GabrielCommerce.Models;
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
                return;   // DB has been seeded
            }

            //Simulando um usuário já cadastrado e uma compra
            var usuario = new Usuario { Nome = "Asdrubal", Email = "teste@teste.com", Cpf = "12345678901" };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            var produto4 = new Produto { Nome = "produto4", Preco = 1000 };
            var produto1 = new Produto { Nome = "produto1", Preco = 1000 };
            var produto2 = new Produto { Nome = "produto2", Preco = 2000 };
            var produto3 = new Produto { Nome = "produto3", Preco = 3000 };
            var produtos = new[] { produto1, produto2, produto3, produto4 };
            context.Produtos.AddRange(produtos);
            context.SaveChanges();

            var pedido = new Pedido { UsuarioId = usuario.Id, Data = DateTime.Now };
            context.Pedidos.Add(pedido);
            context.SaveChanges();

            var produtosComprados = new List<Produto> { produto1, produto3 };

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
