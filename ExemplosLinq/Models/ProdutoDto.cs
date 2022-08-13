namespace ExemplosLinq.Models;

public record ProdutoDto
{
    public string Nome { get; set; }
    public bool Status { get; set; }
    public decimal Valor { get; set; }
}