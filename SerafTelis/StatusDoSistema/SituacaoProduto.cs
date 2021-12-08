using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerafTelis.model.dao.implementacao
{
    internal class SituacaoProduto
    {
        private static bool CriarProduto;
        private static bool BuscarProdutoPeloId;
        private static bool AtualizarProduto;
        private static bool DeletarProduto;

        public static void SetCriarProduto(bool status)
        {
            CriarProduto = status;
        }

        public static bool IsCriarProduto()
        {
            return CriarProduto;
        }

        public static void SetBuscarProdutoPeloId(bool status)
        {
            BuscarProdutoPeloId = status;
        }

        public static bool IsBuscarProdutoPeloId()
        {
            return BuscarProdutoPeloId;
        }

        public static void SetAtualizarProduto(bool status)
        {
            AtualizarProduto = status;
        }

        public static bool IsAtualizarProduto()
        {
            return AtualizarProduto;
        }

        public static bool IsDeletarProduto()
        {
            return DeletarProduto;
        }

        public static void SetDeletarProduto(bool status)
        {
            DeletarProduto = status;
        }
    }
}
