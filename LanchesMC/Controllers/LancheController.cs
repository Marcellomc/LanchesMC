using LanchesMC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMC.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List()
        {
            ViewBag.Lanche = "Lanches";
            ViewData["Categoria"] = "Categoria";

            var lanches = _lancheRepository.Lanches;
            return View(lanches);
        }


    }
}
