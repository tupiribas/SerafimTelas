using SerafTelis.model.dao.implementacao;

namespace SerafTelis.model.dao
{
    public static class DAOCentral
    {
        public static IFuncionarioDAO CriarFuncionarioDao()
        {
            return new FuncionarioDaoCRUD(db.DB.GetConnection());
        }
        public static IProdutoDAO CriarProdutoDao()
        {
            return new ProdutoDaoCRUD(db.DB.GetConnection());
        }
        public static IVendaDAO CriarVendaDao()
        {
            return new VendaDao(db.DB.GetConnection());
        }
    }
}
