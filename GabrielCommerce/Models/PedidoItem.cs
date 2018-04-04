using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GabrielCommerce.Models
{
    public class PedidoItem
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

    }
}
