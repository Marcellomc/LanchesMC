using LanchesMC.Context;
using LanchesMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMC.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext,
                                CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }


        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;

            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;


            foreach(var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetalhe = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Lanche.Preco
                };

                _appDbContext.PedidoDetalhes.Add(pedidoDetalhe);


            }
            _appDbContext.SaveChanges();
        }
    }
}
