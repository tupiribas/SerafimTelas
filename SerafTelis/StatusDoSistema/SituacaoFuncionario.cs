using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerafTelis.model.dao.implementacao
{
    internal class SituacaoFuncionario
    {

        private static bool CriarFuncionario;
        private static bool BuscarFuncionarioPeloId;
        private static bool AtualizarFuncionario;
        private static bool DeletarFuncionario;

        public static void SetCriarFuncionario(bool status)
        {
            CriarFuncionario = status;
        }

        public static bool IsCriarFuncionario()
        {
            return CriarFuncionario;
        }

        public static void SetBuscarFuncionarioPeloId(bool status)
        {
            BuscarFuncionarioPeloId = status;
        }

        public static bool IsBuscarFuncionarioPeloId()
        {
            return BuscarFuncionarioPeloId;
        }

        public static void SetAtualizarFuncionario(bool status)
        {
            AtualizarFuncionario = status;
        }

        public static bool IsAtualizarFuncionario()
        {
            return AtualizarFuncionario;
        }

        public static bool IsDeletarFuncionario()
        {
            return DeletarFuncionario;
        }

        public static void SetDeletarFuncionario(bool status)
        {
            DeletarFuncionario = status;
        }
    }
}
