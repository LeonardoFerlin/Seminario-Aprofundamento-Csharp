using System.ComponentModel.DataAnnotations;

public class Produto
{
    // As propriedades agora são "completas", com campos privados.
    private int _id;
    private string _nome;
    private double _valor;
    private int _qtdEstoque;

    // Construtor principal
    public Produto(int id, string nome, double valor, int qtdEstoque)
    {
        // A validação agora é feita pelo 'set' das propriedades.
        // Se um valor inválido for passado, a exceção será lançada aqui.
        Id = id;
        Nome = nome;
        Valor = valor;
        QtdEstoque = qtdEstoque;
    }
    
    // Construtor sobrecarregado (sem Id)
    // Usa 'this()' para chamar o construtor principal, passando 0 como valor padrão para o Id.
    public Produto(string nome, double valor, int qtdEstoque)
        : this(0, nome, valor, qtdEstoque)
    {
    }

    // Construtor sobrecarregado (sem Id e sem Valor)
    // Usa 'this()' para chamar o construtor principal.
    public Produto(string nome, int qtdEstoque)
        : this(0, nome, 0, qtdEstoque)
    {
    }

    // As propriedades agora têm a validação no 'set'.
    // O 'get' não muda.
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
    
    // O método ValidaCampos() não é mais necessário porque a validação agora está nas propriedades.

    public void AlterarValor(double novoValor)
    {
        if (this.Valor * 100 <= novoValor)
        {
            throw new ValidationException("O novo valor não pode ser 100x maior do que o valor atual.");
        }
        
        // A propriedade 'Valor' está com 'private set'. Para alterá-la, precisamos fazer isso
        // dentro da classe, como você fez. A validação já ocorre no 'set'.
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
            QtdEstoque -= qtd; // A validação no 'set' de QtdEstoque garante que o valor não seja negativo.
        }
        else
        {
            throw new ArgumentException("Operação inválida. Use '+' para adicionar ou '-' para remover.");
        }
    }
} 