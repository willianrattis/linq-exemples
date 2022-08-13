using System.Collections.Generic;

namespace ExemplosLinq.Models;

class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public bool Status { get; set; }
    public decimal Valor { get; set; }
    public int CategoriaId { get; set; }
    public List<Categoria> Categorias { get; set; }
}
