using MySql.Data.MySqlClient;
using SerafTelis.model.entidades;
using System;
using SerafTelis.db;
using SerafTelis.StatusDoSistema;

namespace SerafTelis.model.dao.implementacao
{
    public class VendaDao : IVendaDAO
    {
        private MySqlConnection conn;

        public VendaDao(MySqlConnection conn)
        {
            this.conn = conn;
        }

        private Venda InstaciandoVenda(MySqlDataReader leituraDosDados)
        {
            try
            {
                Venda venda = new Venda();

                venda.Id = leituraDosDados.GetInt32(0);
                venda.DataAtual = Convert.ToString(leituraDosDados.GetDateTime(1)); 
                venda.Produto.Id = leituraDosDados.GetInt32(2);
                venda.Produto.Nome = leituraDosDados.GetString(3);
                venda.Produto.Preco = leituraDosDados.GetDouble(4);
                venda.Produto.Quantidade = leituraDosDados.GetDouble(5);
                venda.Funcionario.Id = leituraDosDados.GetInt32(6);
                venda.Funcionario.Nome = leituraDosDados.GetString(7);
                venda.Funcionario.Sobrenome = leituraDosDados.GetString(8);
                venda.Total = leituraDosDados.GetDouble(4) * leituraDosDados.GetDouble(5);
                ValorTotal(leituraDosDados.GetInt32(0), leituraDosDados.GetDouble(4) * leituraDosDados.GetDouble(5));

                return venda;
            }
            catch (MySqlException error)
            {
                throw new BDException("Falha na instanciação da venda. >>> Cod.:00c13 \n" + error);
            }
        }

        public Venda GerarNotaFiscal()
        {
            conn.Open();
            try
            {
                string sql = "SELECT tb_venda.id,              tb_venda.data_atual, tb_venda.id_produto,      " +
                             "       tb_produto.nome,          tb_produto.preco,    tb_venda.metros,          " +
                             "       tb_venda.id_funcionario, tb_funcionario.nome, tb_funcionario.sobrenome,  " +
                             "       tb_venda.valor_total                                                     " +
                             "FROM tb_venda                                                                   " +
                             "INNER JOIN tb_produto ON tb_venda.id_produto = tb_produto.id                    " +
                             "INNER JOIN tb_funcionario ON tb_venda.id_funcionario = tb_funcionario.id        " +
                             "ORDER BY tb_venda.id DESC limit 1;                                              ";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                var leitura = stmt.ExecuteReader();

                if (leitura.Read())
                {
                    SituacaoVenda.SetGerarNotaDaVenda(true);
                    return InstaciandoVenda(leitura);
                }
                SituacaoVenda.SetGerarNotaDaVenda(false);

                return null;
            }
            catch (MySqlException error)
            {
                throw new BDException("Falha em gerar a nota fiscal da venda. >>> Cod.:00c13 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public void VenderProduto(Venda dadosDaVenda)
        {
            conn.Open();
            try
            {
                string sql = "INSERT INTO tb_venda(id_produto, id_funcionario, metros, data_atual) " +
                              "VALUES (?, ?, ?, ?);";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@id_produto", MySqlDbType.Int32).Value = dadosDaVenda.IdProduto;
                stmt.Parameters.Add("@id_funcionario", MySqlDbType.Int32).Value = dadosDaVenda.IdFuncionario;
                stmt.Parameters.Add("@metros", MySqlDbType.Double).Value = dadosDaVenda.Metros;
                stmt.Parameters.Add("@data_atual", MySqlDbType.DateTime).Value = dadosDaVenda.DataAtual;

                int linhasAlteradas = stmt.ExecuteNonQuery();

                if (linhasAlteradas > 0)
                {
                    SituacaoVenda.SetVenderProduto(true);
                }
            }
            catch (MySqlException error)
            {
                throw new BDException("Falha na venda do produto! Cod.:00c14 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public void AtualizarComissaoDoFuncionario(int idFuncionario, double valorTotal)
        {
            conn.Open();
            try
            {
                string sql = "UPDATE tb_funcionario SET comissao = comissao + ? WHERE id = ?";

                MySqlCommand stmt2 = new MySqlCommand(sql, conn);
                stmt2.Parameters.Add("@comissao", MySqlDbType.Double).Value = valorTotal * 0.5;
                stmt2.Parameters.Add("@id", MySqlDbType.Int32).Value = idFuncionario;

                int linhasAlteradas = stmt2.ExecuteNonQuery();

                if (linhasAlteradas <= 0)
                {
                    SituacaoVenda.SetAtualizarComissaoDoFuncionario(false);
                }
                SituacaoVenda.SetAtualizarComissaoDoFuncionario(true);
            }
            catch (MySqlException error)
            {
                throw new BDException("Falha para atualizar comissao do funcionário! Cod.:00c15 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        private void ValorTotal(int id, double valorTotal)
        {
            conn.Close();
            conn.Open();
            try
            {
                string sql = "UPDATE tb_venda SET valor_total = ? WHERE id = ?";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@valor_total", MySqlDbType.Double).Value = valorTotal;
                stmt.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                int linhasAlteradas = stmt.ExecuteNonQuery();
                
                if (linhasAlteradas <= 0)
                {
                    SituacaoVenda.SetAtualizarValorTotal(false);
                }
                SituacaoVenda.SetAtualizarValorTotal(true);
            }
            catch (MySqlException error)
            {
                throw new BDException("Falha para atualizar valor total do produto! Cod.:00c16 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public void AtualizarEstoque(int idProduto, double quantidadeComprada)
        {
            conn.Close();
            conn.Open();
            try
            {
                string sql = "UPDATE tb_produto SET quantidade = quantidade - ? WHERE id = ?";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@valor_total", MySqlDbType.Double).Value = quantidadeComprada;
                stmt.Parameters.Add("@id", MySqlDbType.Int32).Value = idProduto;

                int linhasAlteradas = stmt.ExecuteNonQuery();

                if (linhasAlteradas <= 0)
                {
                    SituacaoVenda.SetAtualizarEstoque(false);
                }
                SituacaoVenda.SetAtualizarEstoque(true);
            }
            catch (MySqlException error)
            {
                throw new BDException("Falha para atualizar valor total do produto! Cod.:00c17 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito
    }
}
