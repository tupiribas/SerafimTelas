using SerafTelis.model.entidades;
using System.Collections.Generic;

namespace SerafTelis.model.dao
{
    public interface IFuncionarioDAO
    {
        void Inserir(Funcionario obj);
        void Atualizar(Funcionario obj);
        void DeletePeloId(int id);
        Funcionario ProcurarPeloId(int id);
        List<Funcionario> MostrarTodos();
    }
}
