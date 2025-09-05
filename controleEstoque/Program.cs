using System.ComponentModel.DataAnnotations; // Excelente! O uso de ValidationException é uma ótima prática para validação.

public class Produto
{
    // As propriedades agora são "completas", com campos privados.
    // Os campos privados garantem que os dados só podem ser
    // acessados e modificados através das propriedades,
    // que contêm a lógica de validação.
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
        // O 'private set' é uma prática de segurança. Isso impede que o
        // ID seja alterado fora da classe, garantindo a sua imutabilidade
        // após a criação do objeto.
        private set
        {
            if (value <= 0)
            {
                // `nameof(value)` é excelente para obter o nome do parâmetro
                // e tornar a mensagem de erro mais útil e fácil de refatorar.
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
        // A regra de negócio para a alteração do valor está aqui.
        if (this.Valor * 100 <= novoValor)
        {
            throw new ValidationException("O novo valor não pode ser 100x maior do que o valor atual.");
        }
        
        // A propriedade 'Valor' está com 'private set'. Para alterá-la, precisamos fazer isso
        // dentro da classe. A validação já ocorre no 'set'.
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
            // A validação no 'set' de QtdEstoque garante que o valor não seja negativo.
        
            QtdEstoque -= qtd;
        }
        else
        {
            throw new ArgumentException("Operação inválida. Use '+' para adicionar ou '-' para remover.");
        }
    }
}