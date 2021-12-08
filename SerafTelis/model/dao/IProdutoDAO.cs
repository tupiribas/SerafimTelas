using SerafTelis.model.entidades;
using System;
using System.Collections.Generic;

namespace SerafTelis.model.dao
{
    public interface IProdutoDAO
    {
        void Inserir(Produto obj);
        void Atualizar(Produto obj);
        void DeletePeloId(int id);
        Produto ProcurarPeloId(int id);
        List<Produto> MostrarTodos();
    }
}
