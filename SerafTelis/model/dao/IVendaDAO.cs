using SerafTelis.model.entidades;
using System;

namespace SerafTelis.model.dao
{
    public interface IVendaDAO
    { 
        void VenderProduto(Venda dadosDaVenda);

        Venda GerarNotaFiscal();

        void AtualizarComissaoDoFuncionario(int idFuncionario, double valorTotal);

        void AtualizarEstoque(int idProduto, double quantidadeComprada);
    }
}
