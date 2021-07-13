using LanchesMC.Models;
using LanchesMC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LanchesMC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository,CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;

            
        } 

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            var items = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItems = items;

            if (_carrinhoCompra.CarrinhoCompraItems.Count==0)
            {
                ModelState.AddModelError("", "Seu Carrinho está Vazio, Inclua um Lanche...");
                
            }
            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu Pedido :) ";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                _carrinhoCompra.LimparCarrinho();
                return View("~/Views/Pedido/CheckoutCompleto.cshtml",pedido);
            }


            return View(pedido);
        }

        public IActionResult CheckoutCompleto()
        {
            ViewBag.Cliente = TempData["Cliente"];
            ViewBag.DataPedido = TempData["DataPedido"];
            ViewBag.NumeroPedido = TempData["NumeroPedido"];
            ViewBag.TotalPedido = TempData["TotalPedido"];
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu Pedido :) ";
            return View();
        }

    }

 }


