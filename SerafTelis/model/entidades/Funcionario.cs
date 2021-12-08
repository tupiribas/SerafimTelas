using System;

namespace SerafTelis.model.entidades
{
    public class Funcionario
    {
        public int Id;
        public string Nome;
        public string Sobrenome;
        public double Comissao;

        public string Email;
        public string Senha;

        public Funcionario()
        {
        }

        public Funcionario(string nome, string sobrenome, string email, string senha) 
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
        }

        public Funcionario(string email, string senha) 
        {
            Email = email;
            Senha = senha;
        }
    }
}