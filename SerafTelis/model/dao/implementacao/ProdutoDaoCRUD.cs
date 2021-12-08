using MySql.Data.MySqlClient;
using SerafTelis.db;
using SerafTelis.model.entidades;
using System;
using System.Collections.Generic;

namespace SerafTelis.model.dao.implementacao
{
    public class ProdutoDaoCRUD : IProdutoDAO
    {
        private MySqlConnection conn;

        public ProdutoDaoCRUD(MySqlConnection conn)
        {
            this.conn = conn;
        }

        public void Atualizar(Produto obj)
        {
            conn.Open();
            try
            {
                string sql = "UPDATE tb_produto SET nome = ?, preco = ?, quantidade = ? WHERE id = ?";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@nome", MySqlDbType.VarChar, 20).Value = obj.Nome;
                stmt.Parameters.Add("@preco", MySqlDbType.VarChar, 20).Value = obj.Preco;
                stmt.Parameters.Add("@quantidade", MySqlDbType.VarChar, 50).Value = obj.Quantidade;
                stmt.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = obj.Id;


                int linhaAlterada = stmt.ExecuteNonQuery();
                if (linhaAlterada <= 0)
                {
                    SituacaoProduto.SetAtualizarProduto(false);
                }

                SituacaoProduto.SetAtualizarProduto(true);
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falha ao atualizar os dados do produto. Cod.:00c9" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public void DeletePeloId(int id)
        {
            conn.Open();
            try
            {
                string sql = "DELETE FROM tb_produto WHERE id = ?";
                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = id;

                int linhasAlteradas = stmt.ExecuteNonQuery();

                if (linhasAlteradas <= 0)
                {
                    SituacaoProduto.SetDeletarProduto(false);
                }

                SituacaoProduto.SetDeletarProduto(true);
            }
            catch (MySqlException error)
            {
                SituacaoFuncionario.SetDeletarFuncionario(false);
                throw new db.BDException("FALHOU EM DELETAR O PRODUTO. >>> Cod.:00c8 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public void Inserir(Produto obj)
        {
            conn.Open();

            try
            {
                string sql = "INSERT INTO tb_produto (nome, preco, quantidade) " +
                              "VALUE (?, ?, ?)";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@nome", MySqlDbType.VarChar, 20).Value = obj.Nome;
                stmt.Parameters.Add("@preco", MySqlDbType.VarChar, 20).Value = obj.Preco;
                stmt.Parameters.Add("@quantidade", MySqlDbType.VarChar, 50).Value = obj.Quantidade;
                stmt.Parameters.Add("@comissao", MySqlDbType.Double).Value = 0;

                int linhaAlterada = stmt.ExecuteNonQuery();

                if (linhaAlterada <= 0)
                {
                    SituacaoFuncionario.SetCriarFuncionario(false);
                }

                SituacaoFuncionario.SetCriarFuncionario(true);
            }
            catch (MySqlException error)
            {
                SituacaoFuncionario.SetCriarFuncionario(false);
                throw new db.BDException("Falha ao cadastrar o produto! Cod.:00c10 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        private Produto InstaciandoProduto(MySqlDataReader leituraDosDados)
        {
            try
            {
                Produto produto = new Produto();

                    produto.Id = leituraDosDados.GetInt32(0);
                    produto.Nome = leituraDosDados.GetString(1);
                    produto.Preco = Convert.ToDouble(leituraDosDados.GetString(2));
                    produto.Quantidade = leituraDosDados.GetDouble(3);

                return produto;
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falha na instanciação do objeto produto. >>> Cod.:00c13 \n" + error);
            }
        }

        public List<Produto> MostrarTodos()
        {
            conn.Open();

            try
            {
                // Mostrar usuários
                string sql = "SELECT * FROM tb_produto";
                MySqlCommand stmt = new MySqlCommand(sql, conn);

                var leitura = stmt.ExecuteReader();

                List<Produto> produtos = new List<Produto>();
                List<Produto> listaDeProdutos = produtos;

                while (leitura.Read())
                {
                    listaDeProdutos.Add(InstaciandoProduto(leitura));
                }

                return listaDeProdutos;
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falhou em mostrar os dados dos funcionarios. >>> Cod.:00c11 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public Produto ProcurarPeloId(int id)
        {
            conn.Open();

            try
            {
                string sql = "SELECT * FROM tb_produto WHERE id = ?";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                var leitura = stmt.ExecuteReader();

                if (leitura.Read())
                {
                    SituacaoProduto.SetBuscarProdutoPeloId(true);
                    return InstaciandoProduto(leitura);
                }
                SituacaoProduto.SetBuscarProdutoPeloId(false);

                return null;
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falhou em mostrar o dado do produto " +
                    "\nId do produto:" + id + ". >>> Cod.:00c12 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito
    }
}