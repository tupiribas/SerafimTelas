using MySql.Data.MySqlClient;
using SerafTelis.model.entidades;
using System;
using System.Collections.Generic;

namespace SerafTelis.model.dao.implementacao
{
    public class FuncionarioDaoCRUD : IFuncionarioDAO
    {
        private MySqlConnection conn;
        private const double TAXA_COMISSAO_FUNCIONARIO = 0.5;

        public FuncionarioDaoCRUD(MySqlConnection conn)
        {
            this.conn = conn;
        }

        public void Atualizar(Funcionario obj)
        {
            conn.Open();
            try
            {
                string sql = "UPDATE tb_funcionario SET nome = ?, sobrenome = ?, email = ?, senha = ? WHERE id = ?";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                Console.WriteLine(obj.Nome);
                stmt.Parameters.Add("@nome", MySqlDbType.VarChar, 20).Value = obj.Nome;
                stmt.Parameters.Add("@sobrenome", MySqlDbType.VarChar, 20).Value = obj.Sobrenome;
                stmt.Parameters.Add("@email", MySqlDbType.VarChar, 50).Value = obj.Email;
                stmt.Parameters.Add("@senha", MySqlDbType.VarChar, 20).Value = obj.Senha;
                stmt.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = obj.Id;


                int linhaAlterada = stmt.ExecuteNonQuery();
                if (linhaAlterada <= 0)
                {
                    SituacaoFuncionario.SetAtualizarFuncionario(false);
                }

                SituacaoFuncionario.SetAtualizarFuncionario(true);
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falha ao atualizar os dados do funcionario. Cod.:00c7" + error);
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
                string sql = "DELETE FROM tb_funcionario WHERE id = ?";
                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = id;

                int linhasAlteradas = stmt.ExecuteNonQuery();

                if (linhasAlteradas <= 0)
                {
                    SituacaoFuncionario.SetDeletarFuncionario(false);
                }

                SituacaoFuncionario.SetDeletarFuncionario(true);
            }
            catch (MySqlException error)
            {
                SituacaoFuncionario.SetDeletarFuncionario(false);
                throw new db.BDException("FALHOU EM DELETAR O FUNCIONARIO. >>> Cod.:00c8 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public void Inserir(Funcionario obj)
        {
            conn.Open();

            try
            {
                string sql = "INSERT INTO tb_funcionario (nome, sobrenome, email, senha, comissao) " +
                              "VALUE (?, ?, ?, ?, ?)";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@nome", MySqlDbType.VarChar, 20).Value = obj.Nome;
                stmt.Parameters.Add("@sobrenome", MySqlDbType.VarChar, 20).Value = obj.Sobrenome;
                stmt.Parameters.Add("@email", MySqlDbType.VarChar, 50).Value = obj.Email;
                stmt.Parameters.Add("@senha", MySqlDbType.VarChar, 20).Value = obj.Senha;
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
                throw new db.BDException("FALHOU EM CADASTRAR O FUNCIONARIO! Cod.:00c3 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        private Funcionario InstaciandoFuncionario(MySqlDataReader leituraDosDados)
        {
            try
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Id = leituraDosDados.GetInt32(0);
                funcionario.Nome = leituraDosDados.GetString(1);
                funcionario.Sobrenome = leituraDosDados.GetString(2);
                funcionario.Email = leituraDosDados.GetString(3);
                funcionario.Senha = leituraDosDados.GetString(4);

                // Trantando se a comissao for nula
                if (!leituraDosDados.IsDBNull(5))
                {
                    funcionario.Comissao = leituraDosDados.GetDouble(5);
                }

                return funcionario;
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falha na instanciação do objeto Funcionario. >>> Cod.:00c6 \n" + error);
            }
        } // Feito

        public List<Funcionario> MostrarTodos()
        {
            conn.Open();

            try
            {
                string sql = "SELECT * FROM tb_funcionario";
                MySqlCommand stmt = new MySqlCommand(sql, conn);

                var leitura = stmt.ExecuteReader();

                List<Funcionario> funcionarios = new List<Funcionario>();
                List<Funcionario> listaDeFuncionarios = funcionarios;

                while (leitura.Read())
                {
                    listaDeFuncionarios.Add(InstaciandoFuncionario(leitura));
                }

                return listaDeFuncionarios;
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falhou em mostrar os dados dos funcionarios. >>> Cod.:00c4 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public Funcionario ProcurarPeloId(int id)
        {
            conn.Open();

            try
            {
                string sql = "SELECT id, nome, sobrenome, email, senha, comissao FROM tb_funcionario WHERE tb_funcionario.id = ?";

                MySqlCommand stmt = new MySqlCommand(sql, conn);
                stmt.Parameters.Add("@tb_funcionario.id", MySqlDbType.Int32).Value = id;

                var leitura = stmt.ExecuteReader();
                
                if (leitura.Read())
                {
                    SituacaoFuncionario.SetBuscarFuncionarioPeloId(true);
                    return InstaciandoFuncionario(leitura);
                }
                SituacaoFuncionario.SetBuscarFuncionarioPeloId(false);

                return null;
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falhou em mostrar o dado do funcionario " +
                    "\nId do funcionario:" + id + ". >>> Cod.:00c5 \n" + error);
            }
            finally
            {
                conn.Close();
            }
        } // Feito

        public void AtualizarComissao(Funcionario obj, double valorTotalDaCompra)
        {
            conn.Open();
            try
            {
                string sql = "UPDATE tb_funcionario SET comissao = ? WHERE id = ?";

                MySqlCommand stmt = new MySqlCommand(sql, conn);

                stmt.Parameters.Add("@comissao", MySqlDbType.Double).Value = valorTotalDaCompra * TAXA_COMISSAO_FUNCIONARIO;
                stmt.Parameters.Add("@id", MySqlDbType.Int32).Value = obj.Id;

                int linhaAlterada = stmt.ExecuteNonQuery();
                if (linhaAlterada <= 0)
                {
                    SituacaoFuncionario.SetAtualizarFuncionario(false);
                }

                SituacaoFuncionario.SetAtualizarFuncionario(true);
            }
            catch (MySqlException error)
            {
                throw new db.BDException("Falha ao atualizar os dados do funcionario. Cod.:00c7" + error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
