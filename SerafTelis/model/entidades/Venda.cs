using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerafTelis.model.entidades
{
    public class Venda
    {
        public int Id;
        public int IdFuncionario;
        public int IdProduto;
        public double Metros;
        public string DataAtual;
        public double Total;

        public Funcionario Funcionario = new Funcionario();
        public Produto Produto = new Produto();

        public Venda()
        {
        }
        public Venda(double metros, string dataAtual)
        {
            Metros = metros;
            DataAtual = dataAtual;
        }

        public Venda(int idFuncionaio, int idProduto)
        {
            IdFuncionario = idFuncionaio;
            IdProduto = idProduto;
        }

        public Venda(int idFuncionaio, int idProduto, double metros, string dataAtual)
        {
            IdFuncionario = idFuncionaio;
            IdProduto = idProduto;
            Metros = metros;
            DataAtual = dataAtual;
        }

        public void AtualizarEstoqueDeProdutos(double quantidade)
        {
            Produto.Quantidade -= quantidade;
        }
    }
}
