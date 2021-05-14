using LanchesMC.Models;
using System.Collections.Generic;

namespace LanchesMC.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
