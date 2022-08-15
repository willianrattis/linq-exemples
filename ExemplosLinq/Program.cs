namespace ExemplosLinq;

class Program
{
    static void Main(string[] args)
    {
        var listaProdutos = PopularDadosProduto();
        var listaCategorias = PopularDadosCategoria();
        var listaProdutosCategorias = PopularDados();

        LinqRangeLambda(listaProdutos, 1, 5);
    }

    static void LinqSelectForeach(List<Produto> listaProdutos)
    {
        List<ProdutoDto> result = new();

        foreach (var item in listaProdutos)
            result.Add(new ProdutoDto { Nome = item.Nome, Valor = item.Valor });

        ImprimirResultado(result, "resultado Select Com Foreach:");
    }

    static void LinqSelecManytForeach(List<Produto> listaProdutos)
    {
        List<Categoria> result = new();

        foreach (var item in listaProdutos)
        {
            foreach (var categoria in item.Categorias)
            {
                result.Add(categoria);
            }
        }

        ImprimirResultado(result.DistinctBy(c => c.Id).OrderBy(c => c.Id), "resultado SelectMany Com Foreach:");
    }

    static void LinqSelectLambdaProduto(List<Produto> listaProdutos)
    {
        IEnumerable<Produto> result = listaProdutos
                                            .Select(p => p);

        ImprimirResultado(result, "resultado Select Lambda:");
    }

    static void LinqSelectLambdaProdutoPropriedade(List<Produto> listaProdutos)
    {
        IEnumerable<string> result = listaProdutos
                                            .Select(p => p.Id.ToString());

        ImprimirResultadoPropriedade(result, "resultado Select Lambda:");
    }

    static void LinqSelectLambdaProdutoDto(List<Produto> listaProdutos)
    {
        IEnumerable<ProdutoDto> result = listaProdutos
                                            .Select(p => new ProdutoDto
                                            {
                                                Nome = p.Nome,
                                                Valor = p.Valor
                                            });

        ImprimirResultado(result, "resultado Select Lambda:");
    }

    static void LinqSelectLambdaProdutoComWhere(List<Produto> listaProdutos)
    {
        IEnumerable<Produto> result = listaProdutos
                                            .Where(p => p.CategoriaId == 4)
                                            .Select(p => p);

        ImprimirResultado(result, "resultado Select Lambda:");
    }

    static void LinqSelectManyLambdaProduto(List<Produto> listaProdutos)
    {
        // select many junta varias listas em uma única lista 
        IEnumerable<Categoria> result = listaProdutos
                                            .SelectMany(c => c.Categorias)
                                            .DistinctBy(c => c.Id)
                                            .OrderBy(c => c.Id);

        ImprimirResultado(result, "resultado Select Lambda:");
    }

    static void LinqSelectManyComWhereLambdaProduto(List<Produto> listaProdutos)
    {
        // select many junta varias listas em uma única lista 
        IEnumerable<Categoria> result = listaProdutos
                                            .SelectMany(c => c.Categorias)
                                            .Where(c => FiltrarPorIndisponiveis(c))
                                            .DistinctBy(c => c.Id)
                                            .OrderBy(c => c.Id);


        bool FiltrarProdutosId(Categoria categoria) => categoria.Id > 2 && categoria.Id < 7;

        bool FiltrarPorDisponiveis(Categoria categoria) => categoria.Status == true;

        bool FiltrarPorIndisponiveis(Categoria categoria) => categoria.Status == false;


        ImprimirResultado(result, "resultado Select Many Where Lambda:");
    }

    static void LinqSelectManySemWhereComFiltroLambdaProduto(List<Produto> listaProdutos)
    {

        IEnumerable<Categoria> result = listaProdutos
                                            .SelectMany(c => FiltrarIndisponiveisPorCategoria(c.Categorias))
                                            .DistinctBy(c => c.Id)
                                            .OrderBy(c => c.Id);

        List<Categoria> FiltrarIndisponiveisPorCategoria(List<Categoria> categorias)
        {
            var listIndisponiveis = new List<Categoria>();

            foreach (var item in categorias)
            {
                if (item.Status == false)
                    listIndisponiveis.Add(item);
            }

            return listIndisponiveis;
        }


        ImprimirResultado(result, "resultado Select Many Sem Where Lambda:");
    }

    static void LinqSelectManyLambdaProdutoNome(List<Produto> listaProdutos)
    {
        // select many junta varias listas em uma única lista 
        // passado no construtor da lista os dados que foram selecionados
        IEnumerable<string> result = listaProdutos
                                        .SelectMany(c => new List<string>(c.Categorias.Select(i => i.Nome)))
                                        .Distinct()
                                        .OrderBy(c => c);


        ImprimirResultadoPropriedade(result, "resultado SelectMany Lambda:");
    }

    static void LinqSelectManyLambdaProdutoStatus(List<Produto> listaProdutos)
    {
        // select many junta varias listas em uma única lista 
        // passado no construtor da lista os dados que foram selecionados
        IEnumerable<bool> result = listaProdutos
                                        .SelectMany(c => new List<bool>(c.Categorias.Select(i => i.Status)))
                                        .Distinct()
                                        .OrderBy(c => c);

        ImprimirResultadoPropriedade(result, "resultado SelectMany Lambda:");
    }

    static void LinqComWhereLambda(List<Produto> listaProdutos)
    {
        var result = listaProdutos.Where(c => c.Id >= 2 && c.Id <= 5);

        ImprimirResultado(result, "Resultado do Where:");
    }

    static void LinqComWhereLambdaFiltradoPorMetodo(List<Produto> listaProdutos)
    {
        var result = listaProdutos.Where(c => FiltrarProdutoPorValor(c));

        bool FiltrarProdutoPorValor(Produto produtoFiltro) => produtoFiltro.Valor > 50;

        ImprimirResultado(result, "Resultado do Where:");
    }

    static void LinqComOrderByLambda(List<Produto> listaProdutos)
    {
        var result = listaProdutos.Select(c => c).OrderBy(c => c.Id);

        ImprimirResultado(result, "Resultado do OrderBy:");
    }

    static void LinqComOrderByDescendingLambda(List<Produto> listaProdutos)
    {
        var result = listaProdutos.Select(c => c).OrderByDescending(c => c.Id);

        ImprimirResultado(result, "Resultado do OrderBy Descending:");
    }

    static void LinqComReverseLambda(List<Produto> listaProdutos)
    {
        // Lista materializada VOID
        // listaProdutos.Reverse();

        var result = listaProdutos.Select(c => c).Reverse();

        ImprimirResultado(result, "Resultado do reverse:");
    }

    public static void LinqTakeSkipLambda(List<Produto> listaProdutos)
    {
        var trePrimeiros = listaProdutos.Take(3);
        var ignorarTrePrimeiros = listaProdutos.Skip(3);
        var ignorarTrePrimeirosPegarTresProximos = listaProdutos.Skip(3).Take(3);

        ImprimirResultado(trePrimeiros, "Capturando os 3 primeiros itens:");
        ImprimirResultado(ignorarTrePrimeiros, "Ignorando os 3 primeiros itens:");
        ImprimirResultado(ignorarTrePrimeirosPegarTresProximos, "Ignorando os 3 primeiros itens:");
    }

    public static void LinqSumLambda(List<Produto> listaProdutos)
    {
        // valor total da lista
        var valotTotal = listaProdutos.Sum(prod => prod.Valor);

        Console.WriteLine($"Valor total: {valotTotal}");
    }

    public static void LinqMediaLambda(List<Produto> listaProdutos)
    {
        // valor total da lista
        var mediaValorProdutos = listaProdutos.Average(prod => prod.Valor);

        Console.WriteLine($"Media: {mediaValorProdutos}");
    }

    public static void LinqCountLambda(List<Produto> listaProdutos)
    {
        // quantos itens tem na lista
        var quantidadeItensLista = listaProdutos.Count();

        Console.WriteLine($"Total de itens: {quantidadeItensLista}");
    }

    public static void LinqCountComFiltroLambda(List<Produto> listaProdutos, int filtro)
    {
        var result = listaProdutos.Count(prod => prod.Valor > filtro);

        Console.WriteLine($"Total de itens com valor maior que {filtro}: {result}");
    }

    public static void LinqRangeLambda(List<Produto> listaProdutos, int inicio, int fim)
    {
        // criar uma lista com um range de numeros
        var range = Enumerable.Range(inicio, fim);

        foreach (var item in range)
            Console.WriteLine(item);
    }

    public static void LinqRepeatLambda(List<Produto> produtos)
    {
        // criar uma lista com varios itens semelhantes
        var listaProdutoIguais = Enumerable.Repeat(new Produto() { Id = 1 }, 5);

        foreach (var item in listaProdutoIguais)
            Console.WriteLine(item);
    }

    public static void LinqComWhereComparacao(List<Produto> listaProdutos, int filtro)
    {
        var result = from produto in listaProdutos
                     where produto.Valor == filtro
                     select new ProdutoDto
                     {
                         Nome = produto.Nome,
                         Valor = produto.Valor,
                         Status = produto.Status
                     };

        ImprimirResultado(result, $"Resultado da comparação com {filtro}:");
    }

    public static void LinqComGroupByPorCategoria(List<Produto> listaProdutos)
    {
        IEnumerable<IGrouping<int, Produto>> result = from produto in listaProdutos
                                                      group produto by produto.CategoriaId into produtosAgrupados
                                                      select produtosAgrupados;

        foreach (var itemProduto in result)
        {
            Console.WriteLine(itemProduto.Key);

            foreach (var item in itemProduto)
            {
                Console.WriteLine($"Produto: {item.Nome} | Categoria: {item.CategoriaId}");
            }
        }
    }

    public static void LinqComJoinAnonima(List<Produto> listaProdutos, List<Categoria> listaCategorias)
    {
        var result = from produto in listaProdutos
                     join categoria in listaCategorias
                     on produto.CategoriaId equals categoria.Id
                     select new
                     {
                         Produto = produto,
                         Categoria = categoria
                     };


        ImprimirResultadoListaAnonima(result, $"Resultado JOIN:");
    }

    public static void TakeSkip(List<Produto> listaProdutos)
    {
        var trePrimeiros = new List<Produto>();
        var ignorarTrePrimeiros = new List<Produto>();

        for (int i = 0; i < listaProdutos.Count; i++)
        {
            if (i < 3)
            {
                trePrimeiros.Add(listaProdutos[i]);
            }    
            else if (i > 2)
            {
                ignorarTrePrimeiros.Add(listaProdutos[i]);
            }
        }

        ImprimirResultado(trePrimeiros, "Capturando os 3 primeiros itens:");
        ImprimirResultado(ignorarTrePrimeiros, "Ignorando os 3 primeiros itens:");
    }

    public static void Soma(List<Produto> listaProdutos)
    {
        var total = 0M;

        foreach (var item in listaProdutos)
        {
            total += item.Valor;
        }

        Console.WriteLine($"Soma de todos os valores: {total}");
    }

    public static void Media(List<Produto> listaProdutos)
    {
        var total = 0M;

        foreach (var item in listaProdutos)
        {
            total += item.Valor;
        }

        Console.WriteLine($"Soma de todos os valores: {(total / listaProdutos.Count)}");
    }

    #region Popular os Dados 

    static List<Produto> PopularDados()
    {
        // FONTE DE DADOS
        var listaProdutos = new List<Produto>()
        {
            new Produto
            {
                Id = 1,
                Nome = "Camiseta",
                Status = true,
                Valor = 100,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 2,
                        Nome = "Vestuario",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 5,
                        Nome = "Roupas Masculinas",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 5,
                        Nome = "Roupas Femininas",
                        Status = false
                    }
                }
            },
            new Produto
            {
                Id = 2,
                Nome = "Short",
                Status = true,
                Valor = 50,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 2,
                        Nome = "Vestuario",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 5,
                        Nome = "Roupas Masculinas",
                        Status = false
                    },
                    new Categoria
                    {
                        Id = 5,
                        Nome = "Roupas Femininas",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 3,
                Nome = "Video Game",
                Status = true,
                Valor = 3500,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 3,
                        Nome = "Eletronicos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 6,
                        Nome = "Jogos",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 4,
                Nome = "Maquina de lavar",
                Status = true,
                Valor = 50,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 4,
                        Nome = "Eletrodomesticos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 7,
                        Nome = "Casa",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 5,
                Nome = "Microondas",
                Status = true,
                Valor = 1500,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 4,
                        Nome = "Eletrodomesticos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 7,
                        Nome = "Casa",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 8,
                        Nome = "Utencilios Domesticos",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 6,
                Nome = "Arroz",
                Status = true,
                Valor = 25,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 1,
                        Nome = "Alimentos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 9,
                        Nome = "Cesta Basica",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 7,
                Nome = "Feijão",
                Status = true,
                Valor = 17,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 1,
                        Nome = "Alimentos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 9,
                        Nome = "Cesta Basica",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 8,
                Nome = "TV",
                Status = true,
                Valor = 50,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 3,
                        Nome = "Eletronicos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 4,
                        Nome = "Eletrodomesticos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 7,
                        Nome = "Casa",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 10,
                        Nome = "Tv & Video",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 9,
                Nome = "Geladeira",
                Status = true,
                Valor = 50,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 4,
                        Nome = "Eletrodomesticos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 7,
                        Nome = "Casa",
                        Status = true
                    }
                }
            },
            new Produto
            {
                Id = 10,
                Nome = "Panetone",
                Status = true,
                Valor = 17,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Id = 1,
                        Nome = "Alimentos",
                        Status = true
                    },
                    new Categoria
                    {
                        Id = 9,
                        Nome = "Cesta Basica",
                        Status = false
                    }
                }
            }
        };

        return listaProdutos;
    }

    static List<Produto> PopularDadosProduto()
    {
        // FONTE DE DADOS
        var listaProdutos = new List<Produto>()
        {
            new Produto {Id = 1, CategoriaId = 3, Nome = "Camiseta", Status = true, Valor = 100},
            new Produto {Id = 2, CategoriaId = 3, Nome = "Short", Status = true, Valor = 1},
            new Produto {Id = 3, CategoriaId = 1, Nome = "Video Game", Status = true, Valor = 99},
            new Produto {Id = 4, CategoriaId = 4, Nome = "Maquina de lavar", Status = false, Valor = 32},
            new Produto {Id = 5, CategoriaId = 4, Nome = "Microondas", Status = true, Valor = 90},
            new Produto {Id = 6, CategoriaId = 2, Nome = "Arroz", Status = true, Valor = 55},
            new Produto {Id = 7, CategoriaId = 2, Nome = "Feijão", Status = true, Valor = 12},
            new Produto {Id = 8, CategoriaId = 1, Nome = "TV", Status = true, Valor = 45},
            new Produto {Id = 9, CategoriaId = 4, Nome = "Geladeira", Status = true, Valor = 10},
        };

        return listaProdutos;
    }

    static List<Categoria> PopularDadosCategoria()
    {
        var listaCategorias = new List<Categoria>
        {
            new Categoria { Id = 1, Nome = "Eletronicos", Status = true },
            new Categoria { Id = 2, Nome = "Alimentos", Status = true },
            new Categoria { Id = 3, Nome = "Vestuario", Status = false },
            new Categoria { Id = 4, Nome = "Eletrodomesticos", Status = true },
        };

        return listaCategorias;
    }

    #endregion

    #region Impressão

    static void ImprimirResultadoPropriedade(IEnumerable<string> propriedade, string mensagem, string cabecalho = null)
    {
        if (cabecalho is not null)
            Console.WriteLine(cabecalho);

        foreach (var item in propriedade)
            Console.WriteLine($"{mensagem} {item}");

        Console.WriteLine("\n");
    }

    static void ImprimirResultadoPropriedade(IEnumerable<bool> propriedade, string mensagem, string cabecalho = null)
    {
        if (cabecalho is not null)
            Console.WriteLine(cabecalho);

        foreach (var item in propriedade)
            Console.WriteLine($"{mensagem} {item}");
    }

    static void ImprimirResultado(IEnumerable<Produto> produtos, string mensagem, string cabecalho = null)
    {
        if (cabecalho is not null)
            Console.WriteLine(cabecalho);

        foreach (var item in produtos)
            Console.WriteLine($"{mensagem} {item.Id} | {item.Nome} | {item.Valor} | {item.Categorias}");

        Console.WriteLine("\n");
    }

    static void ImprimirResultado(IEnumerable<Categoria> produtos, string mensagem, string cabecalho = null)
    {
        if (cabecalho is not null)
            Console.WriteLine(cabecalho);

        foreach (var item in produtos)
            Console.WriteLine($"{mensagem} {item.Id} | {item.Nome} | {item.Status}");

        Console.WriteLine("\n");
    }

    static void ImprimirResultado(IEnumerable<ProdutoDto> produtos, string mensagem, string cabecalho = null)
    {
        if (cabecalho is not null)
            Console.WriteLine(cabecalho);

        foreach (var item in produtos)
            Console.WriteLine($"{mensagem} {item.Nome} | {item.Valor}");

        Console.WriteLine("\n");
    }

    static void ImprimirResultadoListaAnonima(IEnumerable<dynamic> produtos, string mensagem, string cabecalho = null)
    {
        if (cabecalho is not null)
            Console.WriteLine(cabecalho);

        foreach (var item in produtos)
            Console.WriteLine($"{mensagem} {item.Nome} | {item.Valor}");

        Console.WriteLine("\n");
    }
    #endregion
}
