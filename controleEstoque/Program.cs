
using System.ComponentModel.DataAnnotations;

class Produto
{
    private int id;
    private string nome;
    private double valor;
    private int qtdEstoque;

    public Produto(int id, string nome, double valor, int qtdEstoque)
    {
        validaCampos(id, nome, valor, qtdEstoque);

        this.id = id;
        this.nome = nome;
        this.valor = valor;
        this.qtdEstoque = qtdEstoque;
    }

    public Produto(string nome, double valor, int qtdEstoque)
    {
        //APLICAR AQUI AS SOBRECARGAS
    }


    public Produto(string nome, int qtdEstoque)
    {
        //APLICAR AQUI AS SOBRECARGAS
    }

    private void validaCampos(int id, string nome, double valor, int qtdEstoque)
    {
        if (nome.Length < 4)
        {
            throw new ValidationException("O nome do produto deve ter mais de 3 caracteres");
        }
        if (valor < 0)
        {
            throw new ValidationException("O valor do produto não pode ser negativo");
        }
        if (qtdEstoque < 0)
        {
            throw new ValidationException("A quantidade em estoque do produto não pode ser negativa");
        }
    }

    public void alterarValor(double novoValor)
    {
        if (this.valor * 100 <= novoValor)
        {
            throw new ValidationException("O novo valor não pode ser 100x maior do que o valor atual.");
            // TROCAR EXCEPTION POR UMA EXCEPTION PERSONALIZADA
        }

        this.valor = novoValor;
    }

    public void atualizarEstoque(int qtd, char operacao)
    {
        // APLICAR AQUI ATUALIZAÇÃO DE ESTOQUE
    }
}