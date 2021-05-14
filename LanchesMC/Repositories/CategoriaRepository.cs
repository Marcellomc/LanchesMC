using LanchesMC.Context;
using LanchesMC.Models;
using System.Collections.Generic;

namespace LanchesMC.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext contexto)
        {
            _context = contexto;

        }
        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
