using System.ComponentModel.DataAnnotations; // Excelente! O uso de ValidationException é uma ótima prática para validação.

       //Exceção
public class ProdutoException : Exception
{
    public ProdutoException(string message) : base(message) { }
}

public class Produto
{
    private int _id;
    private string _nome;
    private double _valor;
    private int _qtdEstoque;

    // Construtor principal
    public Produto(int id, string nome, double valor, int qtdEstoque)
    {
        Id = id;
        Nome = nome;
        Valor = valor;
        QtdEstoque = qtdEstoque;
    }
    
    // Construtor sobrecarregado (sem Id)
    public Produto(string nome, double valor, int qtdEstoque)
        : this(0, nome, valor, qtdEstoque)
    {
    }

    // Construtor sobrecarregado (sem Id e sem Valor)
    public Produto(string nome, int qtdEstoque)
        : this(0, nome, 0, qtdEstoque)
    {
    }

    public int Id
    {
        get { return _id; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("O ID deve ser um número positivo.", nameof(value));
            }
            _id = value;
        }
    }

    public string Nome
    {
        get { return _nome; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
            {
                throw new ValidationException("O nome do produto deve ter mais de 3 caracteres e não pode ser nulo.");
            }
            _nome = value;
        }
    }

    public double Valor
    {
        get { return _valor; }
        private set
        {
            if (value < 0)
            {
                throw new ValidationException("O valor do produto não pode ser negativo.");
            }
            _valor = value;
        }
    }

    public int QtdEstoque
    {
        get { return _qtdEstoque; }
        private set
        {
            if (value < 0)
            {
                throw new ValidationException("A quantidade em estoque do produto não pode ser negativa.");
            }
            _qtdEstoque = value;
        }
    }
    
    public void AlterarValor(double novoValor)
    {
        if (this.Valor * 100 <= novoValor)
        {
            throw new ValidationException("O novo valor não pode ser 100x maior do que o valor atual.");
        }
        
        Valor = novoValor;
    }

    public void AtualizarEstoque(int qtd, char operacao)
    {
        if (operacao == '+')
        {
            QtdEstoque += qtd;
        }
        else if (operacao == '-')
        {
            QtdEstoque -= qtd;
        }
        else
        {
            throw new ProdutoException("Operação inválida. Use '+' para adicionar ou '-' para remover.");
        }
    }


    // Reposição de estoque
    public void ReporEstoque(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ProdutoException("A quantidade para reposição deve ser maior que zero.");
        }

        QtdEstoque += quantidade;
    }

    // Vender produto
    public void Vender(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ProdutoException("A quantidade para venda deve ser maior que zero.");
        }

        if (quantidade > QtdEstoque)
        {
            throw new ProdutoException("Estoque insuficiente para realizar a venda.");
        }

        QtdEstoque -= quantidade;
    }
}
