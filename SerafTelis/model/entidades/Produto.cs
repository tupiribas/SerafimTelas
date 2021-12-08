using System;

namespace SerafTelis.model.entidades
{
    public class Produto
    {
        public int Id;
        public string Nome;
        public double Preco;
        public double Quantidade;

        public Funcionario Funcionario;

        public Produto()
        {
        }

        public Produto(string nome, double preco, double quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public Produto(int id, string nome, double preco, double quantidade)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
