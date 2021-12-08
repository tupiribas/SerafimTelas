

namespace SerafTelis.StatusDoSistema
{
    internal class SituacaoVenda
    {
        private static bool VenderProduto;
        private static bool GerarNotaDaVenda;
        private static bool AtualizarComissaoDoFuncionario;
        private static bool AtualizarValorTotal;
        private static bool AtualizarEstoque;

        public static void SetVenderProduto(bool status)
        {
            VenderProduto = status;
        }

        public static bool IsVenderProduto()
        {
            return VenderProduto;
        }

        public static void SetGerarNotaDaVenda(bool status)
        {
            GerarNotaDaVenda = status;
        }

        public static bool IsGerarNotaDaVenda()
        {
            return GerarNotaDaVenda;
        }

        public static void SetAtualizarComissaoDoFuncionario(bool status)
        {
            AtualizarComissaoDoFuncionario = status;
        }

        public static bool IsAtualizarComissaoDoFuncionario()
        {
            return AtualizarComissaoDoFuncionario;
        }

        public static void SetAtualizarValorTotal(bool status)
        {
            AtualizarValorTotal = status;
        }

        public static bool IsAtualizarValorTotal()
        {
            return AtualizarValorTotal;
        }

        public static void SetAtualizarEstoque(bool status)
        {
            AtualizarEstoque = status;
        }

        public static bool IsAtualizarEstoque()
        {
            return AtualizarEstoque;
        }
    }
}
